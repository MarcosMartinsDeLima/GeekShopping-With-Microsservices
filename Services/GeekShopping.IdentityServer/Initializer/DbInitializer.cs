using System.Security.Claims;
using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MysqlContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MysqlContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role){
            _context = context;
            _user =user;
            _role = role;
        }
        public void Initialize()
        {   //cadastrando roles 
           if(_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null ) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            //cadastrando user admin
            ApplicationUser admin = new ApplicationUser(){
                UserName = "Marcos-admin",
                Email = "marcos@a",
                EmailConfirmed = true,
                PhoneNumber ="+55 (11) 12345-6789",
                FirstName = "Marcos",
                LastName = "admin"
                
            };
            _user.CreateAsync(admin,"@Marcos123").GetAwaiter().GetResult();

            //vinculando user para a role
            _user.AddToRoleAsync(admin,IdentityConfiguration.Admin).GetAwaiter().GetResult();

            //criando claims
            var adminClaims = _user.AddClaimsAsync(admin,new Claim[]{
                new Claim(JwtClaimTypes.Name,$"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName,admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName,admin.LastName),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Admin)
            }).Result;

                ApplicationUser client = new ApplicationUser(){
                UserName = "Marcos-client",
                Email = "marcos@a",
                EmailConfirmed = true,
                PhoneNumber ="+55 (11) 12345-6789",
                FirstName = "Marcos",
                LastName = "client"
                
            };
            _user.CreateAsync(client,"@Marcos123").GetAwaiter().GetResult();

            //vinculando user para a role
            _user.AddToRoleAsync(client,IdentityConfiguration.Client).GetAwaiter().GetResult();

            //criando claims
            var clientClaims = _user.AddClaimsAsync(client,new Claim[]{
                new Claim(JwtClaimTypes.Name,$"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName,client.FirstName),
                new Claim(JwtClaimTypes.FamilyName,client.LastName),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Client)
            }).Result;
        }
    }
}