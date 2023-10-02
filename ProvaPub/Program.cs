using Microsoft.EntityFrameworkCore;
using ProvaPub.Helpers;
using ProvaPub.Infra.Db;
using ProvaPub.Repository;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentMethod, PixPaymentRepository>();
builder.Services.AddScoped<IPaymentMethod, CreditPaymentRepository>();
builder.Services.AddScoped<IPaymentMethod, PaypalPaymentRepository>();
builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<RandomService>();
builder.Services.AddTransient<QueryManipulator>();
builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
