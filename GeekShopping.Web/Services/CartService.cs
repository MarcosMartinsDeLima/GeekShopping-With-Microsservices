using System.Net.Http.Headers;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Iservices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/Cart";

        public CartService(HttpClient client){
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.GetAsync($"{basePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel> ();
        }
        public async Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.PostAsJson($"{basePath}/add-cart",cart);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel> ();
            else
                throw new Exception("Something went wrong when calling API");
        }

        public Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Chechout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }


        public Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
             _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.DeleteAsync($"{basePath}/remove-cart/{cartId}");
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool> ();
            else
                throw new Exception($"Something went wrong : {response.ReasonPhrase}");
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.PutAsJson($"{basePath}/update-cart",cart);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel> ();
            else
                throw new Exception($"Something went wrong : {response.ReasonPhrase}");
 
        }
    }
}