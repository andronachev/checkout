using Checkout.Business;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Aggregates.Basket.Read;
using Checkout.Core.Events;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;
using Moq;

namespace Checkout.UnitTests
{
    [TestClass]
    public class BasketServiceTests
    {
        [TestMethod]
        public async Task When_GetBasketSummary_WithoutVatPayable_Then_SummaryReturnedWithoutVat()
        {
            var mockEventStore = new Mock<IEventStore>();
            var mockEventPublisher = new Mock<IEventPublisher>();
            var basketService = new BasketService(mockEventStore.Object, mockEventPublisher.Object);

            int basketId = 12345;
            bool paysVat = false;
            var @events = new Core.Events.EventBase[]
            {
                new BasketCreated(basketId, "customer", paysVat, "open"),
                new BasketArticleAdded(basketId, "tomato", 100),
                new BasketArticleAdded(basketId, "peaches", 80),
                new BasketArticleAdded(basketId, "milk", 50),
                new BasketArticleAdded(basketId, "potato", 30),
                new BasketArticleAdded(basketId, "pringles", 40),
            };
            mockEventStore.Setup(e => e.GetAllEventsByAggregateId(It.IsAny<int>())).ReturnsAsync(@events);

            var basketSummary = await basketService.GetBasketSummary(basketId);

            var aggregate = new BasketAggregate(@events);

            Assert.AreEqual(aggregate.Id, basketSummary.Id);
            Assert.AreEqual(aggregate.Customer, basketSummary.Customer);
            Assert.AreEqual(aggregate.PaysVat, basketSummary.PaysVAT);
            Assert.AreEqual(aggregate.Status, basketSummary.Status);
            Assert.AreEqual(aggregate.Articles.Count, basketSummary.Articles.Count);
            Assert.IsTrue(basketSummary.Articles.All(a => aggregate.Articles.Any(x => x.Article == a.Article && x.Price == a.Price)));
            Assert.AreEqual(aggregate.Articles.Sum(a => a.Price), basketSummary.TotalNet);
            Assert.AreEqual(basketSummary.TotalNet, basketSummary.TotalGross);
        }

        [TestMethod]
        public async Task When_GetBasketSummary_WithVatPayable_Then_SummaryReturnedWithVatIncluded()
        {
            var mockEventStore = new Mock<IEventStore>();
            var mockEventPublisher = new Mock<IEventPublisher>();
            var basketService = new BasketService(mockEventStore.Object, mockEventPublisher.Object);

            int basketId = 12345;
            bool paysVat = true;
            var @events = new Core.Events.EventBase[]
            {
                new BasketCreated(basketId, "customer", paysVat, "open"),
                new BasketArticleAdded(basketId, "tomato", 100),
                new BasketArticleAdded(basketId, "peaches", 80),
                new BasketArticleAdded(basketId, "milk", 50),
                new BasketArticleAdded(basketId, "potato", 30),
                new BasketArticleAdded(basketId, "pringles yes", 40),
            };
            mockEventStore.Setup(e => e.GetAllEventsByAggregateId(It.IsAny<int>())).ReturnsAsync(@events);

            var basketSummary = await basketService.GetBasketSummary(basketId);

            var aggregate = new BasketAggregate(@events);

            Assert.AreEqual(aggregate.Id, basketSummary.Id);
            Assert.AreEqual(aggregate.Customer, basketSummary.Customer);
            Assert.AreEqual(aggregate.PaysVat, basketSummary.PaysVAT);
            Assert.AreEqual(aggregate.Status, basketSummary.Status);
            Assert.AreEqual(aggregate.Articles.Count, basketSummary.Articles.Count);
            Assert.IsTrue(basketSummary.Articles.All(a => aggregate.Articles.Any(x => x.Article == a.Article && x.Price == a.Price)));
            Assert.AreEqual(aggregate.Articles.Sum(a => a.Price), basketSummary.TotalNet);
            Assert.AreEqual(basketSummary.TotalNet * (decimal)1.1, basketSummary.TotalGross);
        }

        [TestMethod]
        public async Task When_UpdateStatus_Then_EventStoredAndPublished()
        {
            var mockEventStore = new Mock<IEventStore>();
            var mockEventPublisher = new Mock<IEventPublisher>();
            var basketService = new BasketService(mockEventStore.Object, mockEventPublisher.Object);

            int basketId = 12345;
            string status = "closed";

            mockEventStore.Setup(e => e.GetAllEventsByAggregateId(It.IsAny<int>())).ReturnsAsync(new Core.Events.EventBase[]
            { 
                new BasketCreated(basketId, "customer", true, "open")
            });

            await basketService.UpdateStatus(basketId, status);

            Func<EventBase, bool> validateEventDellegate = new Func<EventBase, bool>((eventBase) =>
            { 
                if(eventBase is BasketStatusUpdated bskUpdated)
                {
                    return bskUpdated.AggregateId == basketId && bskUpdated.Status == status;
                }
                return false;
            });

            mockEventStore.Verify(e => e.Store(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
            mockEventPublisher.Verify(e => e.Publish(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
        }

        [TestMethod]
        public async Task When_AddArticleLine_Then_EventStoredAndPublished()
        {
            var mockEventStore = new Mock<IEventStore>();
            var mockEventPublisher = new Mock<IEventPublisher>();
            var basketService = new BasketService(mockEventStore.Object, mockEventPublisher.Object);

            int basketId = 12345;
            string article = "peaches";
            int price = 100;


            mockEventStore.Setup(e => e.GetAllEventsByAggregateId(It.IsAny<int>())).ReturnsAsync(new Core.Events.EventBase[]
            {
                new BasketCreated(basketId, "customer", true, "open"),
            });

            await basketService.AddArticleLine(basketId, article, price);

            Func<EventBase, bool> validateEventDellegate = new Func<EventBase, bool>((eventBase) =>
            {
                if (eventBase is BasketArticleAdded bskArticleAdded)
                {
                    return bskArticleAdded.AggregateId == basketId &&
                           bskArticleAdded.Article == article && 
                           bskArticleAdded.Price == price;  
                }
                return false;
            });

            mockEventStore.Verify(e => e.Store(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
            mockEventPublisher.Verify(e => e.Publish(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
        }

        [TestMethod]
        public async Task When_CreateBasket_Then_EventStoredAndPublished()
        {
            var mockEventStore = new Mock<IEventStore>();
            var mockEventPublisher = new Mock<IEventPublisher>();
            var basketService = new BasketService(mockEventStore.Object, mockEventPublisher.Object);

            string customer = "Andrei";
            bool paysVat = true;


            int basketId= await basketService.CreateBasket(customer, paysVat);

            Func<EventBase, bool> validateEventDellegate = new Func<EventBase, bool>((eventBase) =>
            {
                if (eventBase is BasketCreated bskCreated)
                {
                    return bskCreated.AggregateId == basketId &&
                           bskCreated.Customer == customer &&
                           bskCreated.PaysVat == paysVat;
                }
                return false;
            });

            Assert.IsTrue(basketId > 0);
            mockEventStore.Verify(e => e.Store(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
            mockEventPublisher.Verify(e => e.Publish(It.Is<EventBase[]>(a => a.Length == 1 && validateEventDellegate(a[0]))), Times.Once);
        }
    }
}