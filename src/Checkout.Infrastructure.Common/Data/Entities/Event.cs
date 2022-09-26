using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common.Data.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
     
        public int AggregateId { get; set; }
        public string EventType { get; set; }
        public DateTime EventDateTimeUtc { get; set; }

        public string EventPayload { get; set; }
    }
}
