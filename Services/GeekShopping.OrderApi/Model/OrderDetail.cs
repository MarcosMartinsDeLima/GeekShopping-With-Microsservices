using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.OrderApi.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GeekShopping.OrderApi.Model
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        public long OrderHeaderId {get;set;}

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader {get;set;}

        [Column("ProductId")]
        public long ProductId {get;set;}


        [Column("count")]

        public int Count {get;set;}

        [Column("product_name")]
        public string ProductName {get;set;}

        [Column("price")]
        public decimal Price {get;set;}
    }
}