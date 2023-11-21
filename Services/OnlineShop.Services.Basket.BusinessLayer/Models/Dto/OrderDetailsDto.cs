namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class OrderDetailsDto
    {
        public string UserId { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string? Comment { get; set; }

    }
}
