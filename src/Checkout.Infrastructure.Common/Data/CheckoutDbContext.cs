using Checkout.Infrastructure.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common.Data
{
    public class CheckoutDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public CheckoutDbContext(DbContextOptions<CheckoutDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
