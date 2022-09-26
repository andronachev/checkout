using Checkout.Core.Events;
using Checkout.Core.Events.Interfaces;
using Checkout.Infrastructure.Common.Data;
using Checkout.Infrastructure.Common.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common
{
    public class SqlEventStore : IEventStore
    {
        private CheckoutDbContext _dbContext;
        public SqlEventStore(CheckoutDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<EventBase[]> GetAllEventsByAggregateId(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public async Task Store(EventBase[] events)
        {
            foreach(var @event in events)
            {
                var storeEvent = new Event();
                storeEvent.Id = Guid.NewGuid();
                storeEvent.AggregateId = @event.AggregateId;
                storeEvent.EventDateTimeUtc = @event.EventDateTimeUtc;
                storeEvent.EventType = @event.EventType;
                storeEvent.EventPayload = JsonConvert.SerializeObject(@event);

                _dbContext.Events.Add(storeEvent);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
