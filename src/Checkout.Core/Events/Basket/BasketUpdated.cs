using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Basket
{
    public class BasketUpdated : EventBase
    {
        public BasketUpdated(Guid aggregateId, string article, int price)
        {
            EventType = EventTypes.BasketUpdated;
            AggregateId = aggregateId;
            Article = article;
            Price = price;  
        }

        public string Article { get; private set; }
        public int Price { get; private set; }
    }
}
