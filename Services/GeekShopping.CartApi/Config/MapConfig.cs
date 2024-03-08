using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;
using GeekShopping.Services.CartApi.Model;


namespace GeekShopping.Services.CartApi.Config
{
    public class MapConfig
    {
        public static MapperConfiguration RegisterMapping(){
            var mappconfig = new MapperConfiguration(config => {
                config.CreateMap<ProductVo,Product>().ReverseMap();
                config.CreateMap<CartHeaderVo,CartHeader>().ReverseMap();
                config.CreateMap<CartDetailVo,CartDetail>().ReverseMap();
                config.CreateMap<CartVo,Cart>().ReverseMap();
                });
            return mappconfig;
        }
    }
}