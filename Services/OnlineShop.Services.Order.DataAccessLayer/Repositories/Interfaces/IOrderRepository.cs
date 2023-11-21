using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Models.OrderModel>> GetOrdersAsync();

		Task<Models.OrderModel?> GetOrderByIdAsync(int id);

		Task<IEnumerable<Models.OrderModel>> GetOrdersByUserAsync(string userId);

		Task<IEnumerable<Models.OrderModel>> GetOrdersByStatusAsync(OrderStatus status);

		Task CreateOrderAsync(Models.OrderModel order);

		Task UpdateOrderAsync(Models.OrderModel order);

		Task DeleteOrderAsync(Models.OrderModel order);
	}
}
