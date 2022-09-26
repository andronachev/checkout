using Checkout.Core.Events;
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
            this.EventHandlers.Add(EventType.BasketCreated, (@event) => { this.OnCreated(@event); });
            this.EventHandlers.Add(EventType.BasketUpdated, (@event) => { this.OnUpdated(@event); });
            this.EventHandlers.Add(EventType.BasketClosed, (@event) => { this.OnClosed(@event); });
        }

        private void OnCreated(EventBase @event)
        {
        }
        private void OnUpdated(EventBase @event)
        {
        }
        private void OnClosed(EventBase @event)
        {
        }
    }
}
