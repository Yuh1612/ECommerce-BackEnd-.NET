CREATE TABLE [dbo].[ProductCategories]
(
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
	[CategoryId] UNIQUEIDENTIFIER NOT NULL, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreateAt] DATETIME NULL, 
    [CreateBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_ProductCategories_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_ProductCategories_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]) 
)
