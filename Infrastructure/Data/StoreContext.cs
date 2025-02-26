using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;
using Infrastructure.Config;
namespace Intrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        //  modelBuilder.Entity<Product>()
        // .Property(p => p.Price)
        // .HasColumnType("decimal(18, 2)");
        // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Product).Assembly) ;
    }
}
