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
    public class BasketCreatedEventHandler : IEventHandler<BasketCreated>
    {
        private readonly IBasketReadRepository _repository;
        public BasketCreatedEventHandler(IBasketReadRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(BasketCreated @event)
        {
            await _repository.RegisterBasketList(@event.AggregateId, @event.Customer, @event.PaysVat);
        }
    }
}
