namespace GeekShopping.OrderApi.Messages
{
    public class ProductVo
    {
        //o id não sera gerado automaticamente, ele espera que venha o id
        public long Id {get; set;}
        public string Name {get;set;}
        public decimal Price {get;set;}
        public string Description {get;set;}
        public string Category_name {get;set;}
        public string Image_url {get;set;}
    }
}