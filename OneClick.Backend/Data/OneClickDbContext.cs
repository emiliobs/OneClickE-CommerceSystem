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
            new Category { Id = 1, Name = "Apple" },
            new Category { Id = 2, Name = "Cars" },
            new Category { Id = 3, Name = "Beauty" },
            new Category { Id = 4, Name = "Footwear" },
            new Category { Id = 5, Name = "Food" },
            new Category { Id = 6, Name = "Cosmetics" },
            new Category { Id = 7, Name = "Sports" },
            new Category { Id = 8, Name = "Erotic" },
            new Category { Id = 9, Name = "Hardware" },
            new Category { Id = 10, Name = "Gamer" },
            new Category { Id = 11, Name = "Home" },
            new Category { Id = 12, Name = "Garden" },
            new Category { Id = 13, Name = "Toys" },
            new Category { Id = 14, Name = "Lingerie" },
            new Category { Id = 15, Name = "Pets" },
            new Category { Id = 16, Name = "Nutrition" },
            new Category { Id = 17, Name = "Clothing" },
            new Category { Id = 18, Name = "Technology" }
        );

        // Seed Products with REAL Images (Unsplash)
        modelBuilder.Entity<Product>().HasData(
            // --- Apple (CatId: 1) ---
            new Product
            {
                Id = 1,
                Name = "iPhone 15",
                Description = "Apple smartphone, 128GB",
                CategoryId = 1,
                Price = 899.00m,
                Qty = 20,
                ImageURL = "https://images.unsplash.com/photo-1696446701796-da61225697cc?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 2,
                Name = "MacBook Air 13 M2",
                Description = "Apple laptop, 8GB/256GB",
                CategoryId = 1,
                Price = 1199.00m,
                Qty = 8,
                ImageURL = "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format&fit=crop&w=600&q=80"
            },

            // --- Cars (CatId: 2) ---
            new Product
            {
                Id = 3,
                Name = "Car Care Kit",
                Description = "Shampoo, wax and microfiber towels",
                CategoryId = 2,
                Price = 39.99m,
                Qty = 35,
                ImageURL = "https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 4,
                Name = "Dash Cam HD",
                Description = "1080p dashboard camera with loop recording",
                CategoryId = 2,
                Price = 69.99m,
                Qty = 12,
                ImageURL = "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format&fit=crop&w=600&q=80"
            },

            // --- Beauty (CatId: 3) ---
            new Product
            {
                Id = 5,
                Name = "Hydrating Face Cream",
                Description = "Daily moisturizer for all skin types",
                CategoryId = 3,
                Price = 22.50m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 6,
                Name = "Vitamin C Serum",
                Description = "Brightening 10% serum",
                CategoryId = 3,
                Price = 18.99m,
                Qty = 15,
                ImageURL = "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format&fit=crop&w=600&q=80"
            },

            // --- Footwear (CatId: 4) ---
            new Product
            {
                Id = 7,
                Name = "Men's Running Shoes",
                Description = "Lightweight breathable trainers",
                CategoryId = 4,
                Price = 79.90m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 8,
                Name = "Women's Casual Sneakers",
                Description = "Comfort everyday sneakers",
                CategoryId = 4,
                Price = 59.90m,
                Qty = 10,
                ImageURL = "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format&fit=crop&w=600&q=80"
            },

            // --- Food (CatId: 5) ---
            new Product
            {
                Id = 9,
                Name = "Organic Granola 500g",
                Description = "Whole-grain oats with nuts",
                CategoryId = 5,
                Price = 6.99m,
                Qty = 50,
                ImageURL = "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 10,
                Name = "Olive Oil 1L",
                Description = "Extra virgin, cold pressed",
                CategoryId = 5,
                Price = 9.99m,
                Qty = 30,
                ImageURL = "https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format&fit=crop&w=600&q=80"
            },

            // --- Cosmetics (CatId: 6) ---
            new Product
            {
                Id = 11,
                Name = "Matte Lipstick",
                Description = "Long-wear, natural shade",
                CategoryId = 6,
                Price = 12.99m,
                Qty = 35,
                ImageURL = "https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 12,
                Name = "Black Mascara",
                Description = "Water-resistant, volume effect",
                CategoryId = 6,
                Price = 11.49m,
                Qty = 22,
                ImageURL = "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format&fit=crop&w=600&q=80"
            },

            // --- Sports (CatId: 7) ---
            new Product
            {
                Id = 13,
                Name = "Football Size 5",
                Description = "PU match ball",
                CategoryId = 7,
                Price = 29.99m,
                Qty = 60,
                ImageURL = "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 14,
                Name = "Yoga Mat 6mm",
                Description = "Non-slip exercise mat",
                CategoryId = 7,
                Price = 24.50m,
                Qty = 8,
                ImageURL = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format&fit=crop&w=600&q=80"
            },

            // --- Erotic (Romantic/Safe) (CatId: 8) ---
            new Product
            {
                Id = 15,
                Name = "Romance Candle Set",
                Description = "Scented candles set for ambience",
                CategoryId = 8,
                Price = 19.99m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 16,
                Name = "Romantic Board Game",
                Description = "Couples card & board game",
                CategoryId = 8,
                Price = 17.50m,
                Qty = 12,
                ImageURL = "https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format&fit=crop&w=600&q=80"
            },

            // --- Hardware (CatId: 9) ---
            new Product
            {
                Id = 17,
                Name = "Cordless Drill 18V",
                Description = "2 batteries, fast charger",
                CategoryId = 9,
                Price = 89.00m,
                Qty = 14,
                ImageURL = "https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 18,
                Name = "Tool Set 46pcs",
                Description = "Sockets, bits and ratchet",
                CategoryId = 9,
                Price = 39.99m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format&fit=crop&w=600&q=80"
            },

            // --- Gamer (CatId: 10) ---
            new Product
            {
                Id = 19,
                Name = "Mechanical Keyboard RGB",
                Description = "Linear switches, full-size",
                CategoryId = 10,
                Price = 69.90m,
                Qty = 18,
                ImageURL = "https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 20,
                Name = "Gaming Mouse 8K DPI",
                Description = "Programmable buttons",
                CategoryId = 10,
                Price = 29.90m,
                Qty = 45,
                ImageURL = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format&fit=crop&w=600&q=80"
            },

            // --- Home (CatId: 11) ---
            new Product
            {
                Id = 21,
                Name = "LED Desk Lamp",
                Description = "Adjustable arm, warm/cool light",
                CategoryId = 11,
                Price = 22.99m,
                Qty = 45,
                ImageURL = "https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 22,
                Name = "Memory Foam Pillow",
                Description = "Ergonomic cervical pillow",
                CategoryId = 11,
                Price = 34.90m,
                Qty = 16,
                ImageURL = "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format&fit=crop&w=600&q=80"
            },

            // --- Garden (CatId: 12) ---
            new Product
            {
                Id = 23,
                Name = "Garden Hose 15m",
                Description = "Flexible anti-kink hose",
                CategoryId = 12,
                Price = 19.99m,
                Qty = 30,
                ImageURL = "https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 24,
                Name = "Pruning Shears",
                Description = "Bypass pruner stainless steel",
                CategoryId = 12,
                Price = 12.50m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format&fit=crop&w=600&q=80"
            },

            // --- Toys (CatId: 13) ---
            new Product
            {
                Id = 25,
                Name = "Building Blocks Set",
                Description = "Compatible bricks 500 pcs",
                CategoryId = 13,
                Price = 24.99m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 26,
                Name = "Puzzle 1000 Pieces",
                Description = "Landscape illustration",
                CategoryId = 13,
                Price = 14.99m,
                Qty = 12,
                ImageURL = "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format&fit=crop&w=600&q=80"
            },

            // --- Lingerie (CatId: 14) ---
            new Product
            {
                Id = 27,
                Name = "Soft Sleepwear Set",
                Description = "Two-piece loungewear set",
                CategoryId = 14,
                Price = 29.90m,
                Qty = 28,
                ImageURL = "https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 28,
                Name = "Seamless Undergarments",
                Description = "Comfort fit essentials",
                CategoryId = 14,
                Price = 19.90m,
                Qty = 18,
                ImageURL = "https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format&fit=crop&w=600&q=80"
            },

            // --- Pets (CatId: 15) ---
            new Product
            {
                Id = 29,
                Name = "Dry Dog Food 3kg",
                Description = "Chicken & rice formula",
                CategoryId = 15,
                Price = 16.99m,
                Qty = 35,
                ImageURL = "https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 30,
                Name = "Cat Scratching Post",
                Description = "Sisal rope post with base",
                CategoryId = 15,
                Price = 21.99m,
                Qty = 14,
                ImageURL = "https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format&fit=crop&w=600&q=80"
            },

            // --- Nutrition (CatId: 16) ---
            new Product
            {
                Id = 31,
                Name = "Whey Protein 1kg",
                Description = "Vanilla flavor",
                CategoryId = 16,
                Price = 32.90m,
                Qty = 20,
                ImageURL = "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 32,
                Name = "Multivitamin 120 caps",
                Description = "Daily vitamins & minerals",
                CategoryId = 16,
                Price = 14.50m,
                Qty = 10,
                ImageURL = "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format&fit=crop&w=600&q=80"
            },

            // --- Clothing (CatId: 17) ---
            new Product
            {
                Id = 33,
                Name = "Men's Cotton T-Shirt",
                Description = "Classic fit, 100% cotton",
                CategoryId = 17,
                Price = 14.99m,
                Qty = 60,
                ImageURL = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 34,
                Name = "Women's Denim Jacket",
                Description = "Mid-wash, regular fit",
                CategoryId = 17,
                Price = 49.90m,
                Qty = 9,
                ImageURL = "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format&fit=crop&w=600&q=80"
            },

            // --- Technology (CatId: 18) ---
            new Product
            {
                Id = 35,
                Name = "Bluetooth Speaker",
                Description = "Portable speaker with mic",
                CategoryId = 18,
                Price = 34.99m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format&fit=crop&w=600&q=80"
            },
            new Product
            {
                Id = 36,
                Name = "USB-C Hub 6-in-1",
                Description = "HDMI, USB 3.0, SD/MicroSD",
                CategoryId = 18,
                Price = 24.90m,
                Qty = 15,
                ImageURL = "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format&fit=crop&w=600&q=80"
            }
        );
    }
}