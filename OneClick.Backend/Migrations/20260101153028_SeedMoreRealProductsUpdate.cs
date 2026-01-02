using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreRealProductsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Titanium design, A17 Pro chip, the most powerful iPhone yet.", "https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format=80", "iPhone 15 Pro Max", 1199.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price", "Qty" },
                values: new object[] { "Supercharged by M2. Strikingly thin and fast.", 1099.00m, 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Ultimate liquid wax for a deep mirror-like shine.", "Meguiar's Car Wax", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Secure your driving with 24/7 loop recording.", "4K Dash Cam Front/Rear" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Instantly quenches dry skin for a healthy glow.", "Hydro Boost Gel Cream", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Name", "Price", "Qty" },
                values: new object[] { "Brightening serum for uneven skin tone.", "Vitamin C Serum", 34.00m, 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Responsive running shoes for road running.", "Nike Air Zoom Pegasus", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price", "Qty" },
                values: new object[] { "Minimalist leather sneakers for everyday wear.", "Classic White Sneakers", 89.90m, 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Crunchy clusters with almonds and seeds.", "Organic Honey Granola", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Qty" },
                values: new object[] { "Cold-pressed, rich flavor perfect for salads.", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Long-lasting color with a hydrating formula.", "Matte Velvet Lipstick" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dramatic volume without clumping.", "Volumizing Mascara" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "FIFA quality certified ball size 5.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Qty" },
                values: new object[] { "Eco-friendly material with alignment lines.", 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Set of 3 soy wax candles for ambiance.", "Luxury Scented Candles" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Deepen your connection with fun questions.", "Couples Card Game", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "18V power with 2 batteries and case.", "Cordless Drill Driver" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Socket wrench set for home and auto repair.", "46-Piece Tool Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "Tactile blue switches with custom lighting.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "Ultra-lightweight, 20,000 DPI sensor.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dimmable light with USB charging port.", "Modern LED Desk Lamp" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Ergonomic cervical pillow for neck pain relief.", "Memory Foam Pillow" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "Name" },
                values: new object[] { "50ft flexible hose, leak-proof design.", "Expandable Garden Hose" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Sharp titanium blade for gardening.", "Professional Pruning Shears" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "Name" },
                values: new object[] { "1000 pieces set, compatible with major brands.", "Creative Building Blocks" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "Name" },
                values: new object[] { "1000 pieces puzzle, high quality print.", "Landscape Jigsaw Puzzle" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Premium 2-piece pajama set.", "Silk Satin Sleepwear" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Invisible underwear pack of 3.", "Seamless Comfort Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Chicken & Brown Rice Recipe, 15 lbs.", "Premium Adult Dog Food" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "Description",
                value: "Durable sisal pole with plush base.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Chocolate flavor, 2 lbs, 25g protein.", "Whey Protein Isolate" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "Name" },
                values: new object[] { "120 capsules, immunity & energy support.", "Multivitamin Complex" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "Name" },
                values: new object[] { "100% Organic cotton, slim fit.", "Cotton Crew Neck T-Shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Vintage wash, button closure.", "Classic Denim Jacket" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Waterproof IPX7, 360 sound, 12h battery.", "Portable Bluetooth Speaker" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "Name" },
                values: new object[] { "HDMI 4K, USB 3.0, SD Card Reader.", "USB-C Hub 7-in-1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "Description", "Price", "Qty" },
                values: new object[] { "Supercharged by M2. 13.6-inch Liquid Retina display.", 1199.00m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Ultimate protection and shine for your vehicle.", "Premium Car Wax", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Front and rear recording, night vision included.", "4K Dash Cam" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "24-hour hydration for sensitive skin.", "Hydrating Moisturizer", 32.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Name", "Price", "Qty" },
                values: new object[] { "Brightens skin tone and reduces fine lines.", "Vitamin C Glow Serum", 45.00m, 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Lightweight cushioning for long distance runs.", "Pro Running Shoes", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price", "Qty" },
                values: new object[] { "Classic white sneakers for everyday wear.", "Urban Sneakers", 79.90m, 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Organic honey, nuts, and oats blend.", "Artisan Granola", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Qty" },
                values: new object[] { "Cold-pressed, imported from Italy.", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Long-lasting color with a creamy finish.", "Velvet Matte Lipstick" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Waterproof formula for dramatic lashes.", "Volume Mascara" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Official size and weight, high durability.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Qty" },
                values: new object[] { "Eco-friendly material with carrying strap.", 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Set of 3 scented candles for relaxation.", "Aromatherapy Candles" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "Qty" },
                values: new object[] { "Fun and romantic card game for date nights.", "Couples Board Game", 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "18V power with two rechargeable batteries.", "Cordless Drill Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "46-piece socket and wrench set.", "Mechanics Tool Kit" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "Blue switches, fully customizable lighting.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "Ultra-lightweight, 16000 DPI sensor.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "Name" },
                values: new object[] { "LED with wireless charging base.", "Modern Desk Lamp" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Memory foam for better neck support.", "Ergonomic Pillow" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "Name" },
                values: new object[] { "50ft flexible, kink-free water hose.", "Heavy Duty Hose" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Sharp stainless steel pruning shears.", "Garden Shears" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "Name" },
                values: new object[] { "500-piece creative construction set.", "Building Blocks Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "Name" },
                values: new object[] { "1000-piece puzzle, landscape theme.", "Scenic Jigsaw Puzzle" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Soft and breathable 2-piece lounge set.", "Silk Sleepwear Set" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Pack of 3 seamless undergarments.", "Cotton Essentials Pack" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Chicken and brown rice formula, 5kg.", "Premium Dog Food" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "Description",
                value: "Durable sisal rope with plush base.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Chocolate flavor, 2lb tub.", "Isolate Whey Protein" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "Name" },
                values: new object[] { "120 capsules, immunity support.", "Daily Multivitamin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "Name" },
                values: new object[] { "100% Organic cotton, regular fit.", "Classic White Tee" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Vintage wash, button-up front.", "Denim Jacket" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Waterproof, 360-degree sound.", "Bluetooth Speaker" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "Name" },
                values: new object[] { "7-in-1 connectivity for laptops.", "USB-C Hub Adapter" });
        }
    }
}
