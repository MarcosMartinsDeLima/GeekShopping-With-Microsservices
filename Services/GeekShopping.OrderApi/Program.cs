using GeekShopping.OrderApi.MessageConsumer;
using GeekShopping.OrderApi.Model.Context;
using GeekShopping.OrderApi.RabbitMQSender;
using GeekShopping.OrderApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//add the database 

var connection = builder.Configuration["MysqlConnection:MysqlConnectionString"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8,0,5))) );


//builder context mysql
var dbContextBuilder = new DbContextOptionsBuilder<MysqlContext>();
dbContextBuilder.UseMySql(connection, new MySqlServerVersion(new Version(8,0,29)) );

builder.Services.AddSingleton(new OrderRepository(dbContextBuilder.Options));

builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();

builder.Services.AddHostedService<RabbitMQPaymentConsumer>(); 
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQSender>();

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer",options =>{
        options.Authority = "http://localhost:4436/";
        options.TokenValidationParameters = new TokenValidationParameters{
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "geek_shopping");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{ 
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.OrderAPI", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }}
    );
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekShopping.OrderAPI v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

