using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqCandidateSkillEventConsumer : BackgroundService
    {
        private readonly RabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMqCandidateSkillEventConsumer> _logger;
        private IModel? _channel;
        private const string CandidateSkillQueueName = "candidate.skill.created.queue";
        private readonly ushort _prefetchCount = 5;

        public RabbitMqCandidateSkillEventConsumer(
            RabbitMqPersistentConnection persistentConnection,
            ILogger<RabbitMqCandidateSkillEventConsumer> logger)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _persistentConnection.CreateModel();
            _channel.QueueDeclare(
                queue: CandidateSkillQueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.BasicQos(0, _prefetchCount, false);

            _logger.LogInformation("RabbitMQ consumer initialized for queue {Queue}", CandidateSkillQueueName);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var eto = JsonSerializer.Deserialize<CandidateSkillCreatedEto>(message);
                    if (eto != null)
                    {
                        // Burada gerçek iş mantığını çağırabilirsin (DB insert, log, vs.)
                        _logger.LogInformation("Consumed candidate-skill event: CandidateId={CandidateId}, SkillId={SkillId}, SkillName={SkillName}, Level={Level}",
                            eto.CandidateId, eto.SkillId, eto.SkillName, eto.Level);

                        // Simüle / kısa asenkron işlem (isteğe bağlı)
                        await Task.Delay(1, stoppingToken);
                    }
                    else
                    {
                        _logger.LogWarning("Received null or invalid CandidateSkillCreatedEto payload.");
                    }

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing candidate-skill event: {Message}", message);
                    // Hata durumunda requeue: false -> mesaj kuyruğa geri koyulmaz (DLQ kullanıyorsan oraya gidebilir)
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            _channel.BasicConsume(queue: CandidateSkillQueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            try
            {
                _channel?.Close();
                _channel?.Dispose();
            }
            catch { }

            base.Dispose();
        }
    }
}