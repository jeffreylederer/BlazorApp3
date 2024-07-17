CREATE TABLE [dbo].[League] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [LeagueName]  VARCHAR (50) NOT NULL,
    [Active]      BIT          CONSTRAINT [DF_League_Active] DEFAULT ((1)) NOT NULL,
    [TeamSize]    INT          NOT NULL,
    [TiesAllowed] BIT          DEFAULT ((0)) NOT NULL,
    [PointsCount] BIT          DEFAULT ((0)) NOT NULL,
    [WinPoints]   SMALLINT     CONSTRAINT [DF_League_WinPoints] DEFAULT ((1)) NOT NULL,
    [TiePoints]   SMALLINT     CONSTRAINT [DF_League_TiePoints] DEFAULT ((1)) NOT NULL,
    [ByePoints]   SMALLINT     CONSTRAINT [DF_League_ByePoints] DEFAULT ((1)) NOT NULL,
    [StartWeek]   INT          CONSTRAINT [DF_League_StartWeek] DEFAULT ((1)) NOT NULL,
    [PointsLimit] BIT          CONSTRAINT [DF_League_PointsLimit] DEFAULT ((1)) NOT NULL,
    [Divisions]   SMALLINT     CONSTRAINT [DF_League_Divisions] DEFAULT ((1)) NOT NULL,
    [PlayOffs]    BIT          CONSTRAINT [DF_League_PlayOffs] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_League] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_League_RinkOrder] FOREIGN KEY ([StartWeek]) REFERENCES [dbo].[RinkOrder] ([id])
);

