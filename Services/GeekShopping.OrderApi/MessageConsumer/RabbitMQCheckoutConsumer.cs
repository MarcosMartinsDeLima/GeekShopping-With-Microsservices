using System.Text;
using System.Text.Json;
using GeekShopping.OrderApi.Messages;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderApi.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        public RabbitMQCheckoutConsumer(OrderRepository repository){
            _repository = repository;
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue:"checkoutqueue",false,false,false,arguments:null); 
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel,evt) => {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderVo vo = JsonSerializer.Deserialize<CheckoutHeaderVo>(content);

                ProcessOrder(vo).GetAwaiter().GetResult();
                //encerrar processamento, ou seja remover a menssagem da lista
                _channel.BasicAck(evt.DeliveryTag,false);
                
            };
            _channel.BasicConsume("checkoutqueue",false,consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderVo vo)
        {
            OrderHeader order = new ()
            {
                UserId = vo.UserId,
                FirstName = vo.FirstName,
                LastName = vo.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = vo.CardNumber,
                CouponCode = vo.CouponCode,
                CVV = vo.CVV,
                DiscountAmout = vo.DiscountTotal,
                Email = vo.Email,
                ExpiryMonthYear = vo.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                PaymentStatus = false,
                Phone = vo.Phone,
                DateTime = vo.DateTime,
            };
            foreach(var details in  vo.CartDetails )
            {
                OrderDetail detail = new(){
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count

                };
                order.CartTotalItens += detail.Count;
                order.OrderDetails.Add(detail);
            }

            await _repository.AddOrder(order);
        }
    }
}