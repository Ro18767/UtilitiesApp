﻿CREATE TABLE [dbo].[Addresses]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Value] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
)
