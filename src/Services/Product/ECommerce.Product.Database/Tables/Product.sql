CREATE TABLE [dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Code] NVARCHAR(MAX) NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] MONEY NOT NULL DEFAULT 1000, 
    [Weight] FLOAT NOT NULL, 
    [Height] FLOAT NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [slug] NCHAR(50) NULL, 
    [Discount] FLOAT NULL,  
    [BrandId] UNIQUEIDENTIFIER NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDelete] BIT NOT NULL, 
    [CreateAt] DATETIME NULL, 
    [CreateBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_Product_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Brand]([Id])
)
