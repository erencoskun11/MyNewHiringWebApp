using MyNewHiringWebApp.Application.ETOs.DepartmentEtos;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqDepartmentEventPublisher
    {
        private readonly RabbitMqConnectionFactory _connectionFactory;
        private const string DepartmentQueueName = "department.created.queue";

        public RabbitMqDepartmentEventPublisher(RabbitMqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task PublishDepartmentCreatedAsync(DepartmentCreateEtos eto)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: DepartmentQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eto));

                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2; 

                channel.BasicPublish(exchange: "",
                                     routingKey: DepartmentQueueName,
                                     basicProperties: props,
                                     body: body);
            }

            return Task.CompletedTask;
        }
    }
}
