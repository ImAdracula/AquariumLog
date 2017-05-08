CREATE TABLE [Aquarium].[AdministeredChemicals]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[LogEntryId] INT NOT NULL,
	[ChemicalTypeId] INT NOT NULL,
	[UnitOfMassMeasureId] INT NULL,
	[UnitOfVolumetricMeasureId] INT NULL,
	[UnitOfItemMeasureId] INT NULL,
    [Amount] DECIMAL(9, 3) NOT NULL, 
    CONSTRAINT [FK_AdministeredChemicals_LogEntries] FOREIGN KEY ([LogEntryId]) REFERENCES [Aquarium].[LogEntries]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_ChemicalTypes] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [Aquarium].[ChemicalTypes]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfMassMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [Aquarium].[UnitsOfMassMeasure]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfVolumetricMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [Aquarium].[UnitsOfVolumetricMeasure]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfItemMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [Aquarium].[UnitsOfItemMeasure]([Id]), 
    CONSTRAINT [CK_AdministeredChemicals_1UnitOfMeasureIsSelected] CHECK ((CASE WHEN [UnitOfMassMeasureId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [UnitOfVolumetricMeasureId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [UnitOfItemMeasureId] IS NULL THEN 0 ELSE 1 END) = 1)
)

GO

CREATE UNIQUE INDEX [IX_AdministeredChemicals_LogEntryAndChemicalType] ON [Aquarium].[AdministeredChemicals] ([LogEntryId], [ChemicalTypeId])