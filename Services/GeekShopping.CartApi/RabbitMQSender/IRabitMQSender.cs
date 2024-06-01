using GeekShopping.MessageBus;

namespace GeekShopping.CartApi.RabbitMQSender
{
    public interface IRabbitMQMessageSender{

        void SendMessage(BaseMessage baseMessage,string queueName);
    }
}