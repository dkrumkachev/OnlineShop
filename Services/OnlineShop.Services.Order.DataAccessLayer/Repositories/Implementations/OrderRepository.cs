using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.DataAccessLayer.Data;
using OnlineShop.Services.Order.DataAccessLayer.Models;
using OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.DataAccessLayer.Repositories.Implementations
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderContext context;

		public OrderRepository(OrderContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Models.OrderModel>> GetOrdersAsync()
			=> await context.Orders.ToListAsync();

		public async Task<Models.OrderModel?> GetOrderByIdAsync(int id)
			=> await context.Orders.FindAsync(id);

		public async Task<IEnumerable<Models.OrderModel>> GetOrdersByUserAsync(string userId)
			=> await context.Orders.Where(order => order.UserId == userId).ToListAsync();

		public async Task<IEnumerable<Models.OrderModel>> GetOrdersByStatusAsync(OrderStatus status)
			=> await context.Orders.Where(order => order.Status == status).ToListAsync();

		public async Task CreateOrderAsync(Models.OrderModel order)
		{
			await context.Orders.AddAsync(order);
			await context.SaveChangesAsync();
		}

		public async Task UpdateOrderAsync(Models.OrderModel order)
		{
			context.Orders.Update(order);
			await context.SaveChangesAsync();
		}

		public async Task DeleteOrderAsync(Models.OrderModel order)
		{
			context.Orders.Remove(order);
			await context.SaveChangesAsync();
		}
	}
}
