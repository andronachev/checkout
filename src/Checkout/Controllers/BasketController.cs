using Checkout.Core.Aggregates.Basket.Services;
using Checkout.Model;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
    [ApiController]
    [Route("/[action]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;

        private readonly IBasketService _basketService; 
        
        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        [Route("/baskets")]
        public async Task<Guid> Baskets(BasketDto basket)
        {
            Guid basketId = await _basketService.CreateBasket(basket.Customer, basket.PaysVat);
            return basketId;
        }

        [HttpPost]
        [Route("/baskets/{id}/article-line")]
        public async Task ArticleLine(Guid id, [FromBody] ArticleDto article)
        {
            await _basketService.AddArticleLine(id, article.Article, article.Price);
        }


        [HttpPost]
        [Route("/baskets/{id}")]
        public async Task UpdateStatus(Guid id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            await _basketService.UpdateStatus(id, updateStatusDto.Status);
        }
    }
}