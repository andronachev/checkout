using Checkout.Core.Aggregates;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Business
{
    public class ServiceBase
    {
        protected readonly IEventStore _eventStore;
        protected readonly IEventPublisher _eventPublisher;
        
        public ServiceBase(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        protected virtual async Task<T> GetAggregate<T>(int aggregateId) where T : AggregateRoot
        {
            var allEvents = await _eventStore.GetAllEventsByAggregateId(aggregateId);

            var aggregate = (T)Activator.CreateInstance(typeof(T), new object[] { allEvents });

            return aggregate;
        }

        protected virtual  T GetNewAggregate<T>() where T : AggregateRoot
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T));
            return aggregate;
        }

        protected virtual async Task StoreAndPublish(AggregateRoot aggregate)
        {
            await _eventStore.Store(aggregate.PendingEvents);
            await _eventPublisher.Publish(aggregate.PendingEvents);
        }
    }
}
