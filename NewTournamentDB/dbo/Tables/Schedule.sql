CREATE TABLE [dbo].[Schedule] (
    [id]        INT  IDENTITY (1, 1) NOT NULL,
    [GameDate]  DATE NOT NULL,
    [Leagueid]  INT  NOT NULL,
    [Cancelled] BIT  CONSTRAINT [DF_Schedule_Cancelled] DEFAULT ((0)) NOT NULL,
    [PlayOffs]  BIT  CONSTRAINT [DF_Schedule_PlayGame] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Schedule_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK__Schedule__League__4F7CD00D] FOREIGN KEY ([Leagueid]) REFERENCES [dbo].[League] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Schedule_1]
    ON [dbo].[Schedule]([Leagueid] ASC, [GameDate] ASC);

