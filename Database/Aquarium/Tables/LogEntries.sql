CREATE TABLE [dbo].[LogEntries]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[TimeStamp] DATETIME NOT NULL,
	[PotentialOfHydrogen] TINYINT NULL, 
    CONSTRAINT [CK_LogEntries_PotentialOfHydrogenIs14OrLess] CHECK ([PotentialOfHydrogen] IS NULL OR [PotentialOfHydrogen] <= 14)
)

GO

CREATE INDEX [IX_LogEntries_TimeStamp] ON [dbo].[LogEntries] ([TimeStamp])