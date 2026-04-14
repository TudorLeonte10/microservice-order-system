using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistance
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                
                entity.Property(o => o.Status)
                    .HasConversion<string>();
                
                entity.Navigation(o => o.Items)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
                
                entity.HasMany(o => o.Items)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Price)
                      .HasPrecision(18, 2);
            });
        }
    }
}
