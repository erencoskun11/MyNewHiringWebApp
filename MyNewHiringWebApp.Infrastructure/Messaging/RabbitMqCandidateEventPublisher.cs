using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Messaging.Events;
using MyNewHiringWebApp.Application.Messaging.Interfaces;
using RabbitMQ.Client;
namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqCandidateEventPublisher : ICandidateEventPublisher
    {
        private readonly RabbitMqConnectionFactory _connectionFactory;
        private const string QueueName = "candidate.created.queue";


        public RabbitMqCandidateEventPublisher(RabbitMqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task PublishCandidateCreatedAsync(CandidateCreatedEvent @event)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            
            channel.QueueDeclare(queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body : Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

            var props = channel.CreateBasicProperties();
            props.DeliveryModel = 2;
            channel.BasicPublish(exchange: "",
                rotingKey: _queueName,
                basicProperties: props,
                body: body);

            return Task.ComletedTask;     
        }
    }
}
