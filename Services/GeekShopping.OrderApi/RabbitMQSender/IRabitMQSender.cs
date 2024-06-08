using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public interface IRabbitMQMessageSender{

        void SendMessage(BaseMessage baseMessage,string queueName);
    }
}