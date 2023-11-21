using OnlineShop.Services.Order.DataAccessLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Models.Dto
{
	public class OrderCreatedDto
	{
        public int Id { get; set; }

        public string UserId { get; set; }

		public IEnumerable<string> Products { get; set; }

		public string OrderNumber { get; set; }

		public decimal Total { get; set; }

		public DateTime Timestamp { get; set; }

		public OrderStatus Status { get; set; }

		public DateOnly EstimatedDeliveryDate { get; set; }

		public DateOnly? ActualDeliveryDate { get; set; }

		public string DeliveryAddress { get; set; }

		public string PhoneNumber { get; set; }

		public string Comment { get; set; }
	}
}
