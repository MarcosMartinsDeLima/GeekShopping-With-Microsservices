namespace GeekShopping.Email.Messages
{
    public class UpdatePaymentResulMessage
    {
        public long OrderId { get; set; }
        public bool Status{ get; set; }
        public string Email{ get; set; }

    }
}