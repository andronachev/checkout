using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events
{
    public abstract class EventBase
    {
        public string EventType { get; set; }
    

    }


    public static class EventType
    {
        public static string BasketCreated = "BasketCreated";
        public static string BasketUpdated = "BaskedUpdated";
        public static string BasketClosed = "BaskedClosed";
    }
}
