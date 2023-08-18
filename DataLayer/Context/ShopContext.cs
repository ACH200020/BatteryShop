using DataLayer.Entities.Categories;
using DataLayer.Entities.Comments;
using DataLayer.Entities.OrderDetails;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> option) : base(option)
    {
        
    }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OrderDetailDto> OrderDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(modelBuilder);
    }
}