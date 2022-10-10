CREATE TABLE [dbo].[ProductOptions]
(
	[ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [OptionId] UNIQUEIDENTIFIER NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_ProductOptions_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_ProductOptions_Option] FOREIGN KEY ([OptionId]) REFERENCES [Option]([Id]) 
)
