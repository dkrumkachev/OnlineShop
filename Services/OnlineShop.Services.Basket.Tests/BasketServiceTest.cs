using AutoMapper;
using Moq;
using OnlineShop.Services.Basket.BusinessLayer.Mapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Basket.DataLayer.Models.Data;
using OnlineShop.Services.Basket.DataLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace OnlineShop.Services.Basket.Tests
{
	public class BasketServiceTest
	{
		private readonly IMapper mapper;

		public BasketServiceTest()
		{
			var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			mapper = mapperConfig.CreateMapper();
		}

		[Fact]
		public async Task GetBasketAsync_UserId_ReturnResponseDtoWithBasketDto()
		{
			var shoppingCart = new ShoppingCart();
			var mock = new Mock<IBasketRepository>();
			mock.Setup(repo => repo.GetBasketAsync(It.IsAny<string>(), default))
				.ReturnsAsync(shoppingCart);
			var basketService = new BasketService(mock.Object, mapper);

			var actual = await basketService.GetBasketAsync("");

			var expected = new ResponseDto<BasketDto>()
			{
				Result = mapper.Map<BasketDto>(shoppingCart)
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task UpdateBasketAsync_UpdateBasketDto_ReturnResponseDtoWithUpdatedBasketDto()
		{
			var mock = new Mock<IBasketRepository>();
			mock.Setup(repo => repo.UpdateBasketAsync(It.IsAny<ShoppingCart>(), default))
				.ReturnsAsync((ShoppingCart cart, CancellationToken _) => cart);
			var basketService = new BasketService(mock.Object, mapper);
			var updateDto = new UpdateBasketDto();

			var actual = await basketService.UpdateBasketAsync(updateDto);

			var expected = new ResponseDto<BasketDto>()
			{
				Result = mapper.Map<BasketDto>(mapper.Map<ShoppingCart>(updateDto)),
				Message = "Basket updated successfully"
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task DeleteBasketAsync_UserId_ReturnResponseDtoWithSuccessMessage()
		{
			var mock = new Mock<IBasketRepository>();
			var basketService = new BasketService(mock.Object, mapper);

			var actual = await basketService.DeleteBasketAsync("");

			var expected = new ResponseDto<object>()
			{
				Message = "Basket deleted successfully"
			};
			Assert.Equivalent(expected, actual);
		}
	}
}