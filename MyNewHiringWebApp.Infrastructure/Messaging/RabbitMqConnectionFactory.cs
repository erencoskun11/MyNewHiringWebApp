// MyNewHiringWebApp.Infrastructure/Messaging/RabbitMqConnectionFactory.cs
using System;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqConnectionFactory
    {
        private readonly RabbitMQ.Client.ConnectionFactory _factory;

        public RabbitMqConnectionFactory(string hostName = "localhost", string userName = "guest", string password = "guest")
        {
            _factory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public RabbitMQ.Client.IConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }
    }
}

