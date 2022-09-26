using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Events.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Business.EventHandlers
{
    public class BasketCreatedEventHandler
    {
        private readonly IBasketReadRepository _repository;
        public BasketCreatedEventHandler(IBasketReadRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(BasketCreated @event)
        {
            //write in SQL, wherever needed, a read projection etc
        }
    }
}
