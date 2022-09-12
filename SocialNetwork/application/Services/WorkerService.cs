using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;
        private readonly string _queueName;

        public WorkerService(string queueName, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _queueName = queueName;
            _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        public async Task EnqueueAsync(string message)
        {
            var messageLog = new MessageLog() { Message = message, MessageStatus = "sent to " + _queueName };

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ILogRepositoryManager _logRepositoryManager =
                    scope.ServiceProvider.GetRequiredService<ILogRepositoryManager>();
                _logRepositoryManager.MessageLog.Create(messageLog);
                await _logRepositoryManager.SaveAsync();
            }
            

            var body = Encoding.UTF8.GetBytes(messageLog.Id + " " + message);
            _channel.BasicPublish(exchange: "",
                                routingKey: _queueName,
                                basicProperties: null,
                                body: body);
        }

        public void CloseConnection()
        {
            _conn?.Close();
        }
    }
}
