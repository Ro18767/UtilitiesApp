CREATE TABLE [dbo].[Consumptions] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (NEWID()) NOT NULL,
    [IdСontract] UNIQUEIDENTIFIER NOT NULL,
	[Amount] INT NOT NULL,
    [Price] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Consumptions_To_Сontracts] FOREIGN KEY ([IdСontract]) REFERENCES [dbo].[Сontracts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

