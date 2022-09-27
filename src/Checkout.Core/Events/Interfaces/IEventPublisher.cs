﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Events.Interfaces
{
    public interface IEventPublisher
    {
        Task Publish(EventBase[] @events);
    }
}
