CREATE TABLE [dbo].[ShopProducts]
(
	[ShopId] UNIQUEIDENTIFIER NOT NULL, 
    [ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 0, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreateAt] DATETIME NULL, 
    [CreateBy] UNIQUEIDENTIFIER NULL, 
    [UpdateAt] DATETIME NULL, 
    [UpdateBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_ShopProducts_Shop] FOREIGN KEY ([ShopId]) REFERENCES [Shop]([Id]), 
    CONSTRAINT [FK_ShopProducts_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]) 
)
