using Checkout.Core.Events;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;
using System;

namespace Checkout.Infrastructure.Sync
{
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider; 
        public InMemoryEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider; 
        }

        public async Task Publish(EventBase[] @events)
        {
            foreach(var @event in @events)
            {
                await Publish(@event);
            }
        }

        private async Task Publish(EventBase @event)
        {
            switch (@event.EventType)
            {
                case "BasketCreated":
                    {
                        var handler = _serviceProvider.GetService(typeof(IEventHandler<BasketCreated>)) as IEventHandler<BasketCreated>;
                        await handler.Handle(@event as BasketCreated);
                        break;
                    }
                case "BasketArticleAdded":
                    {
                        var handler = _serviceProvider.GetService(typeof(IEventHandler<BasketArticleAdded>)) as IEventHandler<BasketArticleAdded>;
                        await handler.Handle(@event as BasketArticleAdded);
                        break;
                    }
                case "BasketStatusUpdated":
                    {
                        var handler = _serviceProvider.GetService(typeof(IEventHandler<BasketStatusUpdated>)) as IEventHandler<BasketStatusUpdated>;
                        await handler.Handle(@event as BasketStatusUpdated);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}