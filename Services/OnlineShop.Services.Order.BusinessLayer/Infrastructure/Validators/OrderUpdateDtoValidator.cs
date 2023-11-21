using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
	public class OrderUpdateDtoValidator : BaseValidator<OrderUpdateDto>
	{
		public OrderUpdateDtoValidator()
		{
			RuleFor(dto => dto.ActualDeliveryDate)
				.LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
				.When(dto => dto.ActualDeliveryDate != null)
				.WithMessage(ValidatorMessage.ActualDeliveryDateGreaterThanNow);
		}
	}
}
