CREATE TABLE [dbo].[UnitsOfItemMeasure]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_UnitsOfItemMeasure_Name] ON [dbo].[UnitsOfItemMeasure] ([Name])