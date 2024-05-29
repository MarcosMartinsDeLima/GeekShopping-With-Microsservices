using GeekShopping.CartApi.Data.ValueObjects;

namespace GeekShopping.CartApi.Message
{
    public class CheckoutHeaderVO
    {
        public long Id {get;set;}
        public string UserId {get;set;}
        public string CouponCode {get;set;}
        public decimal PurchaseAmout {get;set;}
        public decimal DiscountTotal {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public DateTime DateTime {get;set;}
        public string Phone {get;set;}
        public string Email {get;set;}
        public string CardNumber {get;set;}
        public string CVV {get;set;}
        public string ExpiryMonthYear {get;set;}
        public int CartTotalItens {get;set;}
        public IEnumerable<CartDetailVo> CartDetails {get;set;}
    }
}