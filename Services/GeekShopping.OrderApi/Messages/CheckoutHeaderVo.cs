using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.Messages
{
    public class CheckoutHeaderVo : BaseMessage
    {
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