using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqCandidateSkillEventPublisher
    {
        private readonly RabbitMqConnectionFactory _connectionFactory;
        private const string CandidateSkillQueueName = "candidate.skill.created.queue";

        public RabbitMqCandidateSkillEventPublisher(RabbitMqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;    
        }
        public Task PublisherCandidateSkillCreatedAsync(CandidateCreatedEto eto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: CandidateSkillQueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eto));

            var props = channel.CreateBasicProperties();
            props.DeliveryMode = 2;

            channel.BasicPublish(
               exchange: "",
               routingKey: CandidateSkillQueueName,
               basicProperties: props,
               body: body);

            return Task.CompletedTask;
        }
    }
}
