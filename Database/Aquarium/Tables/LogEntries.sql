CREATE TABLE [dbo].[LogEntries]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[TimeStamp] DATETIME NOT NULL
)

GO

CREATE INDEX [IX_LogEntries_TimeStamp] ON [dbo].[LogEntries] ([TimeStamp])