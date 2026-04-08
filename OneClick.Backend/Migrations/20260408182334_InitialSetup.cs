using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Cars" },
                    { 3, "Beauty" },
                    { 4, "Footwear" },
                    { 5, "Food" },
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[,]
                {
                    { 1, 1, "Apple smartphone, 128GB", "https://images.unsplash.com/photo-1695048133142-1a20484d2569?q=80&w=600", "iPhone 15", 899.00m, 20 },
                    { 2, 1, "Apple laptop, 8GB/256GB", "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?q=80&w=600", "MacBook Air 13 M2", 1199.00m, 8 },
                    { 3, 2, "Shampoo, wax and microfiber towels", "https://images.unsplash.com/photo-1607860108855-64acf2078ed9?q=80&w=600", "Car Care Kit", 39.99m, 35 },
                    { 4, 2, "1080p dashboard camera with loop recording", "https://images.unsplash.com/photo-1590333746438-d81ff1590ce3?q=80&w=600", "Dash Cam HD", 69.99m, 12 },
                    { 5, 3, "Daily moisturizer for all skin types", "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?q=80&w=600", "Hydrating Face Cream", 22.50m, 40 },
                    { 6, 3, "Brightening 10% serum", "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?q=80&w=600", "Vitamin C Serum", 18.99m, 15 },
                    { 7, 4, "Lightweight breathable trainers", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=600", "Men's Running Shoes", 79.90m, 25 },
                    { 8, 4, "Comfort everyday sneakers", "https://images.unsplash.com/photo-1595950653106-6c9ebd614d3a?q=80&w=600", "Women's Casual Sneakers", 59.90m, 10 },
                    { 9, 5, "Whole-grain oats with nuts", "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?q=80&w=600", "Organic Granola 500g", 6.99m, 50 },
                    { 10, 5, "Extra virgin, cold pressed", "https://images.unsplash.com/photo-1474979266404-7cadd259c308?q=80&w=600", "Olive Oil 1L", 9.99m, 30 },
                    { 11, 6, "Long-wear, natural shade", "https://images.unsplash.com/photo-1586495777744-4413f21062fa?q=80&w=600", "Matte Lipstick", 12.99m, 35 },
                    { 12, 6, "Water-resistant, volume effect", "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?q=80&w=600", "Black Mascara", 11.49m, 22 },
                    { 13, 7, "PU match ball", "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?q=80&w=600", "Football Size 5", 29.99m, 60 },
                    { 14, 7, "Non-slip exercise mat", "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?q=80&w=600", "Yoga Mat 6mm", 24.50m, 8 },
                    { 15, 8, "Scented candles set for ambience", "https://images.unsplash.com/photo-1602826347632-009a584de633?q=80&w=600", "Romance Candle Set", 19.99m, 25 },
                    { 16, 8, "Couples card & board game", "https://images.unsplash.com/photo-1632501641765-e568d28b0015?q=80&w=600", "Romantic Board Game", 17.50m, 12 },
                    { 17, 9, "2 batteries, fast charger", "https://images.unsplash.com/photo-1504148455328-c376907d081c?q=80&w=600", "Cordless Drill 18V", 89.00m, 14 },
                    { 18, 9, "Sockets, bits and ratchet", "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?q=80&w=600", "Tool Set 46pcs", 39.99m, 40 },
                    { 19, 10, "Linear switches, full-size", "https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?q=80&w=600", "Mechanical Keyboard RGB", 69.90m, 18 },
                    { 20, 10, "Programmable buttons", "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?q=80&w=600", "Gaming Mouse 8K DPI", 29.90m, 45 },
                    { 21, 11, "Adjustable arm, warm/cool light", "https://images.unsplash.com/photo-1534073828943-f801091a7d58?q=80&w=600", "LED Desk Lamp", 22.99m, 45 },
                    { 22, 11, "Ergonomic cervical pillow", "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?q=80&w=600", "Memory Foam Pillow", 34.90m, 16 },
                    { 23, 12, "Flexible anti-kink hose", "https://images.unsplash.com/photo-1596707328646-778832a8747f?q=80&w=600", "Garden Hose 15m", 19.99m, 30 },
                    { 24, 12, "Bypass pruner stainless steel", "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?q=80&w=600", "Pruning Shears", 12.50m, 25 },
                    { 25, 13, "Compatible bricks 500 pcs", "https://images.unsplash.com/photo-1587654780291-39c940483713?q=80&w=600", "Building Blocks Set", 24.99m, 40 },
                    { 26, 13, "Landscape illustration", "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?q=80&w=600", "Puzzle 1000 Pieces", 14.99m, 12 },
                    { 27, 14, "Two-piece loungewear set", "https://images.unsplash.com/photo-1594967384738-9e63e2621746?q=80&w=600", "Soft Sleepwear Set", 29.90m, 28 },
                    { 28, 14, "Comfort fit essentials", "https://images.unsplash.com/photo-1596489392231-15b565780365?q=80&w=600", "Seamless Undergarments", 19.90m, 18 },
                    { 29, 15, "Chicken & rice formula", "https://images.unsplash.com/photo-1589924691195-41432c84c161?q=80&w=600", "Dry Dog Food 3kg", 16.99m, 35 },
                    { 30, 15, "Sisal rope post with base", "https://images.unsplash.com/photo-1545249390-6bdfa286032f?q=80&w=600", "Cat Scratching Post", 21.99m, 14 },
                    { 31, 16, "Vanilla flavor", "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?q=80&w=600", "Whey Protein 1kg", 32.90m, 20 },
                    { 32, 16, "Daily vitamins & minerals", "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?q=80&w=600", "Multivitamin 120 caps", 14.50m, 10 },
                    { 33, 17, "Classic fit, 100% cotton", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=600", "Men's Cotton T-Shirt", 14.99m, 60 },
                    { 34, 17, "Mid-wash, regular fit", "https://images.unsplash.com/photo-1551028719-00167b16eac5?q=80&w=600", "Women's Denim Jacket", 49.90m, 9 },
                    { 35, 18, "Portable speaker with mic", "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?q=80&w=600", "Bluetooth Speaker", 34.99m, 25 },
                    { 36, 18, "HDMI, USB 3.0, SD/MicroSD", "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?q=80&w=600", "USB-C Hub 6-in-1", 24.90m, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name_CategoryId",
                table: "Products",
                columns: new[] { "Name", "CategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
