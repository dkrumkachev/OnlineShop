using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasketAsync(string userId, CancellationToken cancellationToken = default);
        Task<ResponseDto<BasketDto>> UpdateBasketAsync(UpdateBasketDto basketDto, CancellationToken cancellationToken = default);
        Task<ResponseDto<object>> DeleteBasketAsync(string userId, CancellationToken cancellationToken = default);
    }
}
