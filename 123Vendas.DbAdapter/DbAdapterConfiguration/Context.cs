using _123Vendas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.DbAdapter.DbAdapterConfiguration
{
    public class Context : DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        public DbSet<BranchStore> BranchStores { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("vendas");

            modelBuilder.Entity<Sale>()
                .Property(s => s.Client.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Sale>()
                .Property(s => s.BranchStore.NameStore)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<LoggerApplication>()
                .Property(l => l.Client)
                .HasMaxLength(255);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey("ClientId");

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.BranchStore)
                .WithMany()
                .HasForeignKey("BranchStoreId");

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Products)
                .WithOne()
                .HasForeignKey("SaleId");


            base.OnModelCreating(modelBuilder);
        }
    }
}
