using AutoMapper;
using GeekShopping.Services.ProductApi.Config;
using GeekShopping.Services.ProductApi.Model.Context;
using GeekShopping.Services.ProductApi.Model.Repository;
using GeekShopping.Services.ProductApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration["MysqlConnection:MysqlConnectionString"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8,0,5))) );

//mapper
builder.Services.AddAutoMapper(typeof(Program));
IMapper mapper = MapConfig.RegisterMapping().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//injetando os repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
