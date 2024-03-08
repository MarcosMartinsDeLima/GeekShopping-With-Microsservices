using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Services.CartApi.Model
{
    [Table("Product")]
    public class Product 
    {
        //o id não sera gerado automaticamente, ele espera que venha o id
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id {get; set;}

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name {get;set;}

        [Column("price")]
        [Required]
        [Range(1,10000)]
        public decimal Price {get;set;}

        [Column("Description")]
        [StringLength(500)]
        public string Description {get;set;}

        [Column("category_name")]
        [StringLength(50)]
        public string Category_name {get;set;}

        [Column("image_url")]
        [StringLength(300)]
        public string Image_url {get;set;}
    }
}