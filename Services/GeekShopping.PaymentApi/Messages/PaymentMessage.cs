using GeekShopping.MessageBus;

namespace GeekShopping.PaymentApi.Messages
{
    public class PaymentMessage : BaseMessage
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public decimal PurchaseAmout { get; set; }
        public string Email{ get; set; }

    }
}