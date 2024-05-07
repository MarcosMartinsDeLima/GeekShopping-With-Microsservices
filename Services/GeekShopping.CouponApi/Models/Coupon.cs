using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponApi.Models
{

    [Table("Coupon")]
    public class Coupon 
    {
        //o id não sera gerado automaticamente, ele espera que venha o id
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id {get; set;}

        [Column("CouponCode")]
        [Required]
        [StringLength(30)]
        public string CouponCode {get;set;}

        [Column("DiscountAmout")]
        [Required]
        public decimal DiscountAmout {get;set;}

       
    }
}