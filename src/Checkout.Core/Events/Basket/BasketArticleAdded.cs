using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Basket
{
    public class BasketArticleAdded : EventBase
    {
        public BasketArticleAdded(int aggregateId, string article, int price)
        {
            EventType = EventTypes.BaskedArticleAdded;
            AggregateId = aggregateId;
            Article = article;
            Price = price;  
        }

        public string Article { get; private set; }
        public int Price { get; private set; }
    }
}
