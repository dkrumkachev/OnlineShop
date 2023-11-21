using AutoMapper;
using Moq;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Mapper;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Order.DataAccessLayer.Models;
using OnlineShop.Services.Order.DataAccessLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.Tests
{
	public class OrderServiceTest
	{
		private readonly IMapper mapper;
		private static OrderModel TestOrder => new()
		{
			UserId = "1",
			Products = "[]",
			DeliveryAddress = string.Empty,
			PhoneNumber = string.Empty,
			OrderNumber = string.Empty,
		};

		private static OrderCreateDto TestOrderCreateDTO => new()
		{
			UserId = "1",
			ProductIds = new List<string>(),
			DeliveryAddress = string.Empty,
			PhoneNumber = string.Empty,
		};

		private static OrderUpdateDto TestOrderUpdateDto => new();

		private static List<OrderModel> CreateOrderList(int minElements = 0, int maxElements = 100)
		{
			var random = new Random();
			var size = random.Next(minElements, maxElements);
			var list = new List<OrderModel>(size);
			for (var i = 0; i < size; i++)
			{
				list.Add(TestOrder);
			}
			return list;
		}

		public OrderServiceTest()
		{
			var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			mapper = mapperConfig.CreateMapper();
		}

		[Fact]
		public async Task GetOrderById_ValidOrderId_ReturnResponseDtoPopulatedWithOrderDto()
		{
			var order = TestOrder;
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrderByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(order);
			var orderService = new OrderService(mock.Object, mapper);

			var actual = await orderService.GetOrderByIdAsync(order.Id);


			var expected = new ResponseDto()
			{
				IsSuccess = true,
				Result = mapper.Map<OrderDto>(order)
			};
			Assert.Equivalent(expected, actual, strict: true);
		}

		[Fact]
		public async Task GetOrderById_InvalidOrderId_ThrowsEntityNotFoundException()
		{
			var order = TestOrder;
			var mock = new Mock<IOrderRepository>();
			var orderService = new OrderService(mock.Object, mapper);

			var act = () => orderService.GetOrderByIdAsync(default);

			await Assert.ThrowsAsync<EntityNotFoundException>(act);
		}

		[Fact]
		public async Task GetOrders_ListOfOrders_ReturnResponseDtoPopulatedWithOrderDtos()
		{
			var testOrders = CreateOrderList();
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrdersAsync())
				.ReturnsAsync(testOrders);
			var orderService = new OrderService(mock.Object, mapper);

			var act = await orderService.GetOrdersAsync();

			var expected = new ResponseDto()
			{
				IsSuccess = true,
				Result = mapper.Map<List<OrderDto>>(testOrders)
			};
			Assert.Equivalent(expected, act, strict: true);
		}

		[Theory]
		[InlineData(OrderStatus.Pending)]
		[InlineData(OrderStatus.Cancelled)]
		[InlineData(OrderStatus.Shipped)]
		[InlineData(OrderStatus.Delivered)]
		public async Task GetOrdersByStatusAsync_OrderStatus_ReturnResponseDtoWithOrderDtosWithGivenStatus(OrderStatus status)
		{
			var testOrders = CreateOrderList();
			testOrders.ForEach(order => order.Status = status);
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrdersByStatusAsync(status))
				.ReturnsAsync(testOrders);
			var orderService = new OrderService(mock.Object, mapper);

			var actual = await orderService.GetOrdersByStatusAsync(status);

			var expected = new ResponseDto()
			{
				IsSuccess = true,
				Result = mapper.Map<List<OrderDto>>(testOrders)
			};
			Assert.Equivalent(expected, actual, strict: true);
		}

		[Fact]
		public async Task GetOrdersByUserAsync_UserId_ReturnResponseDtoWithOrderDtosOfUserWithGivenId()
		{
			string userId = TestOrder.UserId;
			var testOrders = CreateOrderList();
			testOrders.ForEach(order => order.UserId = userId);
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrdersByUserAsync(userId))
				.ReturnsAsync(testOrders);
			var orderService = new OrderService(mock.Object, mapper);

			var actual = await orderService.GetOrdersByUserAsync(userId);

			var expected = new ResponseDto()
			{
				IsSuccess = true,
				Result = mapper.Map<List<OrderDto>>(testOrders)
			};
			Assert.Equivalent(expected, actual, strict: true);
		}

		[Fact]
		public async Task CreateOrderAsync_OrderCreateDto_ReturnResponseDtoWithCreatedOrderDto()
		{
			var mock = new Mock<IOrderRepository>();
			var orderService = new OrderService(mock.Object, mapper);
			var orderCreateDto = TestOrderCreateDTO;

			var actual = await orderService.CreateOrderAsync(orderCreateDto);

			var expectedResultObject = mapper.Map<OrderDto>(mapper.Map<OrderModel>(orderCreateDto));
			Assert.IsType<OrderDto>(actual.Result);
			expectedResultObject.Timestamp = ((OrderDto)actual.Result).Timestamp;
			var expected = new ResponseDto()
			{
				IsSuccess = true,
				Message = "Successfully created.",
				Result = expectedResultObject
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task UpdateOrderAsync_ValidId_ReturnResponseDtoWithUpdatedOrderDto()
		{
			var order = TestOrder;
			var orderUpdateDto = TestOrderUpdateDto;
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrderByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(order);
			var orderService = new OrderService(mock.Object, mapper);

			var actual = await orderService.UpdateOrderAsync(order.Id, orderUpdateDto);

			var expected = new ResponseDto
			{
				IsSuccess = true,
				Message = "Successfully updated.",
				Result = mapper.Map<OrderDto>(mapper.Map(orderUpdateDto, order))
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task UpdateOrderAsync_InvalidOrderId_ThrowsEntityNotFoundException()
		{
			var mock = new Mock<IOrderRepository>();
			var orderService = new OrderService(mock.Object, mapper);

			var act = () => orderService.UpdateOrderAsync(default, TestOrderUpdateDto);

			await Assert.ThrowsAsync<EntityNotFoundException>(act);
		}

		[Fact]
		public async Task DeleteOrderAsync_ValidOrderId_ReturnResponseDtoWithMessage()
		{
			var order = TestOrder;
			var mock = new Mock<IOrderRepository>();
			mock.Setup(repository => repository.GetOrderByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(order);
			var orderService = new OrderService(mock.Object, mapper);

			var actual = await orderService.DeleteOrderAsync(order.Id);

			var expected = new ResponseDto
			{
				IsSuccess = true,
				Message = "Successfully deleted."
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task DeleteOrderAsync_InvalidOrderId_ThrowsEntityNotFoundException()
		{
			var mock = new Mock<IOrderRepository>();
			var orderService = new OrderService(mock.Object, mapper);

			var act = () => orderService.DeleteOrderAsync(default);

			await Assert.ThrowsAsync<EntityNotFoundException>(act);
		}

	}
}