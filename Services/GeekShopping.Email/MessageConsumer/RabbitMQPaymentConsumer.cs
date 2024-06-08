using System.Text;
using System.Text.Json;
using GeekShopping.Email.Messages;
using GeekShopping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly EmailRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string exchangeName= "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName= "PaymentEmailUpdateQueueName";

        public RabbitMQPaymentConsumer(EmailRepository repository){
            _repository = repository;
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
            _channel.QueueDeclare(PaymentEmailUpdateQueueName,false,false,false,null);
            _channel.QueueBind(PaymentEmailUpdateQueueName,exchangeName,"PaymentEmail");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel,evt) => {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResulMessage message = JsonSerializer.Deserialize<UpdatePaymentResulMessage>(content);

                ProcessLogs(message).GetAwaiter().GetResult();
                //encerrar processamento, ou seja remover a menssagem da lista
                _channel.BasicAck(evt.DeliveryTag,false);
                
            };
            _channel.BasicConsume(PaymentEmailUpdateQueueName,false,consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessLogs(UpdatePaymentResulMessage message)
        {
          
            try
            {
                await _repository.LogEmail(message);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}