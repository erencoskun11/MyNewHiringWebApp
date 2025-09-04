using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqCandidateEventConsumer : BackgroundService
    {
        private readonly RabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMqCandidateEventConsumer> _logger;
        private IModel? _channel;
        private const string CandidateQueueName = "candidate.created.queue";
        private readonly ushort _prefetchCount = 5; // aynı anda kaç mesaj işlenecek (ayarla)

        public RabbitMqCandidateEventConsumer(RabbitMqPersistentConnection persistentConnection, ILogger<RabbitMqCandidateEventConsumer> logger)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _persistentConnection.CreateModel();
            // ensure queue exists (durable true matches publisher)
            _channel.QueueDeclare(queue: CandidateQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // QoS: prefetch
            _channel.BasicQos(0, _prefetchCount, false);

            _logger.LogInformation("RabbitMQ consumer initialized for queue {Queue}", CandidateQueueName);

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
                    // deserialize into expected ETO
                    var eto = JsonSerializer.Deserialize<CandidateCreatedEto>(message);
                    if (eto != null)
                    {
                        // --- BURAYA işleme mantığını ekle ---
                        // Örnek: loglama / db kaydı / dosyaya yazma / başka servise çağrı
                        _logger.LogInformation("Consumed candidate event: {Email}, Id: {Id}", eto.Email, eto.CandidateId);

                        // simulate async work:
                        await Task.Delay(50, stoppingToken);
                    }
                    else
                    {
                        _logger.LogWarning("Received null or invalid CandidateCreatedEto payload.");
                    }

                    // başarılı -> ack
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing candidate event: {Message}", message);

                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            _channel.BasicConsume(queue: CandidateQueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            try
            {
                _channel?.Close();
                _channel?.Dispose();
            }
            catch { /* ignore */ }

            base.Dispose();
        }
    }
}
