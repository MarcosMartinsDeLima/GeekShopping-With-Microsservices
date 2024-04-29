using AutoMapper;
using GeekShopping.CouponApi.Data.ValueObjects;
using GeekShopping.CouponApi.Models;

namespace GeekShopping.CouponApi.Config
{
    public class MapConfig
    {
        public static MapperConfiguration RegisterMapping(){
            var mappconfig = new MapperConfiguration(config => {
                config.CreateMap<CouponVo,Coupon>().ReverseMap();
            });
            return mappconfig;
        }
    }
}