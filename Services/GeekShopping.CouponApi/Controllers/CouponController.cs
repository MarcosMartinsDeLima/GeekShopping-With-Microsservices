using GeekShopping.CouponApi.Data.ValueObjects;
using GeekShopping.CouponApi.Models.Context;
using GeekShopping.CouponApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponApi.Controllers;

[ApiController]
[Route("ap1/v1/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;
    private readonly MysqlContext _mysqlContext;

    public CouponController(ICouponRepository couponRepository,MysqlContext mysqlContext)
    {
        _couponRepository = couponRepository;
        _mysqlContext = mysqlContext;
    }

    [HttpGet("{couponCode}")]
    [Authorize]
    public async Task<ActionResult<CouponVo>> GetCo(string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCouponCode(couponCode);
        if (coupon == null) return NotFound();
        return Ok(coupon);
        }
}
