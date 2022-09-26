using Checkout.Core.Events;
using Checkout.Core.Events.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket
{
    public class BasketAggregate : AggregateRoot
    {
        public BasketAggregate(): base()
        {
            this.EventReconstitutor.Add(EventTypes.BasketCreated, (@event) => { this.OnCreated(@event as BasketCreated); });
            this.EventReconstitutor.Add(EventTypes.BaskedArticleAdded, (@event) => { this.OnArticleAdded(@event as BasketArticleAdded); });
            this.EventReconstitutor.Add(EventTypes.BasketStatusUpdated, (@event) => { this.OnStatusUpdated(@event as BasketStatusUpdated); });
        }

        public BasketAggregate(EventBase[] @events) : this()
        {
            Reconstitute(@events);
        }

        private void Reconstitute(EventBase[] events)
        {
            foreach(var @event in events)
            {
                if (this.EventReconstitutor.ContainsKey(@event.EventType))
                {
                    this.EventReconstitutor[@event.EventType](@event);
                }
                else
                {
                    throw new InvalidOperationException("Event reconstituter not implemented, canot reconstitute/rehydrate aggregate");
                }
            }
        }

        public string Customer { get; set; }
        public bool PaysVat { get; set; }
        public string Status { get; set; }

        public List<ArticleValueObj> Articles { get; set; } = new();

        public void CreateBasket(string customer, bool paysVat)
        {
            var @event = new BasketCreated(Id, customer, paysVat, "Open");

            this.PendingEventsInternal.Add(@event);

            this.OnCreated(@event);
        }

        public void UpdateStatus(string status)
        {
            var @event = new BasketStatusUpdated(Id, status);

            this.PendingEventsInternal.Add(@event);

            this.OnStatusUpdated(@event);
        }

        public void AddArticleLine(string article, int price)
        {
            var @event = new BasketArticleAdded(Id, article, price);

            this.PendingEventsInternal.Add(@event);

            this.OnArticleAdded(@event);
        }

        private void OnCreated(BasketCreated @event)
        {
            this.Id = @event.AggregateId;
            this.Customer = @event.Customer;
            this.PaysVat = @event.PaysVat;
            this.Status = @event.Status;
        }
        private void OnArticleAdded(BasketArticleAdded @event)
        {
            Articles.Add(new ArticleValueObj()
            {
                Article = @event.Article,
                Price = @event.Price
            });
        }
        private void OnStatusUpdated(BasketStatusUpdated @event)
        {
            this.Status = @event.Status;
        }
    }

    public class ArticleValueObj
    {
        public string Article { get; set; }

        public int Price { get; set; }
    }
}
