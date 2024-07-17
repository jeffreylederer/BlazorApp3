CREATE TABLE [dbo].[UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [id]     INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([id]),
    CONSTRAINT [FK_UserRole_UserRole] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
);

