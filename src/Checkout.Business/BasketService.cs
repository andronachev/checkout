using Checkout.Core.Aggregates;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Aggregates.Basket.Read;
using Checkout.Core.Aggregates.Basket.Services;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;

namespace Checkout.Business
{
    public class BasketService : IBasketService
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _eventPublisher;
        public BasketService(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;   
        }

        public async Task UpdateStatus(int basketId, string status)
        {
            var allEvents = await _eventStore.GetAllEventsByAggregateId(basketId);

            var aggregate = new BasketAggregate(allEvents);

            aggregate.UpdateStatus(status);
            
            await StoreAndPublish(aggregate);
        }

        public async Task AddArticleLine(int basketId, string article, int price)
        {
            var allEvents = await _eventStore.GetAllEventsByAggregateId(basketId);

            var aggregate = new BasketAggregate(allEvents);

            aggregate.AddArticleLine(article, price);

            await StoreAndPublish(aggregate);
        }

        public async Task<int> CreateBasket(string customer, bool paysVat)
        {
            var newAggregate = new BasketAggregate();

            newAggregate.CreateBasket(customer, paysVat);

            await StoreAndPublish(newAggregate);

            return newAggregate.Id;
          
        }

        private async Task StoreAndPublish(AggregateRoot aggregate)
        {
            await _eventStore.Store(aggregate.PendingEvents.ToArray());
            foreach (var @event in aggregate.PendingEvents)
            {
                await _eventPublisher.Publish(@event);
            }
        }

        public async Task<BasketSummary> GetBasketSummary(int basketId)
        {
            var allEvents = await _eventStore.GetAllEventsByAggregateId(basketId);

            var aggregate = new BasketAggregate(allEvents);

            var summary = new BasketSummary();

            summary.Id = aggregate.Id;
            summary.Customer = aggregate.Customer;
            summary.PaysVAT = aggregate.PaysVat;
            summary.Status = aggregate.Status;
            summary.Articles = aggregate.Articles.Select(a => new BasketArticle() { Article = a.Article, Price = a.Price }).ToList();

            summary.TotalNet = summary.Articles.Sum(a => a.Price);
            summary.TotalGross = aggregate.PaysVat ? (summary.TotalNet * (decimal)1.1) : summary.TotalNet;

            return summary;
        }
    }
}