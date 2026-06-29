using Microsoft.EntityFrameworkCore;
using MiniStripe.Domain.Entities;

namespace MiniStripe.Infrastructure.Persistence
{
    public class MiniStripeDbContext : DbContext
    {
        public DbSet<PaymentIntent> PaymentIntents {get; set;} = null!;
        public DbSet<Merchant> Merchants {get; set;} = null!;
        public DbSet<Customer> Customers {get; set;} = null!;
        public MiniStripeDbContext(DbContextOptions<MiniStripeDbContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniStripeDbContext).Assembly);
        }
    }
}