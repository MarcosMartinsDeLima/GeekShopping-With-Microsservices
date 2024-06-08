using System.Text;
using System.Text.Json;
using GeekShopping.MessageBus;
using GeekShopping.OrderApi.Messages;
using RabbitMQ.Client;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public class RabbitMQSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }    

        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            //criar a conexão com rabbitMQ
            if(ConnectionExists())
            {
            using var channel = _connection.CreateModel();
            //declarar a fila
            channel.QueueDeclare(queue:queueName,false,false,false,arguments:null);

            //converter basemessage em um array de bytes
            byte[] body = GetMessageAsByteArray(baseMessage);

            //publicando a messagem
            channel.BasicPublish(exchange:"",routingKey:queueName,basicProperties:null,body:body);
        }
        }

        private byte[] GetMessageAsByteArray(BaseMessage baseMessage)
        {
            //esse options faz com que ele serialize todas as propriedades da classe e não apenas da classe mãe
            var options = new JsonSerializerOptions{
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<PaymentVo>((PaymentVo)baseMessage,options);

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