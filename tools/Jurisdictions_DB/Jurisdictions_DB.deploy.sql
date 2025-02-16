﻿/*
Deployment script for Jurisdictions_DB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Jurisdictions_DB"
:setvar DefaultFilePrefix "Jurisdictions_DB"
:setvar DefaultDataPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\DatabaseTools"
:setvar DefaultLogPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\DatabaseTools"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS ON 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY ON,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[tbl_documents]...';


GO
CREATE TABLE [dbo].[tbl_documents] (
    [documents_key]               UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [standard_sets_key]           UNIQUEIDENTIFIER NOT NULL,
    [documents_id]                NVARCHAR (50)    NULL,
    [documents_asnIdentifier]     NVARCHAR (8)     NULL,
    [documents_publicationStatus] NVARCHAR (20)    NULL,
    [documents_title]             NVARCHAR (250)   NULL,
    [documents_valid]             INT              NULL,
    [documents_sourceURL]         NVARCHAR (500)   NULL,
    CONSTRAINT [pk_tbl_documents_documents_key] PRIMARY KEY NONCLUSTERED ([documents_key] ASC)
);


GO
PRINT N'Creating [dbo].[tbl_jurisdictions]...';


GO
CREATE TABLE [dbo].[tbl_jurisdictions] (
    [jurisdictions_key]   UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [jurisdictions_id]    NVARCHAR (250)   NULL,
    [jurisdictions_title] NVARCHAR (100)   NULL,
    [jurisdictions_type]  NVARCHAR (20)    NULL,
    CONSTRAINT [pk_tbl_jurisdictions_id] PRIMARY KEY NONCLUSTERED ([jurisdictions_key] ASC)
);


GO
PRINT N'Creating [dbo].[tbl_standard_sets]...';


GO
CREATE TABLE [dbo].[tbl_standard_sets] (
    [standard_sets_key]             UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [jurisdictions_key]             UNIQUEIDENTIFIER NOT NULL,
    [standard_sets_id]              NVARCHAR (250)   NULL,
    [standard_sets_subject]         NVARCHAR (250)   NULL,
    [standard_sets_title]           NVARCHAR (250)   NULL,
    [standard_sets_educationLevels] NVARCHAR (MAX)   NULL,
    CONSTRAINT [pk_tbl_standard_sets_standard_sets_key] PRIMARY KEY NONCLUSTERED ([standard_sets_key] ASC)
);


GO
PRINT N'Creating [dbo].[df_tbl_documents_documents_key]...';


GO
ALTER TABLE [dbo].[tbl_documents]
    ADD CONSTRAINT [df_tbl_documents_documents_key] DEFAULT (NEWSEQUENTIALID()) FOR [documents_key];


GO
PRINT N'Creating [dbo].[df_tbl_jurisdictions_jurisdictions_key]...';


GO
ALTER TABLE [dbo].[tbl_jurisdictions]
    ADD CONSTRAINT [df_tbl_jurisdictions_jurisdictions_key] DEFAULT (NEWSEQUENTIALID()) FOR [jurisdictions_key];


GO
PRINT N'Creating [dbo].[df_tbl_standard_sets_standard_sets_key]...';


GO
ALTER TABLE [dbo].[tbl_standard_sets]
    ADD CONSTRAINT [df_tbl_standard_sets_standard_sets_key] DEFAULT (NEWSEQUENTIALID()) FOR [standard_sets_key];


GO
PRINT N'Creating [dbo].[fk_tbl_documents_standard_sets_key]...';


GO
ALTER TABLE [dbo].[tbl_documents]
    ADD CONSTRAINT [fk_tbl_documents_standard_sets_key] FOREIGN KEY ([standard_sets_key]) REFERENCES [dbo].[tbl_standard_sets] ([standard_sets_key]);


GO
PRINT N'Creating [dbo].[pk_tbl_standard_sets_jurisdictions_key]...';


GO
ALTER TABLE [dbo].[tbl_standard_sets]
    ADD CONSTRAINT [pk_tbl_standard_sets_jurisdictions_key] FOREIGN KEY ([jurisdictions_key]) REFERENCES [dbo].[tbl_jurisdictions] ([jurisdictions_key]);


GO
PRINT N'Creating [dbo].[v_jurisdictions]...';


GO
--
--	View [dbo].[v_jurisdictions]	
--
CREATE VIEW [dbo].[v_jurisdictions]
AS
(
SELECT                      [J].[jurisdictions_id], 
                            [J].[jurisdictions_title], 
                            [J].[jurisdictions_type], 
                            [S].[standard_sets_id], 
                            [S].[standard_sets_subject], 
                            [S].[standard_sets_title], 
                            [S].[standard_sets_educationLevels], 
                            [D].[documents_id], 
                            [D].[documents_asnIdentifier], 
                            [D].[documents_publicationStatus], 
                            [D].[documents_title], 
                            [D].[documents_valid], 
                            [D].[documents_sourceURL]


FROM                      [dbo].[tbl_jurisdictions] AS [J]
               INNER JOIN [dbo].[tbl_standard_sets] AS [S] ON [J].[jurisdictions_key] = [S].[jurisdictions_key]
               INNER JOIN [dbo].[tbl_documents]     AS [D] ON [S].[standard_sets_key] = [D].[standard_sets_key]
);
GO
/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

USE [master];
GO

ALTER AUTHORIZATION ON DATABASE::[$(DatabaseName)] TO [sa]
GO

ALTER DATABASE [$(DatabaseName)] SET RECURSIVE_TRIGGERS ON
GO

USE [$(DatabaseName)]
GO


ALTER DATABASE [$(DatabaseName)] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [$(DatabaseName)].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [$(DatabaseName)] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET ARITHABORT OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [$(DatabaseName)] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [$(DatabaseName)] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET  ENABLE_BROKER 
GO

ALTER DATABASE [$(DatabaseName)] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [$(DatabaseName)] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET RECOVERY FULL 
GO

ALTER DATABASE [$(DatabaseName)] SET  MULTI_USER 
GO

ALTER DATABASE [$(DatabaseName)] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [$(DatabaseName)] SET DB_CHAINING OFF 
GO

ALTER DATABASE [$(DatabaseName)] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [$(DatabaseName)] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [$(DatabaseName)] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [$(DatabaseName)] SET QUERY_STORE = OFF
GO

USE [$(DatabaseName)]
GO


ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [$(DatabaseName)] SET  READ_WRITE 
GO

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
