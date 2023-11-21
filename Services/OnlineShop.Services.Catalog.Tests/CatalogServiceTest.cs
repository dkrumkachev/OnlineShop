using AutoMapper;
using Moq;
using OnlineShop.Services.Catalog.Application.Exceptions;
using OnlineShop.Services.Catalog.Application.Mapper;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Implementations;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;

namespace OnlineShop.Services.Catalog.Tests
{
	public class CatalogServiceTest
	{
		private readonly IMapper mapper;

		public CatalogServiceTest()
		{
			var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			mapper = mapperConfig.CreateMapper();
		}
		private static IEnumerable<Product> CreateProductList(int minElements = 0, int maxElements = 100)
		{
			var random = new Random();
			var size = random.Next(minElements, maxElements);
			var list = new List<Product>(size);
			for (var i = 0; i < size; i++)
			{
				list.Add(new Product());
			}
			return list;
		}

		[Fact]
		public async Task GetAllProductsAsync_ReturnsResponseDtoWithProductDtos()
		{
			var products = CreateProductList();
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(products);
			var catalogService = new CatalogService(mock.Object, mapper);

			var actual = await catalogService.GetAllProductsAsync(CancellationToken.None);

			var expected = new ResponseDto<IEnumerable<ProductDto>>()
			{
				Result = mapper.Map<IEnumerable<ProductDto>>(products)
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task GetProductAsync_IdOfExistingProduct_ReturnsResponseDtoWithProductDto()
		{
			var product = new Product();
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(product);
			var catalogService = new CatalogService(mock.Object, mapper);

			var actual = await catalogService.GetProductAsync("", CancellationToken.None);

			var expected = new ResponseDto<ProductDto>()
			{
				Result = mapper.Map<ProductDto>(product)
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task GetProductAsync_IdOfNonexistentProduct_ThrowsProductNotFoundException()
		{
			var mock = new Mock<IProductRepository>();
			var catalogService = new CatalogService(mock.Object, mapper);

			var act = () => catalogService.GetProductAsync("", CancellationToken.None);

			await Assert.ThrowsAsync<ProductNotFoundException>(act);
		}

		[Fact]
		public async Task AddProductAsync_NewProductDto_ReturnsResponseDtoWithProductId()
		{
			const string newProductId = "asd";
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(newProductId);
			var catalogService = new CatalogService(mock.Object, mapper);

			var actual = await catalogService.AddProductAsync(new NewProductDto(), CancellationToken.None);

			var expected = new ResponseDto<string>()
			{
				Message = "Product added successfully",
				Result = newProductId
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task UpdateProductAsync_IdOfExistingProduct_ReturnsResponseDtoWithSuccessMessage()
		{
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new Product());
			var catalogService = new CatalogService(mock.Object, mapper);

			var actual = await catalogService.UpdateProductAsync("", new NewProductDto(), CancellationToken.None);

			var expected = new ResponseDto<object>()
			{
				Message = "Product updated successfully"
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task UpdateProductAsync_IdOfNonexistentProduct_ThrowsProductNotFoundException()
		{
			var mock = new Mock<IProductRepository>();
			var catalogService = new CatalogService(mock.Object, mapper);

			var act = () => catalogService.UpdateProductAsync("", new NewProductDto(), CancellationToken.None);

			await Assert.ThrowsAsync<ProductNotFoundException>(act);
		}

		[Fact]
		public async Task DeleteProductAsync_IdOfExistingProduct_ReturnsResponseDtoWithSuccessMessage()
		{
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);
			var catalogService = new CatalogService(mock.Object, mapper);

			var actual = await catalogService.DeleteProductAsync("", CancellationToken.None);

			var expected = new ResponseDto<object>()
			{
				Message = "Product deleted successfully"
			};
			Assert.Equivalent(expected, actual);
		}

		[Fact]
		public async Task DeleteProductAsync_IdOfNonexistentProduct_ThrowsProductNotFoundException()
		{
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);
			var catalogService = new CatalogService(mock.Object, mapper);

			var act = () => catalogService.DeleteProductAsync("", CancellationToken.None);

			await Assert.ThrowsAsync<ProductNotFoundException>(act);
		}

	}
}