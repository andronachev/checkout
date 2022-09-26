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
        public Guid Id { get; set; }
        public Dictionary<string, Action<EventBase>> EventHandlers { get; set; } = new();
    }
}
