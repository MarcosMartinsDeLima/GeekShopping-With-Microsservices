using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Email.Model.Base

{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public long Id {get; set;}
    }
}