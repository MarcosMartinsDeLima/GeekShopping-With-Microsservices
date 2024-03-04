using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Iservices
{
    public interface IProductService
    {
        Task <IEnumerable<ProductModel>> FindAll(string token);
        Task <ProductModel> FindById(long Id,string token);
        Task <ProductModel> Create(ProductModel product,string token);
        Task <ProductModel> Update(ProductModel product,string token);
        Task <bool> Delete(long Id,string token);      
    }
}