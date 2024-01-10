using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Services.ProductApi.Data.ValueObject
{
    public class ProductVO
    {
        public long Id {get; set;}
        public string Name {get;set;}
        public decimal Price {get;set;}


        public string Description {get;set;}

        
        public string Category_name {get;set;}
        public string Image_url {get;set;}
    }
}