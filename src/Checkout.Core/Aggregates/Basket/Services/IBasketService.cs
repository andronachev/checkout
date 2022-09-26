using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket.Services
{
    public interface IBasketService
    {
        Task<Guid> CreateBasket(string customer, bool paysVat);

        Task AddArticleLine(Guid basketId, string article, int price);

        Task UpdateStatus(Guid basketId, string status);
    }
}
