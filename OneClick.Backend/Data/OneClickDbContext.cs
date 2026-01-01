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

        // Create Index to avoid duplicate category names
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        // =========================================================================
        // 1. SEED CATEGORIES (18 Categories)
        // =========================================================================
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

        // =========================================================================
        // 2. SEED PRODUCTS (36 Products - 2 per Category)
        // All images are cropped to 600x600 for perfect grid alignment.
        // =========================================================================
        modelBuilder.Entity<Product>().HasData(

            // --- 1. Apple ---
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Description = "Titanium design, A17 Pro chip, the most powerful iPhone yet.",
                CategoryId = 1,
                Price = 1199.00m,
                Qty = 20,
                ImageURL = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 2,
                Name = "MacBook Air M2",
                Description = "Supercharged by M2. Strikingly thin and fast.",
                CategoryId = 1,
                Price = 1099.00m,
                Qty = 15,
                ImageURL = "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 2. Cars ---
            new Product
            {
                Id = 3,
                Name = "Meguiar's Car Wax",
                Description = "Ultimate liquid wax for a deep mirror-like shine.",
                CategoryId = 2,
                Price = 24.99m,
                Qty = 50,
                ImageURL = "https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 4,
                Name = "4K Dash Cam Front/Rear",
                Description = "Secure your driving with 24/7 loop recording.",
                CategoryId = 2,
                Price = 89.50m,
                Qty = 12,
                ImageURL = "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 3. Beauty ---
            new Product
            {
                Id = 5,
                Name = "Hydro Boost Gel Cream",
                Description = "Instantly quenches dry skin for a healthy glow.",
                CategoryId = 3,
                Price = 19.99m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 6,
                Name = "Vitamin C Serum",
                Description = "Brightening serum for uneven skin tone.",
                CategoryId = 3,
                Price = 34.00m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 4. Footwear ---
            new Product
            {
                Id = 7,
                Name = "Nike Air Zoom Pegasus",
                Description = "Responsive running shoes for road running.",
                CategoryId = 4,
                Price = 129.99m,
                Qty = 30,
                ImageURL = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 8,
                Name = "Classic White Sneakers",
                Description = "Minimalist leather sneakers for everyday wear.",
                CategoryId = 4,
                Price = 89.90m,
                Qty = 18,
                ImageURL = "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 5. Food ---
            new Product
            {
                Id = 9,
                Name = "Organic Honey Granola",
                Description = "Crunchy clusters with almonds and seeds.",
                CategoryId = 5,
                Price = 8.50m,
                Qty = 60,
                ImageURL = "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 10,
                Name = "Extra Virgin Olive Oil",
                Description = "Cold-pressed, rich flavor perfect for salads.",
                CategoryId = 5,
                Price = 18.99m,
                Qty = 45,
                ImageURL = "https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 6. Cosmetics ---
            new Product
            {
                Id = 11,
                Name = "Matte Velvet Lipstick",
                Description = "Long-lasting color with a hydrating formula.",
                CategoryId = 6,
                Price = 22.00m,
                Qty = 35,
                ImageURL = "https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 12,
                Name = "Volumizing Mascara",
                Description = "Dramatic volume without clumping.",
                CategoryId = 6,
                Price = 16.50m,
                Qty = 22,
                ImageURL = "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 7. Sports ---
            new Product
            {
                Id = 13,
                Name = "Pro Match Football",
                Description = "FIFA quality certified ball size 5.",
                CategoryId = 7,
                Price = 34.99m,
                Qty = 60,
                ImageURL = "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 14,
                Name = "Non-Slip Yoga Mat",
                Description = "Eco-friendly material with alignment lines.",
                CategoryId = 7,
                Price = 29.95m,
                Qty = 15,
                ImageURL = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 8. Erotic (Romantic) ---
            new Product
            {
                Id = 15,
                Name = "Luxury Scented Candles",
                Description = "Set of 3 soy wax candles for ambiance.",
                CategoryId = 8,
                Price = 24.99m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 16,
                Name = "Couples Card Game",
                Description = "Deepen your connection with fun questions.",
                CategoryId = 8,
                Price = 19.99m,
                Qty = 20,
                ImageURL = "https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 9. Hardware ---
            new Product
            {
                Id = 17,
                Name = "Cordless Drill Driver",
                Description = "18V power with 2 batteries and case.",
                CategoryId = 9,
                Price = 89.00m,
                Qty = 14,
                ImageURL = "https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 18,
                Name = "46-Piece Tool Set",
                Description = "Socket wrench set for home and auto repair.",
                CategoryId = 9,
                Price = 45.00m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 10. Gamer ---
            new Product
            {
                Id = 19,
                Name = "RGB Mechanical Keyboard",
                Description = "Tactile blue switches with custom lighting.",
                CategoryId = 10,
                Price = 79.99m,
                Qty = 18,
                ImageURL = "https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 20,
                Name = "Wireless Gaming Mouse",
                Description = "Ultra-lightweight, 20,000 DPI sensor.",
                CategoryId = 10,
                Price = 49.99m,
                Qty = 45,
                ImageURL = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 11. Home ---
            new Product
            {
                Id = 21,
                Name = "Modern LED Desk Lamp",
                Description = "Dimmable light with USB charging port.",
                CategoryId = 11,
                Price = 39.99m,
                Qty = 45,
                ImageURL = "https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 22,
                Name = "Memory Foam Pillow",
                Description = "Ergonomic cervical pillow for neck pain relief.",
                CategoryId = 11,
                Price = 29.99m,
                Qty = 16,
                ImageURL = "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 12. Garden ---
            new Product
            {
                Id = 23,
                Name = "Expandable Garden Hose",
                Description = "50ft flexible hose, leak-proof design.",
                CategoryId = 12,
                Price = 27.50m,
                Qty = 30,
                ImageURL = "https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 24,
                Name = "Professional Pruning Shears",
                Description = "Sharp titanium blade for gardening.",
                CategoryId = 12,
                Price = 14.99m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 13. Toys ---
            new Product
            {
                Id = 25,
                Name = "Creative Building Blocks",
                Description = "1000 pieces set, compatible with major brands.",
                CategoryId = 13,
                Price = 39.95m,
                Qty = 40,
                ImageURL = "https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 26,
                Name = "Landscape Jigsaw Puzzle",
                Description = "1000 pieces puzzle, high quality print.",
                CategoryId = 13,
                Price = 18.50m,
                Qty = 12,
                ImageURL = "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 14. Lingerie ---
            new Product
            {
                Id = 27,
                Name = "Silk Satin Sleepwear",
                Description = "Premium 2-piece pajama set.",
                CategoryId = 14,
                Price = 55.00m,
                Qty = 28,
                ImageURL = "https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 28,
                Name = "Seamless Comfort Set",
                Description = "Invisible underwear pack of 3.",
                CategoryId = 14,
                Price = 24.90m,
                Qty = 18,
                ImageURL = "https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 15. Pets ---
            new Product
            {
                Id = 29,
                Name = "Premium Adult Dog Food",
                Description = "Chicken & Brown Rice Recipe, 15 lbs.",
                CategoryId = 15,
                Price = 42.99m,
                Qty = 35,
                ImageURL = "https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 30,
                Name = "Cat Scratching Post",
                Description = "Durable sisal pole with plush base.",
                CategoryId = 15,
                Price = 29.99m,
                Qty = 14,
                ImageURL = "https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 16. Nutrition ---
            new Product
            {
                Id = 31,
                Name = "Whey Protein Isolate",
                Description = "Chocolate flavor, 2 lbs, 25g protein.",
                CategoryId = 16,
                Price = 59.90m,
                Qty = 20,
                ImageURL = "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 32,
                Name = "Multivitamin Complex",
                Description = "120 capsules, immunity & energy support.",
                CategoryId = 16,
                Price = 19.95m,
                Qty = 10,
                ImageURL = "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 17. Clothing ---
            new Product
            {
                Id = 33,
                Name = "Cotton Crew Neck T-Shirt",
                Description = "100% Organic cotton, slim fit.",
                CategoryId = 17,
                Price = 19.99m,
                Qty = 60,
                ImageURL = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 34,
                Name = "Classic Denim Jacket",
                Description = "Vintage wash, button closure.",
                CategoryId = 17,
                Price = 64.90m,
                Qty = 9,
                ImageURL = "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format&fit=crop&w=600&h=600&q=80"
            },

            // --- 18. Technology ---
            new Product
            {
                Id = 35,
                Name = "Portable Bluetooth Speaker",
                Description = "Waterproof IPX7, 360 sound, 12h battery.",
                CategoryId = 18,
                Price = 49.99m,
                Qty = 25,
                ImageURL = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format&fit=crop&w=600&h=600&q=80"
            },
            new Product
            {
                Id = 36,
                Name = "USB-C Hub 7-in-1",
                Description = "HDMI 4K, USB 3.0, SD Card Reader.",
                CategoryId = 18,
                Price = 34.99m,
                Qty = 15,
                ImageURL = "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format&fit=crop&w=600&h=600&q=80"
            }
        );
    }
}