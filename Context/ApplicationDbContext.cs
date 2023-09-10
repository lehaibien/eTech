using eTech.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eTech.Context
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryImage> CategoryImages { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<ManufacturerImage> ManufacturerImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<UserImage> UserImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<OrderItem>()
      .HasKey(oi => new { oi.OrderId, oi.ProductId });

    }
  }
}
