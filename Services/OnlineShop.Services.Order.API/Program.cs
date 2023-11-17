using OnlineShop.Services.Order.API.Extensions;
using OnlineShop.Services.Order.API.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureBusinessLayerServices();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();

var app = builder.Build();

app.UseGlobalErrorHandler();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
