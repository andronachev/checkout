using Checkout.Core.Aggregates;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Aggregates.Basket.Read;
using Checkout.Core.Aggregates.Basket.Services;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;

namespace Checkout.Business
{
    public class BasketService : ServiceBase, IBasketService
    {
        public BasketService(IEventStore eventStore, IEventPublisher eventPublisher): base(eventStore, eventPublisher)
        {
        }

        public async Task UpdateStatus(int basketId, string status)
        {
            var aggregate = await GetAggregate<BasketAggregate>(basketId);

            aggregate.UpdateStatus(status);
            
            await StoreAndPublish(aggregate);
        }

        public async Task AddArticleLine(int basketId, string article, int price)
        {
            var aggregate = await GetAggregate<BasketAggregate>(basketId);

            aggregate.AddArticleLine(article, price);

            await StoreAndPublish(aggregate);
        }

        public async Task<int> CreateBasket(string customer, bool paysVat)
        {
            var newAggregate = GetNewAggregate<BasketAggregate>();

            newAggregate.CreateBasket(customer, paysVat);

            await StoreAndPublish(newAggregate);

            return newAggregate.Id;
        }

        public async Task<BasketSummary> GetBasketSummary(int basketId)
        {
            var aggregate = await GetAggregate<BasketAggregate>(basketId);

            var summary = new BasketSummary();
            summary.Id = aggregate.Id;
            summary.Customer = aggregate.Customer;
            summary.PaysVAT = aggregate.PaysVat;
            summary.Status = aggregate.Status;
            summary.Articles = aggregate.Articles.Select(a => new BasketArticle() { Article = a.Article, Price = a.Price }).ToList();

            summary.TotalNet = aggregate.Articles.Sum(a => a.Price);
            summary.TotalGross = aggregate.PaysVat ? (summary.TotalNet * (decimal)1.1) : summary.TotalNet;

            return summary;
        }
    }
}