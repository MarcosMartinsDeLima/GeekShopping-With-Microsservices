using AutoMapper;
using GeekShopping.CouponApi.Data.ValueObjects;
using GeekShopping.CouponApi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Repository
{
    public class CouponRepository : ICouponRepository   
    {
        private readonly MysqlContext _mysqlContext;
        private IMapper _mapper;

        public CouponRepository(MysqlContext mysqlContext,IMapper mapper){
            _mysqlContext = mysqlContext;
            _mapper = mapper;
        }

        public async Task<CouponVo> GetCouponByCouponCode(string couponCode)
        {
            var Coupon = await _mysqlContext.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponVo>(Coupon);
        }
    }
}