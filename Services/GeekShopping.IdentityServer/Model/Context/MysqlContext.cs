using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServer.Model.Context
{
    public class MysqlContext : IdentityDbContext<ApplicationUser>
    {
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
    }
}