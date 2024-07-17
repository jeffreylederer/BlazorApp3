CREATE TABLE [dbo].[Membership] (
    [id]         INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NOT NULL,
    [LastName]   VARCHAR (50) NOT NULL,
    [FullName]   AS           (([FirstName]+' ')+[Lastname]),
    [shortname]  VARCHAR (25) NULL,
    [NickName]   AS           (case when isnull([shortname],'')='' then [firstname] else [shortname] end),
    [Wheelchair] BIT          CONSTRAINT [DF_Membership_Wheelchair] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Membership]
    ON [dbo].[Membership]([LastName] ASC, [FirstName] ASC);

