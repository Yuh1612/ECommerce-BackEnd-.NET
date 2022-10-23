CREATE TABLE [dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [Code] UNIQUEIDENTIFIER NOT NULL,
    [ShopId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] MONEY NOT NULL DEFAULT 1000,
    [Quantity] INT NOT NULL DEFAULT 0, 
    [Weight] FLOAT NOT NULL, 
    [Height] FLOAT NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [slug] NCHAR(50) NULL, 
    [Discount] FLOAT NULL,  
    [BrandId] UNIQUEIDENTIFIER NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    [CreatedOn] DATETIME NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [FK_Product_Shop] FOREIGN KEY ([ShopId]) REFERENCES [Shop]([Id]), 
    CONSTRAINT [FK_Product_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Brand]([Id])
)
