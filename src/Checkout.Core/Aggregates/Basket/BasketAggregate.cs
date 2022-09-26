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
        public BasketAggregate()
        {
            Id = Guid.NewGuid();
            this.EventHandlers.Add(EventTypes.BasketCreated, (@event) => { this.OnCreated(@event as BasketCreated); });
            this.EventHandlers.Add(EventTypes.BasketUpdated, (@event) => { this.OnUpdated(@event); });
            this.EventHandlers.Add(EventTypes.BasketClosed, (@event) => { this.OnClosed(@event); });
        }

        public string Customer { get; set; }
        public bool PaysVat { get; set; }

        public void CreateBasket(string customer, bool paysVat)
        {
            var @event = new BasketCreated(customer, paysVat);

            this.PendingEventsInternal.Add(@event);

            this.OnCreated(@event);
        }

        private void OnCreated(BasketCreated @event)
        {
            this.Customer = @event.Customer;
            this.PaysVat = @event.PaysVat;
        }
        private void OnUpdated(EventBase @event)
        {
        }
        private void OnClosed(EventBase @event)
        {
        }
    }
}
