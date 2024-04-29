using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Models.Context
{

    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<Coupon> Coupons {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "ERUDIO_2022_10",
                DiscountAmout = 10
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "ERUDIO_2022_15",
                DiscountAmout = 15
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon{
                Id = 3,
                CouponCode = "marcos",
                DiscountAmout = 25
            });
        }
    }
    }
