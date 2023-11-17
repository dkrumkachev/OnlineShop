using AutoMapper;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataAccessLayer.Models;
using System.Text.Json;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OrderDM, OrderDto>()
				.ForMember(dto => dto.Products, opt =>
					opt.MapFrom(order => DeserializeProducts(order.Products)));

			CreateMap<OrderCreateDto, OrderDM>()
				.ForMember(order => order.Products, opt =>
					opt.MapFrom(dto => SerializeProducts(dto.ProductIds)))
				.AfterMap((dto, order) => order.OrderNumber = order.GenerateOrderNumber());

			CreateMap<OrderUpdateDto, OrderDM>();
		}

		private static string SerializeProducts(IEnumerable<string> productIds)
			=> JsonSerializer.Serialize(productIds);

		private static IEnumerable<string>? DeserializeProducts(string products)
			=> JsonSerializer.Deserialize<IEnumerable<string>>(products);

	}
}
