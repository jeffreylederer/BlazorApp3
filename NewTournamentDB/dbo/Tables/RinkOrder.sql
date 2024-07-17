CREATE TABLE [dbo].[RinkOrder] (
    [id]        INT          NOT NULL,
    [Green]     VARCHAR (25) NOT NULL,
    [Direction] VARCHAR (25) NOT NULL,
    [Boundary]  VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_RinkOrder] PRIMARY KEY CLUSTERED ([id] ASC)
);

