using System.Text.Json;

namespace OnlineShop.Services.Order.BusinessLayer.Models.Dto
{
	public class ResponseDto
	{
		public object? Result { get; set; }

		public bool IsSuccess { get; set; } = true;

		public string Message { get; set; } = "";

		public override string ToString() => JsonSerializer.Serialize(this);
	}
}
