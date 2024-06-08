using GeekShopping.MessageBus;

namespace GeekShopping.PaymentApi.RabbitMQSender
{
    public interface IRabbitMQMessageSender{

        void SendMessage(BaseMessage baseMessage);
    }
}