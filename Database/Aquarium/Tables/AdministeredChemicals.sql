CREATE TABLE [dbo].[AdministeredChemicals]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[LogEntryId] INT NOT NULL,
	[ChemicalTypeId] INT NOT NULL,
	[UnitOfMassMeasureId] INT NULL,
	[UnitOfVolumetricMeasureId] INT NULL,
	[UnitOfItemMeasureId] INT NULL,
    [Amount] DECIMAL(9, 3) NOT NULL, 
    CONSTRAINT [FK_AdministeredChemicals_LogEntries] FOREIGN KEY ([LogEntryId]) REFERENCES [LogEntries]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_ChemicalTypes] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [ChemicalTypes]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfMassMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [UnitsOfMassMeasure]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfVolumetricMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [UnitsOfVolumetricMeasure]([Id]), 
    CONSTRAINT [FK_AdministeredChemicals_UnitsOfItemMeasure] FOREIGN KEY ([ChemicalTypeId]) REFERENCES [UnitsOfItemMeasure]([Id]), 
    CONSTRAINT [CK_AdministeredChemicals_1UnitOfMeasureIsSelected] CHECK ((CASE WHEN [UnitOfMassMeasureId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [UnitOfVolumetricMeasureId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [UnitOfItemMeasureId] IS NULL THEN 0 ELSE 1 END) = 1)
)

GO

CREATE UNIQUE INDEX [IX_AdministeredChemicals_LogEntryAndChemicalType] ON [dbo].[AdministeredChemicals] ([LogEntryId], [ChemicalTypeId])