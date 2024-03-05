using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GeekShopping.Web.Models
{
    public class ProductModel
    {
        public long Id {get; set;}
        public string Name {get;set;}
        public decimal Price {get;set;}


        public string Description {get;set;}

        
        public string Category_name {get;set;}
        public string Image_url {get;set;}

        [Range(1,100)]
        public int Count {get;set;} = 1;
        public string SubStringName(){
            if(Name.Length <24) return Name;
            
            return $"{Name.Substring(0,21)}...";
        }

         public string SubStringDescription(){
            if(Description.Length <355) return Description;
            
            return $"{Description.Substring(0,352)}...";
        }
    }
}