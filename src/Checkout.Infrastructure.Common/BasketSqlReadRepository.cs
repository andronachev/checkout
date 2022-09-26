using Checkout.Core.Aggregates.Basket;
using Checkout.Infrastructure.Common.Data;
using Checkout.Infrastructure.Common.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Checkout.Infrastructure.Common
{
    public class BasketSqlReadRepository : IBasketReadRepository
    {
        private CheckoutDbContext _dbContext;
        public BasketSqlReadRepository(CheckoutDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterBasketList(Guid id, string customer, bool paysVat)
        {
            var basket = new Basket()
            {
                Id = id,
                Customer = customer,
                PaysVat = paysVat,
                Status = "Open"
            };

            _dbContext.Baskets.Add(basket);

            await _dbContext.SaveChangesAsync();
        }
    }
}