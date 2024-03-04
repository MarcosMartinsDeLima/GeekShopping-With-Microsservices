using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        //informações relacionadas a identidade do cliente ou identity resources
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Email(),
                    new IdentityResources.Profile()
                } ;
        //Api scope
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>{
                new ApiScope(name:"geek_shopping",displayName:"GeekShopping Server"),
                new ApiScope(name:"read",displayName:"Read data."),
                new ApiScope(name:"write",displayName:"Write data."),
                new ApiScope(name:"delete",displayName:"Delete data."),
            };
        //Clients
        public static IEnumerable<Client> Clients =>
            new List<Client> {
                // commun client
                new Client{
                    ClientId = "client",
                    ClientSecrets = {new Secret("mysecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read","write","profile"}
                },
                // client authorized
                new Client{
                    ClientId = "geek_shopping",
                    ClientSecrets = {new Secret("mysecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:4430/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc"},
                    AllowedScopes = new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "geek_shopping"
                        
                    }
                }
            };
    }
}