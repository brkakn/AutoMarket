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
        public virtual DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCartDetailEntity> ShoppingCartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<ItemEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<StockEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Item)
                 .WithMany(x => x.Stocks)
                 .HasForeignKey(x => x.ItemId);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<ShoppingCartEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<ShoppingCartDetailEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.ShoppingCart)
                 .WithMany(x => x.ShoppingCartDetails)
                 .HasForeignKey(x => x.ShoppingCartId);
                e.HasOne(x => x.Item)
                 .WithMany(x => x.ShoppingCartDetails)
                 .HasForeignKey(x => x.ItemId);
                e.Property(p => p.RowVersion).IsRowVersion();
            });
        }
    }
}
