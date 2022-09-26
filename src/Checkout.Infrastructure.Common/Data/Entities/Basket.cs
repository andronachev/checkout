using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common.Data.Entities
{
    public class Basket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVat { get; set; }
        public string Status { get; set; }
    }
}
