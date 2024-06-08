namespace GeekShopping.OrderApi.Messages
{
    public class UpdatePaymentResultVo
    {
        public long OrderId { get; set; }
        public bool Status{ get; set; }
        public string Email{ get; set; }

    }
}