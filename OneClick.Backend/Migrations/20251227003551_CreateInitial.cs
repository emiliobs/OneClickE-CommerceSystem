using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Clothing" },
                    { 3, "Books" },
                    { 4, "Home & Garden" },
                    { 5, "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[,]
                {
                    { 1, 1, "15.6 inch, 8GB RAM, 256GB SSD", "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400&h=300&fit=crop", "Laptop HP", 899.99m, 15 },
                    { 2, 1, "Ergonomic design, 2.4GHz wireless", "https://images.unsplash.com/photo-1527814050087-3793815479db?w=400&h=300&fit=crop", "Wireless Mouse", 25.50m, 50 },
                    { 3, 1, "Fast charging, 6ft length", "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=400&h=300&fit=crop", "USB-C Cable", 12.99m, 100 },
                    { 4, 2, "100% Cotton, Size M", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400&h=300&fit=crop", "T-Shirt Blue", 19.99m, 30 },
                    { 5, 2, "Denim, Regular fit", "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400&h=300&fit=crop", "Jeans Classic", 49.99m, 20 },
                    { 6, 2, "Warm and waterproof", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400&h=300&fit=crop", "Winter Jacket", 89.99m, 10 },
                    { 7, 3, "Complete guide for beginners", "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=300&fit=crop", "C# Programming Guide", 35.00m, 25 },
                    { 8, 3, "By Robert C. Martin", "https://images.unsplash.com/photo-1541963463532-d68292c34b19?w=400&h=300&fit=crop", "Clean Code", 42.50m, 18 },
                    { 9, 3, "Essential guide", "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400&h=300&fit=crop", "Design Patterns", 38.99m, 22 },
                    { 10, 4, "12-cup programmable", "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400&h=300&fit=crop", "Coffee Maker", 79.99m, 12 },
                    { 11, 4, "5-piece essential set", "https://images.unsplash.com/photo-1591727884967-5e6b8f19123c?w=400&h=300&fit=crop", "Garden Tools Set", 45.00m, 8 },
                    { 12, 4, "Adjustable brightness", "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=400&h=300&fit=crop", "LED Desk Lamp", 32.99m, 35 },
                    { 13, 5, "Non-slip, eco-friendly", "https://images.unsplash.com/photo-1599901860904-17e6ed7083a0?w=400&h=300&fit=crop", "Yoga Mat", 28.50m, 40 },
                    { 14, 5, "5-25 lbs adjustable", "https://images.unsplash.com/photo-1534367507877-0edd93bd013b?w=400&h=300&fit=crop", "Dumbbells Set", 120.00m, 6 },
                    { 15, 5, "Lightweight, Size 10", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=300&fit=crop", "Running Shoes", 85.00m, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
