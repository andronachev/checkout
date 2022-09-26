using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events
{
    public abstract class EventBase
    {
        public Guid AggregateId { get; protected set; }
        public string EventType { get; protected set; }
    
        public DateTime EventDateTimeUtc { get; set; } = DateTime.UtcNow;
    }


    public static class EventTypes
    {
        public static string BasketCreated = "BasketCreated";
        public static string BasketUpdated = "BasketUpdated";
        public static string BasketStatusUpdated = "BasketStatusUpdated";
    }
}
