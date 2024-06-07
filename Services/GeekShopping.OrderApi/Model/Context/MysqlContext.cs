using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Model.Context
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<OrderDetail> Details {get;set;}
        public DbSet<OrderHeader> Headers {get;set;}
    }
}