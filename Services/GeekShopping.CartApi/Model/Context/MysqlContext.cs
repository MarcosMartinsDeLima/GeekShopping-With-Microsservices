using GeekShopping.CartApi.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Services.CartApi.Model.Context
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<Product> Products {get;set;}
        public DbSet<CartDetail> CartDetails {get;set;}
        public DbSet<CartHeader> CartHeaders {get;set;}
    }
}