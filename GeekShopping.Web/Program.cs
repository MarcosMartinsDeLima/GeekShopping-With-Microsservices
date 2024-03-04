using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Iservices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);


//dependency injection
builder.Services.AddHttpClient<IProductService,ProductService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})  .AddCookie("Cookies",c => {
    c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddOpenIdConnect("oidc",options => {
        options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "geek_shopping";
        options.ClientSecret = "mysecret";
        options.ResponseType = "code";
        options.ClaimActions.MapJsonKey("role","role","role");
        options.ClaimActions.MapJsonKey("sub","sub","sub");
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("geek_shopping");
        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();  

app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();

app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
