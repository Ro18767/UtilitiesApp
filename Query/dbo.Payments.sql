CREATE TABLE [dbo].[Payments] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (NEWID()) NOT NULL,
    [IdСontract] UNIQUEIDENTIFIER NOT NULL,
    [Price] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payments_To_Сontracts] FOREIGN KEY ([IdСontract]) REFERENCES [dbo].[Сontracts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

