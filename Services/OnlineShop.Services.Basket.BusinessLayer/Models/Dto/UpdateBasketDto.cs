namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class UpdateBasketDto
    {
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
