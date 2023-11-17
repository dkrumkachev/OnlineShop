using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.API.Controllers
{
	[Route("api/orders")]
	[ApiController]
	public class OrderController(IOrderService orderService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var response = await orderService.GetOrdersAsync();
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			var response = await orderService.GetOrderByIdAsync(id);
			return Ok(response);
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetOrdersByUser(string userId)
		{
			var response = await orderService.GetOrdersByUserAsync(userId);
			return Ok(response);
		}

		[HttpGet("status/{status}")]
		public async Task<IActionResult> GetOrdersByStatus(OrderStatus status)
		{
			var response = await orderService.GetOrdersByStatusAsync(status);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
		{
			var response = await orderService.CreateOrderAsync(orderCreateDto);
			return Ok(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
		{
			var response = await orderService.UpdateOrderAsync(id, orderUpdateDto);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var response = await orderService.DeleteOrderAsync(id);
			return Ok(response);
		}
	}
}
