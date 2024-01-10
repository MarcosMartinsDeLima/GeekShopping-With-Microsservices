using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Services.ProductApi.Data.ValueObject;

namespace GeekShopping.Services.ProductApi.Model.Repository
{
    public interface IProductRepository
    {
        Task <IEnumerable<ProductVO>> FindAll();
        Task <ProductVO> FindById(long Id);
        Task <ProductVO> Create(ProductVO vo);
        Task <ProductVO> Update(ProductVO vo);
        Task <bool> Delete(long Id);

    }
}