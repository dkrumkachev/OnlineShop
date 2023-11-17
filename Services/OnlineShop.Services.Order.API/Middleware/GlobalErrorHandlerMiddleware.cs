using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using System.Net;

namespace OnlineShop.Services.Order.API.Middleware
{
	public class GlobalErrorHandlerMiddleware
	{
		private readonly RequestDelegate next;

		public GlobalErrorHandlerMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				response.StatusCode = ex switch
				{
					EntityNotFoundException => (int)HttpStatusCode.NotFound,
					ValidationException => (int)HttpStatusCode.BadRequest,
					_ => (int)HttpStatusCode.InternalServerError,
				};
				var responseObject = new ResponseDto
				{
					IsSuccess = false,
					Message = ex.Message,
				};
				await response.WriteAsync(responseObject.ToString());
			}
		}
	}

	public static class GlobalErrorHandlerMiddlewareExtension
	{
		public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<GlobalErrorHandlerMiddleware>();
		}
	}
}
