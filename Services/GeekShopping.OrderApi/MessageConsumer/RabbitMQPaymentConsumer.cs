using System.Text;
using System.Text.Json;
using GeekShopping.OrderApi.Messages;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.RabbitMQSender;
using GeekShopping.OrderApi.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderApi.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string exchangeName= "DirectPaymentUpdateExchange";        
        private const string PaymentOrderUpdateQueueName= "PaymentOrderUpdateQueueName";
        public RabbitMQPaymentConsumer(OrderRepository repository,IRabbitMQMessageSender rabbitMQMessageSender){
            _repository = repository;
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
            _channel.QueueDeclare(PaymentOrderUpdateQueueName,false,false,false,null);
            _channel.QueueBind(PaymentOrderUpdateQueueName,exchangeName,"PaymentOrder");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel,evt) => {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVo vo = JsonSerializer.Deserialize<UpdatePaymentResultVo>(content);

                UpdatePaymentStatus(vo).GetAwaiter().GetResult();
                //encerrar processamento, ou seja remover a menssagem da lista
                _channel.BasicAck(evt.DeliveryTag,false);
                
            };
            _channel.BasicConsume(PaymentOrderUpdateQueueName,false,consumer);
            return Task.CompletedTask;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVo vo)
        {
          
            try
            {
                await _repository.UpdateOrderPaymentStatus(vo.OrderId,vo.Status);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}