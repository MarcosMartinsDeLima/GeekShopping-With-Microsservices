using GeekShopping.OrderApi.Model.Context;
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

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer",options =>{
        options.Authority = "http://localhost:4436/";
        options.TokenValidationParameters = new TokenValidationParameters{
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{ 
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


app.UseAuthentication();


app.MapControllers();

app.Run();

