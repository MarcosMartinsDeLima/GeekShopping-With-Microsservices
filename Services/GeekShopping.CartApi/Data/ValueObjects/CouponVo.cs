namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CouponVo
    {
        public long Id {get; set;}

        public string CouponCode {get;set;}
        public decimal DiscountAmout {get;set;}
    }
}