using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket.Read
{
    public class BasketArticle
    {
        public string Article { get; set; }

        public int Price { get; set; }
    }
}
