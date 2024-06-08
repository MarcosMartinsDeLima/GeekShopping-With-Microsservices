using System.Text;
using System.Text.Json;
using GeekShopping.MessageBus;
using GeekShopping.PaymentApi.Messages;
using RabbitMQ.Client;

namespace GeekShopping.PaymentApi.RabbitMQSender
{
    public class RabbitMQSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string exchangeName= "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName= "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName= "PaymentOrderUpdateQueueName";

        public RabbitMQSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }    

        public void SendMessage(BaseMessage baseMessage)
        {
            //criar a conexão com rabbitMQ
            if(ConnectionExists())
            {
            using var channel = _connection.CreateModel();
            //declarar a exchange, durable false faz com que ela se apague após consumir
            channel.ExchangeDeclare(exchangeName,ExchangeType.Direct,durable:false);
            channel.QueueDeclare(PaymentEmailUpdateQueueName,false,false,false,null);
            channel.QueueDeclare(PaymentOrderUpdateQueueName,false,false,false,null);

            channel.QueueBind(PaymentEmailUpdateQueueName,exchangeName,"PaymentEmail");
            channel.QueueBind(PaymentOrderUpdateQueueName,exchangeName,"PaymentOrder");
            //converter basemessage em um array de bytes
            byte[] body = GetMessageAsByteArray(baseMessage);

            //publicando a messagem
            channel.BasicPublish(exchange:exchangeName,"PaymentEmail",basicProperties:null,body:body);
            channel.BasicPublish(exchange:exchangeName,"PaymentOrder",basicProperties:null,body:body);
        }
        }

        private byte[] GetMessageAsByteArray(BaseMessage baseMessage)
        {
            //esse options faz com que ele serialize todas as propriedades da classe e não apenas da classe mãe
            var options = new JsonSerializerOptions{
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)baseMessage,options);

            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private bool ConnectionExists()
        {
            if(_connection != null) return true;
            CreateConnection();
            return _connection !=null;
        }


    }
}