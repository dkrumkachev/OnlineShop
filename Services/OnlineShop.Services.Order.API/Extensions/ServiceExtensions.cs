using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Mapper;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators;
using OnlineShop.Services.Order.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Order.DataAccessLayer.Data;
using OnlineShop.Services.Order.DataAccessLayer.Repositories.Implementations;
using OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace OnlineShop.Services.Order.API.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<OrderContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		}

		public static void ConfigureRepositories(this IServiceCollection services)
		{
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		public static void ConfigureBusinessLayerServices(this IServiceCollection services)
		{
			services.AddScoped<IOrderService, OrderService>();
		}

		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
		}

		public static void ConfigureFluentValidation(this IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(typeof(OrderCreateDtoValidator).Assembly);
		}
	}
}
