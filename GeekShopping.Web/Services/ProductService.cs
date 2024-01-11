using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Iservices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/product";

        public ProductService(HttpClient client){
            _client = client ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAs< List <ProductModel>> ();
        }

        public async Task<ProductModel> FindById(long Id)
        {
            var response = await _client.GetAsync($"{basePath}/{Id}");
            return await response.ReadContentAs<ProductModel> ();
        }

        public async Task<ProductModel> Create(ProductModel product)
        {
            var response = await _client.PostAsJson(basePath,product);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel> ();
            else
                throw new Exception($"Something went wrong : {response.ReasonPhrase}");
        }

        public async Task<bool> Delete(long Id)
        {
            var response = await _client.DeleteAsync($"{basePath}/{Id}");
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool> ();
            else
                throw new Exception($"Something went wrong : {response.ReasonPhrase}");
        }

        public async Task<ProductModel> Update(ProductModel product)
        {
            
            var response = await _client.PutAsJson(basePath,product);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel> ();
            else
                throw new Exception($"Something went wrong : {response.ReasonPhrase}");
        }
    }
}