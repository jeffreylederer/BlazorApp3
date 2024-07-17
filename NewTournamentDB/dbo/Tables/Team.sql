CREATE TABLE [dbo].[Team] (
    [id]         INT      IDENTITY (1, 1) NOT NULL,
    [Skip]       INT      NULL,
    [ViceSkip]   INT      NULL,
    [Lead]       INT      NULL,
    [Leagueid]   INT      NOT NULL,
    [TeamNo]     INT      NOT NULL,
    [DivisionId] SMALLINT CONSTRAINT [DF_Team_Division] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK__Players] FOREIGN KEY ([Skip]) REFERENCES [dbo].[Player] ([id]),
    CONSTRAINT [FK__Players1] FOREIGN KEY ([ViceSkip]) REFERENCES [dbo].[Player] ([id]),
    CONSTRAINT [FK__Players2] FOREIGN KEY ([Lead]) REFERENCES [dbo].[Player] ([id]),
    CONSTRAINT [FK__Team__leagueid__4D94879B] FOREIGN KEY ([Leagueid]) REFERENCES [dbo].[League] ([id])
);

