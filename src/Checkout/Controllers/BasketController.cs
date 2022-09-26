using Checkout.Core.Aggregates.Basket.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;

        private readonly IBasketService _basketService; 
        
        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpGet(Name = "GetBaskets")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
            .ToArray();
        }
    }
}