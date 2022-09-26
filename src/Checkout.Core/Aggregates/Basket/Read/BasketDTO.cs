using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Aggregates.Basket.Read
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public string Status { get; set; } = "Open";
        public int TotalNet { get; set; }
        public int TotalGross { get; set; }
        public List<ArticleDTO> Articles { get; set; } = new();
    }
}
