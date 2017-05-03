CREATE TABLE [dbo].[UnitsOfMassMeasure]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[VolumeInGrams] DECIMAL(9, 3) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_UnitsOfMassMeasure_Name] ON [dbo].[UnitsOfMassMeasure] ([Name])