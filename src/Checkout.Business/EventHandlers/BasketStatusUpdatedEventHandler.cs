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
    public class BasketStatusUpdatedEventHandler : IEventHandler<BasketStatusUpdated>
    {
        private readonly IBasketReadRepository _repository;
        public BasketStatusUpdatedEventHandler(IBasketReadRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(BasketStatusUpdated @event)
        {
            await _repository.UpdateBasketStatus(@event.AggregateId, @event.Status);
        }
    }
}
