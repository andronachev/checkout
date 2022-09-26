using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common.Data.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVat { get; set; }
        public string Status { get; set; }
    }
}
