// MyNewHiringWebApp.Infrastructure/Messaging/RabbitMqCandidateEventPublisher.cs
using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;
using MyNewHiringWebApp.Application.Messaging.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqCandidateEventPublisher : ICandidateEventPublisher
    {
        private readonly RabbitMqConnectionFactory _connectionFactory;
        private const string CandidateQueueName = "candidate.created.queue";
        private const string CandidateSkillQueueName = "candidate.skill.created.queue";

        public RabbitMqCandidateEventPublisher(RabbitMqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task PublishCandidateCreatedAsync(CandidateCreatedEto eto)
        {
            // using ile bağlantı ve kanal kapatılıyor
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: CandidateQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eto));

                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2; // persistent

                channel.BasicPublish(exchange: "", routingKey: CandidateQueueName, basicProperties: props, body: body);
            }

            return Task.CompletedTask;
        }

        public Task PublishCandidateSkillCreatedAsync(CandidateSkillCreatedEto eto)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: CandidateSkillQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eto));

                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;

                channel.BasicPublish(exchange: "", routingKey: CandidateSkillQueueName, basicProperties: props, body: body);
            }

            return Task.CompletedTask;
        }
    }
}
