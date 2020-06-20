using DBAccess;
using GamesSaver.Services;
using GamesSaver.Services.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerBackgroundServices
{
    public class QueueReaderBackgroundService : BackgroundService
    {
        private IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private QueueSettings _queueSettings;

        public QueueReaderBackgroundService( IOptions<QueueSettings> queueSettings, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _queueSettings = queueSettings.Value;
            InitializeConnection();
            InitializeQueue();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var gameEntries = JsonConvert.DeserializeObject<IEnumerable<GameEntryDTO>>(content);
                    var dbcontext = scope.ServiceProvider.GetRequiredService<GameServiceDBContext>();
                    var _gamesPricesService = new GamesPricesService(dbcontext);
                    _gamesPricesService.SaveGamePrices(gameEntries);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume(_queueSettings.QueueName, false, consumer);
            return Task.CompletedTask;
        }
        private void InitializeConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(_queueSettings.ConnectionString);
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void InitializeQueue()
        {
            _channel.ExchangeDeclare(_queueSettings.ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueSettings.QueueName, false, false, false, null);
            _channel.QueueBind(_queueSettings.QueueName, _queueSettings.ExchangeName, _queueSettings.ExchangeName);
        }
    }
}
