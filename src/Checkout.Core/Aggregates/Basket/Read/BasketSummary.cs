using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket.Read
{
    public class BasketSummary
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public string Status { get; set; }
        public int TotalNet { get; set; }
        public decimal TotalGross { get; set; }
        public List<BasketArticle> Articles { get; set; } = new();
    }
}
