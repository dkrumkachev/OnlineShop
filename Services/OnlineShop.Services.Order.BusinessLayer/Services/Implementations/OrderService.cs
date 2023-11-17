using AutoMapper;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Order.DataAccessLayer.Models;
using OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Implementations
{
	public class OrderService(IOrderRepository orderRepository, IMapper mapper) : IOrderService
	{
		public async Task<ResponseDto> GetOrderByIdAsync(int id)
		{
			var order = await orderRepository.GetOrderByIdAsync(id)
				?? throw new EntityNotFoundException($"Order with id {id} does not exist.");
			return new ResponseDto
			{
				Result = mapper.Map<OrderDto>(order)
			};
		}

		public async Task<ResponseDto> GetOrdersAsync()
		{
			var orders = await orderRepository.GetOrdersAsync();
			return new ResponseDto
			{
				Result = mapper.Map<IEnumerable<OrderDto>>(orders)
			};
		}

		public async Task<ResponseDto> GetOrdersByStatusAsync(OrderStatus status)
		{
			var orders = await orderRepository.GetOrdersByStatusAsync(status);
			return new ResponseDto
			{
				Result = mapper.Map<IEnumerable<OrderDto>>(orders)
			};
		}

		public async Task<ResponseDto> GetOrdersByUserAsync(string userId)
		{
			var orders = await orderRepository.GetOrdersByUserAsync(userId);
			return new ResponseDto
			{
				Result = mapper.Map<IEnumerable<OrderDto>>(orders)
			};
		}

		public async Task<ResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
		{
			var order = mapper.Map<OrderDM>(orderCreateDto);
			await orderRepository.CreateOrderAsync(order);
			return new ResponseDto
			{
				Message = "Successfully created.",
				Result = mapper.Map<OrderDto>(order)
			};
		}

		public async Task<ResponseDto> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
		{
			var order = await orderRepository.GetOrderByIdAsync(id)
				?? throw new EntityNotFoundException($"Order with id {id} does not exist.");
			mapper.Map(orderUpdateDto, order);
			await orderRepository.UpdateOrderAsync(order);
			return new ResponseDto
			{
				Message = "Successfully updated.",
				Result = mapper.Map<OrderDto>(order)
			};
		}

		public async Task<ResponseDto> DeleteOrderAsync(int id)
		{
			var order = await orderRepository.GetOrderByIdAsync(id)
				?? throw new EntityNotFoundException($"Order with id {id} does not exist.");
			await orderRepository.DeleteOrderAsync(order);
			return new ResponseDto
			{
				Message = "Successfully deleted."
			};
		}
	}
}
