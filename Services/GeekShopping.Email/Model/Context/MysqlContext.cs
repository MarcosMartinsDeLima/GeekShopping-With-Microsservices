using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<EmailLog> Emails {get;set;}
    }
}