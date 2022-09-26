using Checkout.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates
{
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            Id = new Random().Next(1000,100000);
        }
        public int Id { get; protected set; }
        protected Dictionary<string, Action<EventBase>> EventReconstitutor { get; set; } = new();
        public EventBase[] PendingEvents { get { return this.PendingEventsInternal.ToArray(); } }
        protected List<EventBase> PendingEventsInternal { get; } = new();
    }
}
