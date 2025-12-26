using Microsoft.EntityFrameworkCore;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Data;

public class OneClickDbContext : DbContext
{
    public OneClickDbContext(DbContextOptions<OneClickDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Configures the database schema using Fluent API.
    /// We use this method to define advanced rules that Data Annotations cannot handle,
    /// such as:
    /// 1. Complex Relationships: Preventing cascade deletes (Restrict).
    /// 2. Unique Constraints: Ensuring Category names are unique.
    /// 3. Data Seeding: Populating the database with initial test data.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //  Configure Prodcut-Category relationsship
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add unique constraind on Category Name

        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" },
                new Category { Id = 4, Name = "Home & Garden" },
                new Category { Id = 5, Name = "Sports" }

            );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
    // Electronics
    new Product { Id = 1, Name = "Laptop HP", Description = "15.6 inch, 8GB RAM, 256GB SSD", ImageURL = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400&h=300&fit=crop", Price = 899.99m, Qty = 15, CategoryId = 1 },
    new Product { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic design, 2.4GHz wireless", ImageURL = "https://images.unsplash.com/photo-1527814050087-3793815479db?w=400&h=300&fit=crop", Price = 25.50m, Qty = 50, CategoryId = 1 },
    new Product { Id = 3, Name = "USB-C Cable", Description = "Fast charging, 6ft length", ImageURL = "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=400&h=300&fit=crop", Price = 12.99m, Qty = 100, CategoryId = 1 },

    // Clothing
    new Product { Id = 4, Name = "T-Shirt Blue", Description = "100% Cotton, Size M", ImageURL = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400&h=300&fit=crop", Price = 19.99m, Qty = 30, CategoryId = 2 },
    new Product { Id = 5, Name = "Jeans Classic", Description = "Denim, Regular fit", ImageURL = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400&h=300&fit=crop", Price = 49.99m, Qty = 20, CategoryId = 2 },
    new Product { Id = 6, Name = "Winter Jacket", Description = "Warm and waterproof", ImageURL = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400&h=300&fit=crop", Price = 89.99m, Qty = 10, CategoryId = 2 },

    // Books
    new Product { Id = 7, Name = "C# Programming Guide", Description = "Complete guide for beginners", ImageURL = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=300&fit=crop", Price = 35.00m, Qty = 25, CategoryId = 3 },
    new Product { Id = 8, Name = "Clean Code", Description = "By Robert C. Martin", ImageURL = "https://images.unsplash.com/photo-1541963463532-d68292c34b19?w=400&h=300&fit=crop", Price = 42.50m, Qty = 18, CategoryId = 3 },
    new Product { Id = 9, Name = "Design Patterns", Description = "Essential guide", ImageURL = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400&h=300&fit=crop", Price = 38.99m, Qty = 22, CategoryId = 3 },

    // Home & Garden
    new Product { Id = 10, Name = "Coffee Maker", Description = "12-cup programmable", ImageURL = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400&h=300&fit=crop", Price = 79.99m, Qty = 12, CategoryId = 4 },
    new Product { Id = 11, Name = "Garden Tools Set", Description = "5-piece essential set", ImageURL = "https://images.unsplash.com/photo-1591727884967-5e6b8f19123c?w=400&h=300&fit=crop", Price = 45.00m, Qty = 8, CategoryId = 4 },
    new Product { Id = 12, Name = "LED Desk Lamp", Description = "Adjustable brightness", ImageURL = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=400&h=300&fit=crop", Price = 32.99m, Qty = 35, CategoryId = 4 },

    // Sports
    new Product { Id = 13, Name = "Yoga Mat", Description = "Non-slip, eco-friendly", ImageURL = "https://images.unsplash.com/photo-1599901860904-17e6ed7083a0?w=400&h=300&fit=crop", Price = 28.50m, Qty = 40, CategoryId = 5 },
    new Product { Id = 14, Name = "Dumbbells Set", Description = "5-25 lbs adjustable", ImageURL = "https://images.unsplash.com/photo-1534367507877-0edd93bd013b?w=400&h=300&fit=crop", Price = 120.00m, Qty = 6, CategoryId = 5 },
    new Product { Id = 15, Name = "Running Shoes", Description = "Lightweight, Size 10", ImageURL = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=300&fit=crop", Price = 85.00m, Qty = 14, CategoryId = 5 }
);
    }
}