CREATE TABLE [dbo].[Match] (
    [id]         INT IDENTITY (1, 1) NOT NULL,
    [WeekId]     INT NOT NULL,
    [Rink]       INT NOT NULL,
    [TeamNo1]    INT NOT NULL,
    [TeamNo2]    INT NULL,
    [Team1Score] INT CONSTRAINT [DF_Match_Team1Score] DEFAULT ((0)) NOT NULL,
    [Team2Score] INT CONSTRAINT [DF_Match_Team2Score] DEFAULT ((0)) NOT NULL,
    [ForFeitId]  INT CONSTRAINT [DF_Match_ForFeit] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Match_Schedule] FOREIGN KEY ([WeekId]) REFERENCES [dbo].[Schedule] ([id]),
    CONSTRAINT [FK_Match_Team] FOREIGN KEY ([TeamNo1]) REFERENCES [dbo].[Team] ([id]),
    CONSTRAINT [FK_Match_Team1] FOREIGN KEY ([TeamNo2]) REFERENCES [dbo].[Team] ([id])
);

