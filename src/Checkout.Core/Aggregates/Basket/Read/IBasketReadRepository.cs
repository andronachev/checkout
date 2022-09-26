using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket
{
    public interface IBasketReadRepository
    {
        Task RegisterBasketList(Guid id, string customer, bool paysVat);
        Task UpdateBasketStatus(Guid id, string status);
    }
}
