
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2011 10:29:17
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

IF OBJECT_ID(N'[dbo].[FK_TweetTwitrucUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TweetSet] DROP CONSTRAINT [FK_TweetTwitrucUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[TweetSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TweetSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Nickname] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [TokenSecret] nvarchar(max)  NOT NULL,
    [TwitterNick] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TweetSet'
CREATE TABLE [dbo].[TweetSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TweetId] bigint  NOT NULL,
    [Content] nvarchar(145)  NOT NULL,
    [Date] datetime  NOT NULL,
    [AuthorNick] nvarchar(max)  NOT NULL,
    [Sent] bit  NOT NULL,
    [TwitrucUsers_Id] bigint  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TweetSet'
ALTER TABLE [dbo].[TweetSet]
ADD CONSTRAINT [PK_TweetSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TwitrucUsers_Id] in table 'TweetSet'
ALTER TABLE [dbo].[TweetSet]
ADD CONSTRAINT [FK_TweetTwitrucUser]
    FOREIGN KEY ([TwitrucUsers_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TweetTwitrucUser'
CREATE INDEX [IX_FK_TweetTwitrucUser]
ON [dbo].[TweetSet]
    ([TwitrucUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------