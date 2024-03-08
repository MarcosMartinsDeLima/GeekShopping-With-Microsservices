using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.Services.CartApi.Model;
using GeekShopping.Services.CartApi.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GeekShopping.CartApi.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId {get;set;}

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader {get;set;}

        public long ProductId {get;set;}

        [ForeignKey("ProductId")]
        public virtual Product Product {get;set;}

        [Column("count")]

        public int Count {get;set;}
    }
}