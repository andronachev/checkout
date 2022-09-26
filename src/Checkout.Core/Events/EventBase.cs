using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events
{
    public abstract class EventBase
    {
        public int AggregateId { get; protected set; }
        public string EventType { get; protected set; }
        public DateTime EventDateTimeUtc { get; private set; } = DateTime.UtcNow;
    }


    public static class EventTypes
    {
        public static string BasketCreated = "BasketCreated";
        public static string BaskedArticleAdded = "BasketArticleAdded";
        public static string BasketStatusUpdated = "BasketStatusUpdated";
    }
}
