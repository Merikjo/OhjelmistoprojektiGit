
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/05/2019 09:53:36
-- Generated from EDMX file: D:\VS2017-Student\OhjelmistoprojektiDan\Ohjelmistoprojekti\Ohjelmistoprojekti\Ohjelmistoprojekti\Models\ohjelmistoprojekti.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Ohjelmistodatahaku];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_HenkilonOsaamiset_Henkilot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HenkilonOsaamiset] DROP CONSTRAINT [FK_HenkilonOsaamiset_Henkilot];
GO
IF OBJECT_ID(N'[dbo].[FK_HenkilonOsaamiset_Osaamisaiheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HenkilonOsaamiset] DROP CONSTRAINT [FK_HenkilonOsaamiset_Osaamisaiheet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[HenkilonOsaamiset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HenkilonOsaamiset];
GO
IF OBJECT_ID(N'[dbo].[Henkilot]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Henkilot];
GO
IF OBJECT_ID(N'[dbo].[Osaamisaiheet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Osaamisaiheet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'HenkilonOsaamiset'
CREATE TABLE [dbo].[HenkilonOsaamiset] (
    [HenkilonOsaamisID] int IDENTITY(1,1) NOT NULL,
    [OsaamisaiheID] int  NULL,
    [HenkiloID] int  NULL,
    [Osaamistaso] int  NULL
);
GO

-- Creating table 'Henkilot'
CREATE TABLE [dbo].[Henkilot] (
    [HenkiloID] int IDENTITY(1,1) NOT NULL,
    [Etunimi] nvarchar(50)  NULL,
    [Sukunimi] nvarchar(50)  NULL,
    [TyoPuhelin] nvarchar(50)  NULL,
    [TyoSahkoposti] nvarchar(100)  NULL,
    [Organiaatio] nvarchar(100)  NULL,
    [Henkilonumero] int  NULL
);
GO

-- Creating table 'Osaamisaiheet'
CREATE TABLE [dbo].[Osaamisaiheet] (
    [OsaamisaiheID] int IDENTITY(1,1) NOT NULL,
    [Kuvaus] nvarchar(200)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [HenkilonOsaamisID] in table 'HenkilonOsaamiset'
ALTER TABLE [dbo].[HenkilonOsaamiset]
ADD CONSTRAINT [PK_HenkilonOsaamiset]
    PRIMARY KEY CLUSTERED ([HenkilonOsaamisID] ASC);
GO

-- Creating primary key on [HenkiloID] in table 'Henkilot'
ALTER TABLE [dbo].[Henkilot]
ADD CONSTRAINT [PK_Henkilot]
    PRIMARY KEY CLUSTERED ([HenkiloID] ASC);
GO

-- Creating primary key on [OsaamisaiheID] in table 'Osaamisaiheet'
ALTER TABLE [dbo].[Osaamisaiheet]
ADD CONSTRAINT [PK_Osaamisaiheet]
    PRIMARY KEY CLUSTERED ([OsaamisaiheID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [HenkiloID] in table 'HenkilonOsaamiset'
ALTER TABLE [dbo].[HenkilonOsaamiset]
ADD CONSTRAINT [FK_HenkilonOsaamiset_Henkilot]
    FOREIGN KEY ([HenkiloID])
    REFERENCES [dbo].[Henkilot]
        ([HenkiloID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HenkilonOsaamiset_Henkilot'
CREATE INDEX [IX_FK_HenkilonOsaamiset_Henkilot]
ON [dbo].[HenkilonOsaamiset]
    ([HenkiloID]);
GO

-- Creating foreign key on [OsaamisaiheID] in table 'HenkilonOsaamiset'
ALTER TABLE [dbo].[HenkilonOsaamiset]
ADD CONSTRAINT [FK_HenkilonOsaamiset_Osaamisaiheet]
    FOREIGN KEY ([OsaamisaiheID])
    REFERENCES [dbo].[Osaamisaiheet]
        ([OsaamisaiheID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HenkilonOsaamiset_Osaamisaiheet'
CREATE INDEX [IX_FK_HenkilonOsaamiset_Osaamisaiheet]
ON [dbo].[HenkilonOsaamiset]
    ([OsaamisaiheID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------