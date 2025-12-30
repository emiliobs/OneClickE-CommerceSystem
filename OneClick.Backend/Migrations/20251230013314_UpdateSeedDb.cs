using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Apple");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cars");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Beauty");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Footwear");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Food");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Cosmetics" },
                    { 7, "Sports" },
                    { 8, "Erotic" },
                    { 9, "Hardware" },
                    { 10, "Gamer" },
                    { 11, "Home" },
                    { 12, "Garden" },
                    { 13, "Toys" },
                    { 14, "Lingerie" },
                    { 15, "Pets" },
                    { 16, "Nutrition" },
                    { 17, "Clothing" },
                    { 18, "Technology" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "Apple smartphone, 128GB", "https://picsum.photos/seed/iphone15/600/400", "iPhone 15", 899.00m, 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "Apple laptop, 8GB/256GB", "https://picsum.photos/seed/macbookair/600/400", "MacBook Air 13 M2", 1199.00m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 2, "Shampoo, wax and microfiber towels", "https://picsum.photos/seed/carcare/600/400", "Car Care Kit", 39.99m, 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "1080p dashboard camera with loop recording", "https://picsum.photos/seed/dashcam/600/400", "Dash Cam HD", 69.99m, 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 3, "Daily moisturizer for all skin types", "https://picsum.photos/seed/facecream/600/400", "Hydrating Face Cream", 22.50m, 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 3, "Brightening 10% serum", "https://picsum.photos/seed/vitcserum/600/400", "Vitamin C Serum", 18.99m, 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price" },
                values: new object[] { 4, "Lightweight breathable trainers", "https://picsum.photos/seed/runningshoes/600/400", "Men's Running Shoes", 79.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 4, "Comfort everyday sneakers", "https://picsum.photos/seed/casualsneakers/600/400", "Women's Casual Sneakers", 59.90m, 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 5, "Whole-grain oats with nuts", "https://picsum.photos/seed/granola/600/400", "Organic Granola 500g", 6.99m, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 5, "Extra virgin, cold pressed", "https://picsum.photos/seed/oliveoil/600/400", "Olive Oil 1L", 9.99m, 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 6, "Long-wear, natural shade", "https://picsum.photos/seed/lipstick/600/400", "Matte Lipstick", 12.99m, 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 6, "Water-resistant, volume effect", "https://picsum.photos/seed/mascara/600/400", "Black Mascara", 11.49m, 22 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 7, "PU match ball", "https://picsum.photos/seed/football/600/400", "Football Size 5", 29.99m, 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 7, "Non-slip exercise mat", "https://picsum.photos/seed/yogamat/600/400", "Yoga Mat 6mm", 24.50m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 8, "Scented candles set for ambience", "https://picsum.photos/seed/romancecandles/600/400", "Romance Candle Set", 19.99m, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[,]
                {
                    { 16, 8, "Couples card & board game", "https://picsum.photos/seed/romancegame/600/400", "Romantic Board Game", 17.50m, 12 },
                    { 17, 9, "2 batteries, fast charger", "https://picsum.photos/seed/drill/600/400", "Cordless Drill 18V", 89.00m, 14 },
                    { 18, 9, "Sockets, bits and ratchet", "https://picsum.photos/seed/toolset/600/400", "Tool Set 46pcs", 39.99m, 40 },
                    { 19, 10, "Linear switches, full-size", "https://picsum.photos/seed/keyboard/600/400", "Mechanical Keyboard RGB", 69.90m, 18 },
                    { 20, 10, "Programmable buttons", "https://picsum.photos/seed/gamingmouse/600/400", "Gaming Mouse 8K DPI", 29.90m, 45 },
                    { 21, 11, "Adjustable arm, warm/cool light", "https://picsum.photos/seed/desklamp/600/400", "LED Desk Lamp", 22.99m, 45 },
                    { 22, 11, "Ergonomic cervical pillow", "https://picsum.photos/seed/pillow/600/400", "Memory Foam Pillow", 34.90m, 16 },
                    { 23, 12, "Flexible anti-kink hose", "https://picsum.photos/seed/gardenhose/600/400", "Garden Hose 15m", 19.99m, 30 },
                    { 24, 12, "Bypass pruner stainless steel", "https://picsum.photos/seed/pruner/600/400", "Pruning Shears", 12.50m, 25 },
                    { 25, 13, "Compatible bricks 500 pcs", "https://picsum.photos/seed/blocks/600/400", "Building Blocks Set", 24.99m, 40 },
                    { 26, 13, "Landscape illustration", "https://picsum.photos/seed/puzzle/600/400", "Puzzle 1000 Pieces", 14.99m, 12 },
                    { 27, 14, "Two-piece loungewear set", "https://picsum.photos/seed/sleepwear/600/400", "Soft Sleepwear Set", 29.90m, 28 },
                    { 28, 14, "Comfort fit essentials", "https://picsum.photos/seed/undergarments/600/400", "Seamless Undergarments", 19.90m, 18 },
                    { 29, 15, "Chicken & rice formula", "https://picsum.photos/seed/dogfood/600/400", "Dry Dog Food 3kg", 16.99m, 35 },
                    { 30, 15, "Sisal rope post with base", "https://picsum.photos/seed/catpost/600/400", "Cat Scratching Post", 21.99m, 14 },
                    { 31, 16, "Vanilla flavor", "https://picsum.photos/seed/whey/600/400", "Whey Protein 1kg", 32.90m, 20 },
                    { 32, 16, "Daily vitamins & minerals", "https://picsum.photos/seed/multivitamin/600/400", "Multivitamin 120 caps", 14.50m, 10 },
                    { 33, 17, "Classic fit, 100% cotton", "https://picsum.photos/seed/tshirt/600/400", "Men's Cotton T-Shirt", 14.99m, 60 },
                    { 34, 17, "Mid-wash, regular fit", "https://picsum.photos/seed/denimjacket/600/400", "Women's Denim Jacket", 49.90m, 9 },
                    { 35, 18, "Portable speaker with mic", "https://picsum.photos/seed/speaker/600/400", "Bluetooth Speaker", 34.99m, 25 },
                    { 36, 18, "HDMI, USB 3.0, SD/MicroSD", "https://picsum.photos/seed/usbhub/600/400", "USB-C Hub 6-in-1", 24.90m, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Electronics");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Clothing");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Books");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Home & Garden");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Sports");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "15.6 inch, 8GB RAM, 256GB SSD", "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400&h=300&fit=crop", "Laptop HP", 899.99m, 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "Ergonomic design, 2.4GHz wireless", "https://images.unsplash.com/photo-1527814050087-3793815479db?w=400&h=300&fit=crop", "Wireless Mouse", 25.50m, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 1, "Fast charging, 6ft length", "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=400&h=300&fit=crop", "USB-C Cable", 12.99m, 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { "100% Cotton, Size M", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400&h=300&fit=crop", "T-Shirt Blue", 19.99m, 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 2, "Denim, Regular fit", "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400&h=300&fit=crop", "Jeans Classic", 49.99m, 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 2, "Warm and waterproof", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400&h=300&fit=crop", "Winter Jacket", 89.99m, 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price" },
                values: new object[] { 3, "Complete guide for beginners", "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=300&fit=crop", "C# Programming Guide", 35.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 3, "By Robert C. Martin", "https://images.unsplash.com/photo-1541963463532-d68292c34b19?w=400&h=300&fit=crop", "Clean Code", 42.50m, 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 3, "Essential guide", "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400&h=300&fit=crop", "Design Patterns", 38.99m, 22 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 4, "12-cup programmable", "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400&h=300&fit=crop", "Coffee Maker", 79.99m, 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 4, "5-piece essential set", "https://images.unsplash.com/photo-1591727884967-5e6b8f19123c?w=400&h=300&fit=crop", "Garden Tools Set", 45.00m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 4, "Adjustable brightness", "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=400&h=300&fit=crop", "LED Desk Lamp", 32.99m, 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 5, "Non-slip, eco-friendly", "https://images.unsplash.com/photo-1599901860904-17e6ed7083a0?w=400&h=300&fit=crop", "Yoga Mat", 28.50m, 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 5, "5-25 lbs adjustable", "https://images.unsplash.com/photo-1534367507877-0edd93bd013b?w=400&h=300&fit=crop", "Dumbbells Set", 120.00m, 6 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[] { 5, "Lightweight, Size 10", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=300&fit=crop", "Running Shoes", 85.00m, 14 });
        }
    }
}
