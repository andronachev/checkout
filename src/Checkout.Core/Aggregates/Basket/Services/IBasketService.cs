using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket.Services
{
    public interface IBasketService
    {
        Task<int> CreateBasket(string customer, bool paysVat);

        Task AddArticleLine(int basketId, string article, int price);

        Task UpdateStatus(int basketId, string status);
    }
}
