namespace GeekShopping.OrderApi.Messages
{
    public class CartDetailVo
    {
        public long Id {get;set;}
        public long CartHeaderId {get;set;}
        public long ProductId {get;set;}
        public virtual  ProductVo Product {get;set;}
        public int Count {get;set;}
    }
}