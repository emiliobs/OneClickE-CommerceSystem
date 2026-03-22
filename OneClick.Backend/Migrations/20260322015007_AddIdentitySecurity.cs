using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneClick.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentitySecurity : Migration
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
                        onDelete: ReferentialAction.Restrict);
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "STATIC-CONCURRENCY-STAMP-1", "emilio@yopmail.com", true, "Emilio", "Barrera", false, null, "EMILIO@YOPMAIL.COM", "EMILIO@YOPMAIL.COM", "123456", null, false, "Admin", "STATIC-SECURITY-STAMP-1", false, "emilio@yopmail.com" });

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
                    { 1, 1, "Titanium design, A17 Pro chip, the most powerful iPhone yet.", "https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format=80", "iPhone 15 Pro Max", 1199.00m, 20 },
                    { 2, 1, "Supercharged by M2. Strikingly thin and fast.", "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format=80", "MacBook Air M2", 1099.00m, 15 },
                    { 3, 2, "Ultimate liquid wax for a deep mirror-like shine.", "https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format=80", "Meguiar's Car Wax", 24.99m, 50 },
                    { 4, 2, "Secure your driving with 24/7 loop recording.", "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format=80", "4K Dash Cam Front/Rear", 89.50m, 12 },
                    { 5, 3, "Instantly quenches dry skin for a healthy glow.", "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format=80", "Hydro Boost Gel Cream", 19.99m, 40 },
                    { 6, 3, "Brightening serum for uneven skin tone.", "https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format=80", "Vitamin C Serum", 34.00m, 25 },
                    { 7, 4, "Responsive running shoes for road running.", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format=80", "Nike Air Zoom Pegasus", 129.99m, 30 },
                    { 8, 4, "Minimalist leather sneakers for everyday wear.", "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format=80", "Classic White Sneakers", 89.90m, 18 },
                    { 9, 5, "Crunchy clusters with almonds and seeds.", "https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format=80", "Organic Honey Granola", 8.50m, 60 },
                    { 10, 5, "Cold-pressed, rich flavor perfect for salads.", "https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format=80", "Extra Virgin Olive Oil", 18.99m, 45 },
                    { 11, 6, "Long-lasting color with a hydrating formula.", "https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format=80", "Matte Velvet Lipstick", 22.00m, 35 },
                    { 12, 6, "Dramatic volume without clumping.", "https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format=80", "Volumizing Mascara", 16.50m, 22 },
                    { 13, 7, "FIFA quality certified ball size 5.", "https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format=80", "Pro Match Football", 34.99m, 60 },
                    { 14, 7, "Eco-friendly material with alignment lines.", "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format=80", "Non-Slip Yoga Mat", 29.95m, 15 },
                    { 15, 8, "Set of 3 soy wax candles for ambiance.", "https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format=80", "Luxury Scented Candles", 24.99m, 25 },
                    { 16, 8, "Deepen your connection with fun questions.", "https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format=80", "Couples Card Game", 19.99m, 20 },
                    { 17, 9, "18V power with 2 batteries and case.", "https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format=80", "Cordless Drill Driver", 89.00m, 14 },
                    { 18, 9, "Socket wrench set for home and auto repair.", "https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format=80", "46-Piece Tool Set", 45.00m, 40 },
                    { 19, 10, "Tactile blue switches with custom lighting.", "https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format=80", "RGB Mechanical Keyboard", 79.99m, 18 },
                    { 20, 10, "Ultra-lightweight, 20,000 DPI sensor.", "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format=80", "Wireless Gaming Mouse", 49.99m, 45 },
                    { 21, 11, "Dimmable light with USB charging port.", "https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format=80", "Modern LED Desk Lamp", 39.99m, 45 },
                    { 22, 11, "Ergonomic cervical pillow for neck pain relief.", "https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format=80", "Memory Foam Pillow", 29.99m, 16 },
                    { 23, 12, "50ft flexible hose, leak-proof design.", "https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format=80", "Expandable Garden Hose", 27.50m, 30 },
                    { 24, 12, "Sharp titanium blade for gardening.", "https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format=80", "Professional Pruning Shears", 14.99m, 25 },
                    { 25, 13, "1000 pieces set, compatible with major brands.", "https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format=80", "Creative Building Blocks", 39.95m, 40 },
                    { 26, 13, "1000 pieces puzzle, high quality print.", "https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format=80", "Landscape Jigsaw Puzzle", 18.50m, 12 },
                    { 27, 14, "Premium 2-piece pajama set.", "https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format=80", "Silk Satin Sleepwear", 55.00m, 28 },
                    { 28, 14, "Invisible underwear pack of 3.", "https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format=80", "Seamless Comfort Set", 24.90m, 18 },
                    { 29, 15, "Chicken & Brown Rice Recipe, 15 lbs.", "https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format=80", "Premium Adult Dog Food", 42.99m, 35 },
                    { 30, 15, "Durable sisal pole with plush base.", "https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format=80", "Cat Scratching Post", 29.99m, 14 },
                    { 31, 16, "Chocolate flavor, 2 lbs, 25g protein.", "https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format=80", "Whey Protein Isolate", 59.90m, 20 },
                    { 32, 16, "120 capsules, immunity & energy support.", "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format=80", "Multivitamin Complex", 19.95m, 10 },
                    { 33, 17, "100% Organic cotton, slim fit.", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format=80", "Cotton Crew Neck T-Shirt", 19.99m, 60 },
                    { 34, 17, "Vintage wash, button closure.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format=80", "Classic Denim Jacket", 64.90m, 9 },
                    { 35, 18, "Waterproof IPX7, 360 sound, 12h battery.", "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format=80", "Portable Bluetooth Speaker", 49.99m, 25 },
                    { 36, 18, "HDMI 4K, USB 3.0, SD Card Reader.", "https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format=80", "USB-C Hub 7-in-1", 34.99m, 15 }
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
