using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Iservices
{
    public interface IProductService
    {
        Task <IEnumerable<ProductModel>> FindAll();
        Task <ProductModel> FindById(long Id);
        Task <ProductModel> Create(ProductModel product);
        Task <ProductModel> Update(ProductModel product);
        Task <bool> Delete(long Id);      
    }
}