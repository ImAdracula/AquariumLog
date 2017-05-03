CREATE TABLE [dbo].[ChemicalTypes]
(
	[Id] INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_ChemicalTypes_Name] ON [dbo].[ChemicalTypes] ([Name])