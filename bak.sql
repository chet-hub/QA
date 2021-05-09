IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Reputation] int NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DeviceCodes] (
    [UserCode] nvarchar(200) NOT NULL,
    [DeviceCode] nvarchar(200) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DeviceCodes] PRIMARY KEY ([UserCode])
);
GO

CREATE TABLE [PersistedGrants] (
    [Key] nvarchar(200) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NULL,
    [ConsumedTime] datetime2 NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PersistedGrants] PRIMARY KEY ([Key])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Questions] (
    [Id] int NOT NULL IDENTITY,
    [Votes] int NOT NULL,
    [Answers] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [CreateDateTime] datetime2 NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Questions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Answers] (
    [Id] int NOT NULL IDENTITY,
    [Votes] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreateDateTime] datetime2 NOT NULL,
    [QuestionId] int NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Answers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tags] (
    [Id] int NOT NULL IDENTITY,
    [QuestionId] int NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tags_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [CreateDateTime] datetime2 NOT NULL,
    [UserId] nvarchar(450) NULL,
    [QuestionId] int NULL,
    [AnswerId] int NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Answers_AnswerId] FOREIGN KEY ([AnswerId]) REFERENCES [Answers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'd2eac9d3-6141-476c-9020-f42250d63e80', N'b952fe20-b530-4847-ae39-a1f454ec5013', N'Admin', NULL),
(N'd2eac9d3-6141-476c-9020-f42250d63e81', N'16d7b153-2b68-4337-b6a9-639120aea0fb', N'Guest', NULL),
(N'd2eac9d3-6141-476c-9020-f42250d63e82', N'f26a0020-b7f8-4656-95ec-5e0b9aa95f87', N'Moderator', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'Reputation', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [Reputation], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'd2eac9d3-6141-476c-9020-f42250d63e86', 0, N'3b3e029c-9d49-4d25-968b-9c3a913d903c', N'ff@qq.com', CAST(1 AS bit), CAST(1 AS bit), NULL, N'FF@QQ.COM', N'FF@QQ.COM', N'AQAAAAEAACcQAAAAEMQJrTVkVJiBSAvpCuUKqC3g7BQyCNs1igCObs9zKmTySe8b1gNJ1iFMpviFa++k2w==', NULL, CAST(0 AS bit), 0, N'N4Q2NOPGJ5DYCTUU67NCDN6EELEJFO4N', CAST(0 AS bit), N'ff@qq.com'),
(N'd985a7b1-d58b-4266-ab86-a0a0ff91ccc1', 0, N'714d2b03-873f-479b-9249-4e7ef30866f9', N'cc861010@gmail.com', CAST(1 AS bit), CAST(1 AS bit), NULL, N'CC861010@GMAIL.COM', N'CC861010@GMAIL.COM', N'AQAAAAEAACcQAAAAEIyzWsOem/kp/V7dgVxg6cmAvcFfC+8zAc0bLT6iOLsm7qMiONewX13Nm3kihjYVvQ==', NULL, CAST(0 AS bit), 0, N'USIVDWV4UHHSCT6ZTTJDMSRXHVVU4P2D', CAST(0 AS bit), N'cc861010@gmail.com'),
(N'f984066c-816d-4531-bd9a-c63256ca7000', 0, N'dd5e1fdf-03ea-47f2-84c7-2873d0ae85b9', N'cc@qq.com', CAST(1 AS bit), CAST(1 AS bit), NULL, N'CC@QQ.COM', N'CC@QQ.COM', N'AQAAAAEAACcQAAAAEL3A1emCK92O5spftjyhQkRYl0VhfVQQGmkmn1QM68AWMzDNoOLXsfXgOmNcpaWprw==', NULL, CAST(0 AS bit), 0, N'BNDKY6ICQC3YUUQL5OTFI2277AJY7LGJ', CAST(0 AS bit), N'cc@qq.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'Reputation', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'RoleId') AND [object_id] = OBJECT_ID(N'[AspNetRoleClaims]'))
    SET IDENTITY_INSERT [AspNetRoleClaims] ON;
INSERT INTO [AspNetRoleClaims] ([Id], [ClaimType], [ClaimValue], [RoleId])
VALUES (-1, N'abc', N'1', N'd2eac9d3-6141-476c-9020-f42250d63e80'),
(-2, N'b', N'1', N'd2eac9d3-6141-476c-9020-f42250d63e81'),
(-3, N'c', N'1', N'd2eac9d3-6141-476c-9020-f42250d63e82');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'RoleId') AND [object_id] = OBJECT_ID(N'[AspNetRoleClaims]'))
    SET IDENTITY_INSERT [AspNetRoleClaims] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'd2eac9d3-6141-476c-9020-f42250d63e80', N'd2eac9d3-6141-476c-9020-f42250d63e86'),
