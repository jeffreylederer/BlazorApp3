CREATE TABLE [dbo].[UserLeague] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [UserId]   INT          NOT NULL,
    [LeagueId] INT          NOT NULL,
    [Roles]    VARCHAR (50) NULL,
    CONSTRAINT [PK_UserLeague] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_UserLeague_League] FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[League] ([id]),
    CONSTRAINT [FK_UserLeague_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserLeague]
    ON [dbo].[UserLeague]([UserId] ASC, [LeagueId] ASC);

