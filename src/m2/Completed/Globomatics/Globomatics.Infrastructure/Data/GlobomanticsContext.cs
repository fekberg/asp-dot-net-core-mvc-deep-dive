using Globomantics.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Infrastructure.Data;

public class GlobomanticsContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // MOVE TO A SECURE PLACE!!!!
        var connectionString = "Data Source=globomantics.db;";

        optionsBuilder.UseSqlite(connectionString);
    }

    public static void CreateInitialDatabase(GlobomanticsContext context)
    {
        context.Database.EnsureDeleted();

        context.Database.Migrate();

        context.Products.Add(new Product { ProductId = Guid.Parse("4bc34cb4-c16e-4172-97af-4f90d2c494ec"), Name = "Alexander Lemtov Live", Price = 65m });
        context.Products.Add(new Product { ProductId = Guid.Parse("cda496ae-ec4d-410f-8bcd-26aaca5ba9da"), Name = "To The Moon And Back", Price = 135m });
        context.Products.Add(new Product { ProductId = Guid.Parse("92bc5f1c-0851-4fbb-931a-d6f807aae99a"), Name = "The State Of Affairs: Mariam Live!", Price = 85m });

        context.SaveChanges();

    }
}
