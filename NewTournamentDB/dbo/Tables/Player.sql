CREATE TABLE [dbo].[Player] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [Leagueid]     INT NOT NULL,
    [MembershipId] INT NOT NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK__Player__leagueid__4E88ABD4] FOREIGN KEY ([Leagueid]) REFERENCES [dbo].[League] ([id]),
    CONSTRAINT [FK_Player_Membership] FOREIGN KEY ([MembershipId]) REFERENCES [dbo].[Membership] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Player]
    ON [dbo].[Player]([MembershipId] ASC, [Leagueid] ASC);

