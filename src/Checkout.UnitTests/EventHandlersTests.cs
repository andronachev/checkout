using Checkout.Business;
using Checkout.Business.EventHandlers;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Aggregates.Basket.Read;
using Checkout.Core.Events;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;
using Moq;

namespace Checkout.UnitTests
{
    [TestClass]
    public class EventHandlersTests
    {
        [TestMethod]
        public async Task When_BasketCreatedEventHandler_Then_EventHandled()
        {
            var repositoryMock = new Mock<IBasketReadRepository>();
            var eventHandler = new BasketCreatedEventHandler(repositoryMock.Object);

            var @event = new BasketCreated(1235, "customer", true, "open");

            await eventHandler.Handle(@event);

            repositoryMock.Verify(r => r.RegisterBasketList(@event.AggregateId, @event.Customer, @event.PaysVat, @event.Status), Times.Once);
        }

        [TestMethod]
        public async Task When_BasketStatusUpdatedEventHandler_Then_EventHandled()
        {
            var repositoryMock = new Mock<IBasketReadRepository>(MockBehavior.Strict);
            var eventHandler = new BasketStatusUpdatedEventHandler(repositoryMock.Object);

            var @event = new BasketStatusUpdated(1235, "open");

            var task = new Task(() => { });
            task.Start();

            repositoryMock.Setup(r => r.UpdateBasketStatus(It.IsAny<int>(), It.IsAny<string>())).Returns(task);

            await eventHandler.Handle(@event);

            repositoryMock.Verify(r => r.UpdateBasketStatus(@event.AggregateId, @event.Status), Times.Once);
        }
    }
}