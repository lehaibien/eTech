using eTech.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTech.Context {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>()
              .HasKey(oi => new { oi.OrderId, oi.ProductId });
            modelBuilder.Entity<Category>()
              .HasMany(cat => cat.Products).WithOne(p => p.Category);
            modelBuilder.Entity<Brand>()
              .HasMany(b => b.Products).WithOne(p => p.Brand);
            modelBuilder.Entity<CartItem>()
                .HasKey(c => new { c.UserId, c.ProductId });
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders).WithOne(o => o.User);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems).WithOne(oi => oi.Order);
            // Auto include address and image to applicationUser
        }
    }
}
