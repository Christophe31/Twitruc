
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/15/2011 22:27:26
-- Generated from EDMX file: C:\Users\Christophe\Documents\Dev\TP\Twitruc\Twitruc.DAL\db.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [blog_pourri];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserEntity1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tweets] DROP CONSTRAINT [FK_UserEntity1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitrucUsers];
GO
IF OBJECT_ID(N'[dbo].[Tweets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tweets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[TwitrucUsers] (
    [Id] uniqueidentifier default newid()  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Nickname] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [TokenSecret] nvarchar(max)  NOT NULL,
    [TwitterNick] nvarchar(max)  NOT NULL,
    [Inscription] datetime default getdate()  NOT NULL
);
GO

-- Creating table 'Tweets'
CREATE TABLE [dbo].[Tweets] (
    [Id] uniqueidentifier default newid() NOT NULL,
    [TweetId] bigint  NOT NULL,
    [Content] nvarchar(145)  NOT NULL,
    [Date] datetime default getdate() NOT NULL,
    [TwitrucUser_Id] uniqueidentifier  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[TwitrucUsers]
ADD CONSTRAINT [PKTwitrucUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tweets'
ALTER TABLE [dbo].[Tweets]
ADD CONSTRAINT [PK_Tweets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Tweets'
ALTER TABLE [dbo].[Tweets]
ADD CONSTRAINT [FK_UserEntity1]
    FOREIGN KEY ([TwitrucUser_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEntity1'
CREATE INDEX [IX_FK_UserEntity1]
ON [dbo].[Tweets]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------