using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekShopping.Services.ProductApi.Data.ValueObject;
using GeekShopping.Services.ProductApi.Model;

namespace GeekShopping.Services.ProductApi.Config
{
    public class MapConfig
    {
        public static MapperConfiguration RegisterMapping(){
            var mappconfig = new MapperConfiguration(config => {
                config.CreateMap<ProductVO,Product>();
                config.CreateMap<Product,ProductVO>();});
            return mappconfig;
        }
    }
}