using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartApi.Model
{
    public class Cart
    {
        public CartHeader CartHeader {get;set;}

        public IEnumerable<CartDetail> CartDetails {get;set;}
    }
}