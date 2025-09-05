using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyNewHiringWebApp.Application.ETOs.DepartmentEtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqDepartmentEventConsumer : BackgroundService
    {
        private readonly RabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMqDepartmentEventConsumer> _logger;
        private IModel? _channel;
        private const string DepartmentQueueName = "department.created.queue";
        private readonly ushort _prefetchCount = 5;
        
        public RabbitMqDepartmentEventConsumer(
            RabbitMqPersistentConnection persistentConnection,
            ILogger<RabbitMqDepartmentEventConsumer> logger
            )
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _persistentConnection.CreateModel();
            _channel.QueueDeclare(
                queue: DepartmentQueueName,
                durable: true,
                exclusive : false,
                autoDelete:false,
                arguments:null);

            _channel.BasicQos(0, _prefetchCount, false);
            _logger.LogInformation("RabbitMQ consumer initialized for queue {Queue}", DepartmentQueueName);
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
                    var eto = JsonSerializer.Deserialize<DepartmentCreateEtos>(message);
                    if (eto != null)
                    {
                        _logger.LogInformation("Consumed department event: {Id}, {Name}", eto.DepartmentId, eto.Name);
                        await Task.Delay(1, stoppingToken);
                    }
                    else
                    {
                        _logger.LogWarning("Received null or invalid DepartmentCreatedEvent payload.");

                    }
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing department event: {Message}", message);
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };
            _channel.BasicConsume(queue: DepartmentQueueName, autoAck: false, consumer: consumer);

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
