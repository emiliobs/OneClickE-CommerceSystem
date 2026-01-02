using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreRealProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Titanium design, A17 Pro chip, 128GB.", "https://images.unsplash.com/photo-1696446701796-da61225697cc?auto=format=80", "iPhone 15 Pro", 999.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageURL", "Name" },
                values: new object[] { "Supercharged by M2. 13.6-inch Liquid Retina display.", "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format=80", "MacBook Air M2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Ultimate protection and shine for your vehicle.", "https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format=80", "Premium Car Wax", 24.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Front and rear recording, night vision included.", "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format=80", "4K Dash Cam", 89.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "24-hour hydration for sensitive skin.", "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format=80", "Hydrating Moisturizer", 32.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Brightens skin tone and reduces fine lines.", "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format=80", "Vitamin C Glow Serum", 45.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Lightweight cushioning for long distance runs.", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format=80", "Pro Running Shoes", 129.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Classic white sneakers for everyday wear.", "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format=80", "Urban Sneakers", 79.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Organic honey, nuts, and oats blend.", "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format=80", "Artisan Granola", 8.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Cold-pressed, imported from Italy.", "https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format=80", "Extra Virgin Olive Oil", 18.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Long-lasting color with a creamy finish.", "https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format=80", "Velvet Matte Lipstick", 22.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Waterproof formula for dramatic lashes.", "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format=80", "Volume Mascara", 16.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Official size and weight, high durability.", "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format=80", "Pro Match Football", 34.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Eco-friendly material with carrying strap.", "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format=80", "Non-Slip Yoga Mat", 29.95m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Set of 3 scented candles for relaxation.", "https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format=80", "Aromatherapy Candles", 24.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Fun and romantic card game for date nights.", "https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format=80", "Couples Board Game", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageURL", "Name" },
                values: new object[] { "18V power with two rechargeable batteries.", "https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format=80", "Cordless Drill Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "46-piece socket and wrench set.", "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format=80", "Mechanics Tool Kit", 45.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Blue switches, fully customizable lighting.", "https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format=80", "RGB Mechanical Keyboard", 79.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Ultra-lightweight, 16000 DPI sensor.", "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format=80", "Wireless Gaming Mouse", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "LED with wireless charging base.", "https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format=80", "Modern Desk Lamp", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Memory foam for better neck support.", "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format=80", "Ergonomic Pillow", 29.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "50ft flexible, kink-free water hose.", "https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format=80", "Heavy Duty Hose", 27.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Sharp stainless steel pruning shears.", "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format=80", "Garden Shears", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "500-piece creative construction set.", "https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format=80", 39.95m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "1000-piece puzzle, landscape theme.", "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format=80", "Scenic Jigsaw Puzzle", 18.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Soft and breathable 2-piece lounge set.", "https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format=80", "Silk Sleepwear Set", 55.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Pack of 3 seamless undergarments.", "https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format=80", "Cotton Essentials Pack", 24.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Chicken and brown rice formula, 5kg.", "https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format=80", "Premium Dog Food", 42.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "Durable sisal rope with plush base.", "https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format=80", 29.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Chocolate flavor, 2lb tub.", "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format=80", "Isolate Whey Protein", 59.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "120 capsules, immunity support.", "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format=80", "Daily Multivitamin", 19.95m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "100% Organic cotton, regular fit.", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format=80", "Classic White Tee", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Vintage wash, button-up front.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format=80", "Denim Jacket", 64.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "Waterproof, 360-degree sound.", "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format=80", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "7-in-1 connectivity for laptops.", "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format=80", "USB-C Hub Adapter", 34.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Apple smartphone, 128GB", "https://images.unsplash.com/photo-1696446701796-da61225697cc?auto=format&fit=crop&w=600&q=80", "iPhone 15", 899.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageURL", "Name" },
                values: new object[] { "Apple laptop, 8GB/256GB", "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format&fit=crop&w=600&q=80", "MacBook Air 13 M2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Shampoo, wax and microfiber towels", "https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format&fit=crop&w=600&q=80", "Car Care Kit", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "1080p dashboard camera with loop recording", "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format&fit=crop&w=600&q=80", "Dash Cam HD", 69.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Daily moisturizer for all skin types", "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format&fit=crop&w=600&q=80", "Hydrating Face Cream", 22.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Brightening 10% serum", "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format&fit=crop&w=600&q=80", "Vitamin C Serum", 18.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Lightweight breathable trainers", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&q=80", "Men's Running Shoes", 79.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Comfort everyday sneakers", "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format&fit=crop&w=600&q=80", "Women's Casual Sneakers", 59.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Whole-grain oats with nuts", "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format&fit=crop&w=600&q=80", "Organic Granola 500g", 6.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Extra virgin, cold pressed", "https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format&fit=crop&w=600&q=80", "Olive Oil 1L", 9.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Long-wear, natural shade", "https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format&fit=crop&w=600&q=80", "Matte Lipstick", 12.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Water-resistant, volume effect", "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format&fit=crop&w=600&q=80", "Black Mascara", 11.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "PU match ball", "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format&fit=crop&w=600&q=80", "Football Size 5", 29.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Non-slip exercise mat", "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format&fit=crop&w=600&q=80", "Yoga Mat 6mm", 24.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Scented candles set for ambience", "https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format&fit=crop&w=600&q=80", "Romance Candle Set", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Couples card & board game", "https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format&fit=crop&w=600&q=80", "Romantic Board Game", 17.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageURL", "Name" },
                values: new object[] { "2 batteries, fast charger", "https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format&fit=crop&w=600&q=80", "Cordless Drill 18V" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Sockets, bits and ratchet", "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format&fit=crop&w=600&q=80", "Tool Set 46pcs", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Linear switches, full-size", "https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format&fit=crop&w=600&q=80", "Mechanical Keyboard RGB", 69.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Programmable buttons", "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format&fit=crop&w=600&q=80", "Gaming Mouse 8K DPI", 29.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Adjustable arm, warm/cool light", "https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format&fit=crop&w=600&q=80", "LED Desk Lamp", 22.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Ergonomic cervical pillow", "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format&fit=crop&w=600&q=80", "Memory Foam Pillow", 34.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Flexible anti-kink hose", "https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format&fit=crop&w=600&q=80", "Garden Hose 15m", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Bypass pruner stainless steel", "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format&fit=crop&w=600&q=80", "Pruning Shears", 12.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "Compatible bricks 500 pcs", "https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format&fit=crop&w=600&q=80", 24.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Landscape illustration", "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format&fit=crop&w=600&q=80", "Puzzle 1000 Pieces", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Two-piece loungewear set", "https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format&fit=crop&w=600&q=80", "Soft Sleepwear Set", 29.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Comfort fit essentials", "https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format&fit=crop&w=600&q=80", "Seamless Undergarments", 19.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Chicken & rice formula", "https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format&fit=crop&w=600&q=80", "Dry Dog Food 3kg", 16.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "Sisal rope post with base", "https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format&fit=crop&w=600&q=80", 21.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Vanilla flavor", "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format&fit=crop&w=600&q=80", "Whey Protein 1kg", 32.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Daily vitamins & minerals", "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format&fit=crop&w=600&q=80", "Multivitamin 120 caps", 14.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Classic fit, 100% cotton", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&q=80", "Men's Cotton T-Shirt", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Mid-wash, regular fit", "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format&fit=crop&w=600&q=80", "Women's Denim Jacket", 49.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "ImageURL", "Price" },
                values: new object[] { "Portable speaker with mic", "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format&fit=crop&w=600&q=80", 34.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "HDMI, USB 3.0, SD/MicroSD", "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format&fit=crop&w=600&q=80", "USB-C Hub 6-in-1", 24.90m });
        }
    }
}
