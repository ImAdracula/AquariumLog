CREATE TABLE [Aquarium].[UnitsOfVolumetricMeasure]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[VolumeInCubicCentimeters] DECIMAL(9, 3) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_UnitsOfVolumetricMeasure_Name] ON [Aquarium].[UnitsOfVolumetricMeasure] ([Name])