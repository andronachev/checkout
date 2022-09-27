using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket
{
    public interface IBasketReadRepository
    {
        Task RegisterBasketList(int id, string customer, bool paysVat, string status);
        Task UpdateBasketStatus(int id, string status);
    }
}
