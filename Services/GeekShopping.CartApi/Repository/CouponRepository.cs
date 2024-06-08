using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CouponApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository
{
    public class CouponRepository : ICouponRepository   
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client){
            _client = client;
        }

        public async Task<CouponVo> GetCouponByCouponCode(string couponCode,string token)
        {
            // "api/v1/Coupon";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.GetAsync($"/api/v1/coupon/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();

            if(response.StatusCode != HttpStatusCode.OK) return new CouponVo();
            return JsonSerializer.Deserialize<CouponVo>(content, new JsonSerializerOptions
            {PropertyNameCaseInsensitive = true});

        }
    }
}