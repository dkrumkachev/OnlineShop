using FluentValidation;
using FluentValidation.Results;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
	public class OrderCreateDtoValidator : BaseValidator<OrderCreateDto>
	{
		public OrderCreateDtoValidator()
		{
			RuleFor(dto => dto.UserId)
				.NotEmpty()
				.WithMessage(ValidatorMessage.EmptyUserId);
			RuleForEach(dto => dto.ProductIds)
				.NotEmpty()
				.WithMessage(ValidatorMessage.EmptyProductIds);
			RuleFor(dto => dto.Total)
				.GreaterThanOrEqualTo(0)
				.WithMessage(ValidatorMessage.TotalLessThanZero);
			RuleFor(dto => dto.PhoneNumber)
				.NotEmpty()
				.WithMessage(ValidatorMessage.EmptyPhoneNumber);
			RuleFor(dto => dto.DeliveryAddress)
				.NotEmpty()
				.WithMessage(ValidatorMessage.EmptyDeliveryAddress);
		}
	}
}
