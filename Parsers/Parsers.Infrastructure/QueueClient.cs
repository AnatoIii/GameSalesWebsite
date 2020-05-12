using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parsers.Core;
using Parsers.Core.Models;
using RabbitMQ.Client;

namespace Parsers.Infrastructure
{
    public class QueueClient
    {
        IConnection _connection;
        IModel _channel;
        ILogger _logger;
        QueueSettings _queueSettings;
        public QueueClient(ILogger logger, QueueSettings queueSettings)
        {
            _logger = logger;
            _queueSettings = queueSettings;
            InitializeConnection();
            InitializeQueue();
        }
        private void InitializeConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            try
            {
                factory.Uri = new Uri(_queueSettings.ConnectionString);
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch(Exception e)
            {
                _logger.Log(e.Message);
                throw new Exception(e.Message);
            }
        }

        private void InitializeQueue()
        {
            _channel.ExchangeDeclare(_queueSettings.ExchangeName,ExchangeType.Direct);
            _channel.QueueDeclare(_queueSettings.QueueName, false, false, false, null);
            _channel.QueueBind(_queueSettings.QueueName,_queueSettings.ExchangeName, null, null);
        }

        public void SendEntries(IEnumerable<GameEntry> entries)
        {
            _logger.Log($"Started Sending Entries - {DateTime.Now}");
            string data = JsonConvert.SerializeObject(entries);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            _channel.BasicPublish(_queueSettings.ExchangeName, null, null, dataBytes);
            _logger.Log($"Ended Sending Entries - {DateTime.Now}");
        }
    }
}
