using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Basket
{
    public class BasketStatusUpdated : EventBase
    {
        public BasketStatusUpdated(Guid aggregateId, string status)
        {
            EventType = EventTypes.BasketStatusUpdated;
            AggregateId = aggregateId;
            Status = status;
        }

        public string Status { get; private set; }
    }
}
