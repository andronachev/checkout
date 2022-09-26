using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Business.EventHandlers
{
    public class BasketUpdatedEventHandler : IEventHandler<BasketUpdated>
    {
        private readonly IBasketReadRepository _repository;
        public BasketUpdatedEventHandler(IBasketReadRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(BasketUpdated @event)
        {
            //maybe save the list of articles in SQL
        }
    }
}
