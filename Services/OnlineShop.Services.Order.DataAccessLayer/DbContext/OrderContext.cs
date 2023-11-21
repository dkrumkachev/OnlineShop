using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.DataAccessLayer.Data
{
	public class OrderContext(DbContextOptions<OrderContext> option) : DbContext(option)
	{
		public DbSet<Models.OrderModel> Orders { get; set; }

	}
}
