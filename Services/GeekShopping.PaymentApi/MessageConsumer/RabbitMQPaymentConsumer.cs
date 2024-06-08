using System.Text;
using System.Text.Json;
using GeekShopping.PaymentApi.Messages;
using GeekShopping.PaymentApi.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.PaymentApi.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IProcessPayment _processPayment;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        public RabbitMQPaymentConsumer(IProcessPayment processPayment,IRabbitMQMessageSender rabbitMQMessageSender){
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue:"orderpaymentprocessqueue",false,false,false,arguments:null); 
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel,evt) => {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);

                ProcessPayment(vo).GetAwaiter().GetResult();
                //encerrar processamento, ou seja remover a menssagem da lista
                _channel.BasicAck(evt.DeliveryTag,false);
                
            };
            _channel.BasicConsume("orderpaymentprocessqueue",false,consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            var result = _processPayment.PaymentProcessor();
            UpdatePaymentResultMessage paymentResult = new (){
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email,
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(paymentResult);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}