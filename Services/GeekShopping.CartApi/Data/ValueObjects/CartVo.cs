using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CartVo
    {
        public CartHeaderVo CartHeader {get;set;}
        public IEnumerable<CartDetailVo> CartDetails {get;set;}
    }
}