

using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data;

public class InvoicesDbContext : DbContext
{
    public DbSet<Person>? Persons { get; set; }
    public DbSet<Invoice>? Invoices { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
        base.OnModelCreating (modelBuilder);

        modelBuilder
            .Entity<Invoice>()
            .Property(x => x.Price)
            .HasColumnType("decimal(10,2)");
        modelBuilder
            .Entity<Invoice>()
            .HasOne(i => i.Seller)
            .WithMany(p => p.Sales)
            .HasForeignKey(i => i.SellerId);
        modelBuilder
            .Entity<Invoice>()
            .HasOne(i => i.Buyer)
            .WithMany(p => p.Purchases)
            .HasForeignKey(i => i.BuyerId);
    }
    public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options)
        : base(options)
    {
    }
}