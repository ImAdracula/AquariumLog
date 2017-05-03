CREATE TABLE [Aquarium].[UnitsOfItemMeasure]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_UnitsOfItemMeasure_Name] ON [Aquarium].[UnitsOfItemMeasure] ([Name])