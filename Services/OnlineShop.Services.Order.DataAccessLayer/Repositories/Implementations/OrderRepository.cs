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

		public async Task<IEnumerable<OrderDM>> GetOrdersAsync()
			=> await context.Orders.ToListAsync();

		public async Task<OrderDM?> GetOrderByIdAsync(int id)
			=> await context.Orders.FindAsync(id);

		public async Task<IEnumerable<OrderDM>> GetOrdersByUserAsync(string userId)
			=> await context.Orders.Where(order => order.UserId == userId).ToListAsync();

		public async Task<IEnumerable<OrderDM>> GetOrdersByStatusAsync(OrderStatus status)
			=> await context.Orders.Where(order => order.Status == status).ToListAsync();

		public async Task CreateOrderAsync(OrderDM order)
		{
			await context.Orders.AddAsync(order);
			await context.SaveChangesAsync();
		}

		public async Task UpdateOrderAsync(OrderDM order)
		{
			context.Orders.Update(order);
			await context.SaveChangesAsync();
		}

		public async Task DeleteOrderAsync(OrderDM order)
		{
			context.Orders.Remove(order);
			await context.SaveChangesAsync();
		}
	}
}
