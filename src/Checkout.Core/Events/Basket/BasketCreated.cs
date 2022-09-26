using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Basket
{
    public class BasketCreated : EventBase
    {
        public BasketCreated(Guid aggregateId, string customer, bool paysVat, string status)
        {
            EventType = EventTypes.BasketCreated;
            AggregateId = aggregateId;
            Customer = customer;
            PaysVat = paysVat;
            Status = status;
        }

        public string Customer { get; private set; }
        public string Status { get; private set; }
        public bool PaysVat { get; private set; }
    }
}
