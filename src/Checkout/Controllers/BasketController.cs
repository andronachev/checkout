using Checkout.Core.Aggregates.Basket.Read;
using Checkout.Core.Aggregates.Basket.Services;
using Checkout.Infrastructure.Common.Data;
using Checkout.Infrastructure.Common.Data.Entities;
using Checkout.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Controllers
{
    [ApiController]
    [Route("/[action]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;

        private readonly IBasketService _basketService;
        private readonly CheckoutDbContext _dbContext;

        public BasketController(IBasketService basketService, CheckoutDbContext dbContext, ILogger<BasketController> logger)
        {
            _logger = logger;
            _basketService = basketService;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("/baskets")]
        public async Task<int> Baskets(BasketDto basket)
        {
            int basketId = await _basketService.CreateBasket(basket.Customer, basket.PaysVat);
            return basketId;
        }

        [HttpGet]
        [Route("/baskets")]
        public async Task<Basket[]> GetBaskets()
        {
            var baskets = await _dbContext.Baskets.ToArrayAsync();
            return baskets;
        }

        [HttpGet]
        [Route("/baskets/{id}")]
        public async Task<BasketSummary> GetBasket(int id)
        {
            var basketSummary = await _basketService.GetBasketSummary(id);
            return basketSummary;
        }

        [HttpPost]
        [Route("/baskets/{id}/article-line")]
        public async Task ArticleLine(int id, [FromBody] ArticleDto article)
        {
            await _basketService.AddArticleLine(id, article.Article, article.Price);
        }


        [HttpPost]
        [Route("/baskets/{id}")]
        public async Task UpdateStatus(int id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            await _basketService.UpdateStatus(id, updateStatusDto.Status);
        }
    }
}