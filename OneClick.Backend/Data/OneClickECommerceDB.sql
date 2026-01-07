CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(1000) NOT NULL,
    [ImageURL] nvarchar(500) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Qty] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [OrderStatus] nvarchar(max) NOT NULL,
    [ShippingAddress] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [CartItems] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [Name])
VALUES (1, N'Apple'),
(2, N'Cars'),
(3, N'Beauty'),
(4, N'Footwear'),
(5, N'Food'),
(6, N'Cosmetics'),
(7, N'Sports'),
(8, N'Erotic'),
(9, N'Hardware'),
(10, N'Gamer'),
(11, N'Home'),
(12, N'Garden'),
(13, N'Toys'),
(14, N'Lingerie'),
(15, N'Pets'),
(16, N'Nutrition'),
(17, N'Clothing'),
(18, N'Technology');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'PasswordHash', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Email], [Name], [PasswordHash], [Role])
VALUES (1, N'emilio@yopmail.com', N'Emilio Admin', N'123456', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'PasswordHash', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Description', N'ImageURL', N'Name', N'Price', N'Qty') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [CategoryId], [Description], [ImageURL], [Name], [Price], [Qty])
VALUES (1, 1, N'Titanium design, A17 Pro chip, the most powerful iPhone yet.', N'https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format=80', N'iPhone 15 Pro Max', 1199.0, 20),
(2, 1, N'Supercharged by M2. Strikingly thin and fast.', N'https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format=80', N'MacBook Air M2', 1099.0, 15),
(3, 2, N'Ultimate liquid wax for a deep mirror-like shine.', N'https://images.unsplash.com/photo-1601362840469-51e4d8d58785?auto=format=80', N'Meguiar''s Car Wax', 24.99, 50),
(4, 2, N'Secure your driving with 24/7 loop recording.', N'https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format=80', N'4K Dash Cam Front/Rear', 89.5, 12),
(5, 3, N'Instantly quenches dry skin for a healthy glow.', N'https://images.unsplash.com/photo-1620916566398-39f1143ab7be?auto=format=80', N'Hydro Boost Gel Cream', 19.99, 40),
(6, 3, N'Brightening serum for uneven skin tone.', N'https://images.unsplash.com/photo-1620916297397-a4a5402a3c6c?auto=format=80', N'Vitamin C Serum', 34.0, 25),
(7, 4, N'Responsive running shoes for road running.', N'https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format=80', N'Nike Air Zoom Pegasus', 129.99, 30),
(8, 4, N'Minimalist leather sneakers for everyday wear.', N'https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format=80', N'Classic White Sneakers', 89.9, 18),
(9, 5, N'Crunchy clusters with almonds and seeds.', N'https://images.unsplash.com/photo-1517093728432-a0440f8d45ca?auto=format=80', N'Organic Honey Granola', 8.5, 60),
(10, 5, N'Cold-pressed, rich flavor perfect for salads.', N'https://images.unsplash.com/photo-1474979266404-7cadd259c308?auto=format=80', N'Extra Virgin Olive Oil', 18.99, 45),
(11, 6, N'Long-lasting color with a hydrating formula.', N'https://images.unsplash.com/photo-1586495777744-4413f21062fa?auto=format=80', N'Matte Velvet Lipstick', 22.0, 35),
(12, 6, N'Dramatic volume without clumping.', N'https://images.unsplash.com/photo-1631214524020-7e18db9a8f92?auto=format=80', N'Volumizing Mascara', 16.5, 22),
(13, 7, N'FIFA quality certified ball size 5.', N'https://images.unsplash.com/photo-1614632537423-1e6c2e7e0aab?auto=format=80', N'Pro Match Football', 34.99, 60),
(14, 7, N'Eco-friendly material with alignment lines.', N'https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format=80', N'Non-Slip Yoga Mat', 29.95, 15),
(15, 8, N'Set of 3 soy wax candles for ambiance.', N'https://images.unsplash.com/photo-1602826347632-009a584de633?auto=format=80', N'Luxury Scented Candles', 24.99, 25),
(16, 8, N'Deepen your connection with fun questions.', N'https://images.unsplash.com/photo-1630260655866-e3256037b605?auto=format=80', N'Couples Card Game', 19.99, 20),
(17, 9, N'18V power with 2 batteries and case.', N'https://images.unsplash.com/photo-1504148455328-c376907d081c?auto=format=80', N'Cordless Drill Driver', 89.0, 14),
(18, 9, N'Socket wrench set for home and auto repair.', N'https://images.unsplash.com/photo-1581235720704-06d3acfcb36f?auto=format=80', N'46-Piece Tool Set', 45.0, 40),
(19, 10, N'Tactile blue switches with custom lighting.', N'https://images.unsplash.com/photo-1587829741301-dc798b91a602?auto=format=80', N'RGB Mechanical Keyboard', 79.99, 18),
(20, 10, N'Ultra-lightweight, 20,000 DPI sensor.', N'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?auto=format=80', N'Wireless Gaming Mouse', 49.99, 45),
(21, 11, N'Dimmable light with USB charging port.', N'https://images.unsplash.com/photo-1534073828943-f801091a7d58?auto=format=80', N'Modern LED Desk Lamp', 39.99, 45),
(22, 11, N'Ergonomic cervical pillow for neck pain relief.', N'https://images.unsplash.com/photo-1584100936595-c0654b55a2e6?auto=format=80', N'Memory Foam Pillow', 29.99, 16),
(23, 12, N'50ft flexible hose, leak-proof design.', N'https://images.unsplash.com/photo-1596707328646-778832a8747f?auto=format=80', N'Expandable Garden Hose', 27.5, 30),
(24, 12, N'Sharp titanium blade for gardening.', N'https://images.unsplash.com/photo-1622374274291-3e4b77f32997?auto=format=80', N'Professional Pruning Shears', 14.99, 25),
(25, 13, N'1000 pieces set, compatible with major brands.', N'https://images.unsplash.com/photo-1587654780291-39c940483713?auto=format=80', N'Creative Building Blocks', 39.95, 40),
(26, 13, N'1000 pieces puzzle, high quality print.', N'https://images.unsplash.com/photo-1610419885843-0c4a457493a7?auto=format=80', N'Landscape Jigsaw Puzzle', 18.5, 12),
(27, 14, N'Premium 2-piece pajama set.', N'https://images.unsplash.com/photo-1594967384738-9e63e2621746?auto=format=80', N'Silk Satin Sleepwear', 55.0, 28),
(28, 14, N'Invisible underwear pack of 3.', N'https://images.unsplash.com/photo-1596489392231-15b565780365?auto=format=80', N'Seamless Comfort Set', 24.9, 18),
(29, 15, N'Chicken & Brown Rice Recipe, 15 lbs.', N'https://images.unsplash.com/photo-1589924691195-41432c84c161?auto=format=80', N'Premium Adult Dog Food', 42.99, 35),
(30, 15, N'Durable sisal pole with plush base.', N'https://images.unsplash.com/photo-1545249390-6bdfa286032f?auto=format=80', N'Cat Scratching Post', 29.99, 14),
(31, 16, N'Chocolate flavor, 2 lbs, 25g protein.', N'https://images.unsplash.com/photo-1593095948071-474c5cc2989d?auto=format=80', N'Whey Protein Isolate', 59.9, 20),
(32, 16, N'120 capsules, immunity & energy support.', N'https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?auto=format=80', N'Multivitamin Complex', 19.95, 10),
(33, 17, N'100% Organic cotton, slim fit.', N'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format=80', N'Cotton Crew Neck T-Shirt', 19.99, 60),
(34, 17, N'Vintage wash, button closure.', N'https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format=80', N'Classic Denim Jacket', 64.9, 9),
(35, 18, N'Waterproof IPX7, 360 sound, 12h battery.', N'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format=80', N'Portable Bluetooth Speaker', 49.99, 25),
(36, 18, N'HDMI 4K, USB 3.0, SD Card Reader.', N'https://images.unsplash.com/photo-1630080644612-4b2eb00438a9?auto=format=80', N'USB-C Hub 7-in-1', 34.99, 15);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Description', N'ImageURL', N'Name', N'Price', N'Qty') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO


CREATE INDEX [IX_CartItems_ProductId] ON [CartItems] ([ProductId]);
GO


CREATE INDEX [IX_CartItems_UserId] ON [CartItems] ([UserId]);
GO


CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);
GO


CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO


CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
GO


CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
GO


CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO


CREATE UNIQUE INDEX [IX_Products_Name_CategoryId] ON [Products] ([Name], [CategoryId]);
GO


CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO


