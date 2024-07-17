CREATE TABLE [dbo].[User] (
    [id]           INT                IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (450)      NOT NULL,
    [Password]     VARCHAR (50)       NOT NULL,
    [DisplayName]  VARCHAR (50)       NOT NULL,
    [IsActive]     BIT                CONSTRAINT [DF_User_IsActive] DEFAULT ((1)) NOT NULL,
    [LastLoggedIn] DATETIMEOFFSET (7) NULL,
    [SerialNumber] VARCHAR (450)      NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC)
);

