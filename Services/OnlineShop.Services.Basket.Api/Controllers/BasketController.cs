using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;

namespace OnlineShop.Serivces.Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBasketAsync([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var response = await _basketService.GetBasketAsync(userId, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBasketAsync([FromBody] UpdateBasketDto basketDto, CancellationToken cancellationToken)
        {
            var response = await _basketService.UpdateBasketAsync(basketDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBasketAsync([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var response = await _basketService.DeleteBasketAsync(userId, cancellationToken);

            return Ok(response);
        }
    }
}
