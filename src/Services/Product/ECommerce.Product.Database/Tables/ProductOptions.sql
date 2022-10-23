CREATE TABLE [dbo].[ProductOptions]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [OptionId] UNIQUEIDENTIFIER NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_ProductOptions_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_ProductOptions_Option] FOREIGN KEY ([OptionId]) REFERENCES [Option]([Id]) 
)