(N'd2eac9d3-6141-476c-9020-f42250d63e81', N'd985a7b1-d58b-4266-ab86-a0a0ff91ccc1'),
(N'd2eac9d3-6141-476c-9020-f42250d63e82', N'f984066c-816d-4531-bd9a-c63256ca7000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Answers', N'CreateDateTime', N'Description', N'Title', N'UserId', N'Votes') AND [object_id] = OBJECT_ID(N'[Questions]'))
    SET IDENTITY_INSERT [Questions] ON;
INSERT INTO [Questions] ([Id], [Answers], [CreateDateTime], [Description], [Title], [UserId], [Votes])
VALUES (-1, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -1 ', N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-2, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -2', N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-3, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -3', N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-4, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -4', N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-5, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -5', N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-6, 1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', N'Question title -6', N'f984066c-816d-4531-bd9a-c63256ca7000', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Answers', N'CreateDateTime', N'Description', N'Title', N'UserId', N'Votes') AND [object_id] = OBJECT_ID(N'[Questions]'))
    SET IDENTITY_INSERT [Questions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreateDateTime', N'Description', N'QuestionId', N'UserId', N'Votes') AND [object_id] = OBJECT_ID(N'[Answers]'))
    SET IDENTITY_INSERT [Answers] ON;
INSERT INTO [Answers] ([Id], [CreateDateTime], [Description], [QuestionId], [UserId], [Votes])
VALUES (-1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -1, N'f984066c-816d-4531-bd9a-c63256ca7000', 12),
(-6, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -5, N'f984066c-816d-4531-bd9a-c63256ca7000', 10),
(-5, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -4, N'f984066c-816d-4531-bd9a-c63256ca7000', 10),
(-3, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -2, N'f984066c-816d-4531-bd9a-c63256ca7000', 10),
(-4, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -3, N'f984066c-816d-4531-bd9a-c63256ca7000', 10),
(-2, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -1, N'f984066c-816d-4531-bd9a-c63256ca7000', 10);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreateDateTime', N'Description', N'QuestionId', N'UserId', N'Votes') AND [object_id] = OBJECT_ID(N'[Answers]'))
    SET IDENTITY_INSERT [Answers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnswerId', N'CreateDateTime', N'Description', N'QuestionId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] ON;
INSERT INTO [Comments] ([Id], [AnswerId], [CreateDateTime], [Description], [QuestionId], [UserId])
VALUES (-2, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -1, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-4, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -2, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-5, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -3, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-1, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -1, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-6, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -4, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-7, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -5, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-3, NULL, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', -1, N'f984066c-816d-4531-bd9a-c63256ca7000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnswerId', N'CreateDateTime', N'Description', N'QuestionId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'QuestionId') AND [object_id] = OBJECT_ID(N'[Tags]'))
    SET IDENTITY_INSERT [Tags] ON;
INSERT INTO [Tags] ([Id], [Name], [QuestionId])
VALUES (-1, N'iphone', -1),
(-2, N'apple', -2),
(-5, N'java', -5),
(-3, N'android', -3),
(-4, N'c#', -4),
(-6, N'js', -6);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'QuestionId') AND [object_id] = OBJECT_ID(N'[Tags]'))
    SET IDENTITY_INSERT [Tags] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnswerId', N'CreateDateTime', N'Description', N'QuestionId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] ON;
INSERT INTO [Comments] ([Id], [AnswerId], [CreateDateTime], [Description], [QuestionId], [UserId])
VALUES (-8, -1, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', NULL, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-9, -2, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', NULL, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-10, -3, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', NULL, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-11, -4, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', NULL, N'f984066c-816d-4531-bd9a-c63256ca7000'),
(-12, -4, '2021-05-05T00:00:00.0000000', N'{"blocks":[{"key":"a7jmu","text":"12321123123213","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}', NULL, N'f984066c-816d-4531-bd9a-c63256ca7000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnswerId', N'CreateDateTime', N'Description', N'QuestionId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] OFF;
GO

CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
GO

CREATE INDEX [IX_Answers_UserId] ON [Answers] ([UserId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Comments_AnswerId] ON [Comments] ([AnswerId]);
GO

CREATE INDEX [IX_Comments_QuestionId] ON [Comments] ([QuestionId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_DeviceCodes_DeviceCode] ON [DeviceCodes] ([DeviceCode]);
GO

CREATE INDEX [IX_DeviceCodes_Expiration] ON [DeviceCodes] ([Expiration]);
GO

CREATE INDEX [IX_PersistedGrants_Expiration] ON [PersistedGrants] ([Expiration]);
GO

CREATE INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [PersistedGrants] ([SubjectId], [ClientId], [Type]);
GO

CREATE INDEX [IX_PersistedGrants_SubjectId_SessionId_Type] ON [PersistedGrants] ([SubjectId], [SessionId], [Type]);
GO

CREATE INDEX [IX_Questions_UserId] ON [Questions] ([UserId]);
GO

CREATE INDEX [IX_Tags_QuestionId] ON [Tags] ([QuestionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509224106_Initial', N'5.0.3');
GO

COMMIT;
GO


