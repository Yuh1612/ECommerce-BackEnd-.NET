CREATE TABLE [dbo].[ProductCategories]
(
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
	[CategoryId] UNIQUEIDENTIFIER NOT NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([ProductId], [CategoryId]) ,
    CONSTRAINT [FK_ProductCategories_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_ProductCategories_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id])
)
