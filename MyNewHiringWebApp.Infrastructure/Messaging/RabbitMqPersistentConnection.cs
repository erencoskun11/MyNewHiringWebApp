using RabbitMQ.Client;
using System;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqPersistentConnection : IDisposable
    {
        private readonly RabbitMqConnectionFactory _factory;
        private IConnection? _connection;

        public RabbitMqPersistentConnection(RabbitMqConnectionFactory factory)
        {
            _factory = factory;
        }

        public IModel CreateModel()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _factory.CreateConnection();
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.IsOpen) _connection.Close();
                    _connection.Dispose();
                }
            }
            catch { /* ignore */ }
        }
    }
}
