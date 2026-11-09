using ClothingStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Clothing> Clothings { get; set; }
    public DbSet<ClothingSize> ClothingSizes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}