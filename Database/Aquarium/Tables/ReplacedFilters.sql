CREATE TABLE [Aquarium].[ReplacedFilters]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[LogEntryId] INT NOT NULL,
	[FilterTypeId] INT NOT NULL,
	[Note] NVARCHAR(50) NULL,
    CONSTRAINT [FK_ReplacedFilters_LogEntries] FOREIGN KEY ([LogEntryId]) REFERENCES [Aquarium].[LogEntries]([Id]), 
    CONSTRAINT [FK_ReplacedFilters_FilterTypes] FOREIGN KEY ([FilterTypeId]) REFERENCES [Aquarium].[FilterTypes]([Id]), 
)
