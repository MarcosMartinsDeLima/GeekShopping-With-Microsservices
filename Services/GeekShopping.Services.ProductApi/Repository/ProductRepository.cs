using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekShopping.Services.ProductApi.Data.ValueObject;
using GeekShopping.Services.ProductApi.Model;
using GeekShopping.Services.ProductApi.Model.Context;
using GeekShopping.Services.ProductApi.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Services.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MysqlContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(IMapper mapper, MysqlContext context){
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync(); 
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long Id)
        {
            Product product = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long Id)
        {
            try
            {
                Product product = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
                if(product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
    }
}