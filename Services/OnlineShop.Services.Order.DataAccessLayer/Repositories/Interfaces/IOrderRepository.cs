using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		Task<IEnumerable<OrderDM>> GetOrdersAsync();

		Task<OrderDM?> GetOrderByIdAsync(int id);

		Task<IEnumerable<OrderDM>> GetOrdersByUserAsync(string userId);

		Task<IEnumerable<OrderDM>> GetOrdersByStatusAsync(OrderStatus status);

		Task CreateOrderAsync(OrderDM order);

		Task UpdateOrderAsync(OrderDM order);

		Task DeleteOrderAsync(OrderDM order);
	}
}
