﻿CREATE TABLE [Aquarium].[FilterTypes]
(
	[Id] INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_FilterTypes_Name] ON [Aquarium].[FilterTypes] ([Name])