using Checkout.Core.Events;
using Checkout.Core.Events.Interfaces;
using Checkout.Infrastructure.Common.Data;
using Checkout.Infrastructure.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<EventBase[]> GetAllEventsByAggregateId(int aggregateId)
        {
            var storedEvents = await _dbContext.Events.Where(e => e.AggregateId == aggregateId).ToListAsync();
            List<EventBase> coreEvents = new List<EventBase>();
            foreach(var storedEvent in storedEvents)
            {
                Type eventType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).FirstOrDefault(t => t.Name == storedEvent.EventType);
                var coreEvent = JsonConvert.DeserializeObject(storedEvent.EventPayload, eventType);
                coreEvents.Add(coreEvent as EventBase);
            }

            return coreEvents.ToArray();
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
