using AutoMarket.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Api.Infrastructures.Database
{
    public class AutoMarketDbContext : DbContext
    {
        public AutoMarketDbContext(DbContextOptions<AutoMarketDbContext> options) : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ItemEntity> Items { get; set; }
        public virtual DbSet<StockEntity> Stocks { get; set; }
        public virtual DbSet<BasketEntity> Baskets { get; set; }
        public virtual DbSet<BasketDetailEntity> BasketDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ItemEntity>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<StockEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Item)
                 .WithMany(x => x.Stocks)
                 .HasForeignKey(x => x.ItemId);
            });

            modelBuilder.Entity<BasketEntity>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<BasketDetailEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Basket)
                 .WithMany(x => x.BasketDetails)
                 .HasForeignKey(x => x.BasketId);
            });
        }
    }
}
