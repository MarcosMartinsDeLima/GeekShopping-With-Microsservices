using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Services.ProductApi.Model.Base;
using Microsoft.AspNetCore.SignalR;

namespace GeekShopping.Services.ProductApi.Model
{
    [Table("Product")]
    public class Product : BaseEntity
    {
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