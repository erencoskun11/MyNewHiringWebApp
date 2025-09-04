using Azure.Identity;
using Elasticsearch.Net;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Messaging
{
    public class RabbitMqConnectionFactory
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        public RabbitMqConnectionFactory(string hostName,string userName,string password)
        {
            _hostName = hostName;
            _userName = userName;
            _password = password;
        }
        
            public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hotName,
                UserName = _userName,
                Password = _password
            };
            return factory.CreateConnectionAsync();
        }
        
    }
}
