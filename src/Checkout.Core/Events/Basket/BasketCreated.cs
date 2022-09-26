using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Basket
{
    public class BasketCreated : EventBase
    {
        public BasketCreated(string customer, bool paysVat)
        {
            EventType = EventTypes.BasketCreated;
            Customer = customer;
            PaysVat = paysVat;  
        }

        public string Customer { get; private set; }
        public bool PaysVat { get; private set; }
    }
}
