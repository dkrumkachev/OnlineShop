using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Interfaces
{
	public interface IOrderService
	{
		Task<ResponseDto> GetOrdersAsync();

		Task<ResponseDto> GetOrderByIdAsync(int id);

		Task<ResponseDto> GetOrdersByStatusAsync(OrderStatus status);

		Task<ResponseDto> GetOrdersByUserAsync(string userId);

		Task<ResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto);

		Task<ResponseDto> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);

		Task<ResponseDto> DeleteOrderAsync(int id);
	}
}
