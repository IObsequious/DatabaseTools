/********************************************************************************
        Insert Procedures
********************************************************************************/

CREATE PROCEDURE [dbo].[sp_insert_tbl_documents]
(
    @documents_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @documents_id NVARCHAR(50) = NULL,
    @documents_asnIdentifier NVARCHAR(8) = NULL,
    @documents_publicationStatus NVARCHAR(20) = NULL,
    @documents_title NVARCHAR(250) = NULL,
    @documents_valid INT = NULL,
    @documents_sourceURL NVARCHAR(500) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_documents]
        (
            [documents_key],
            [standard_sets_key],
            [documents_id],
            [documents_asnIdentifier],
            [documents_publicationStatus],
            [documents_title],
            [documents_valid],
            [documents_sourceURL]
        )
        VALUES
        (
            @documents_key,
            @standard_sets_key,
            @documents_id,
            @documents_asnIdentifier,
            @documents_publicationStatus,
            @documents_title,
            @documents_valid,
            @documents_sourceURL
        );
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_insert_tbl_jurisdictions]
(
    @jurisdictions_key UNIQUEIDENTIFIER,
    @jurisdictions_id NVARCHAR(250) = NULL,
    @jurisdictions_title NVARCHAR(100) = NULL,
    @jurisdictions_type NVARCHAR(20) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_jurisdictions]
        (
            [jurisdictions_key],
            [jurisdictions_id],
            [jurisdictions_title],
            [jurisdictions_type]
        )
        VALUES
        (
            @jurisdictions_key,
            @jurisdictions_id,
            @jurisdictions_title,
            @jurisdictions_type
        );
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_insert_tbl_standard_sets]
(
    @standard_sets_key UNIQUEIDENTIFIER,
    @jurisdictions_key UNIQUEIDENTIFIER,
    @standard_sets_id NVARCHAR(250) = NULL,
    @standard_sets_subject NVARCHAR(250) = NULL,
    @standard_sets_title NVARCHAR(250) = NULL,
    @standard_sets_educationLevels NVARCHAR(MAX) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_standard_sets]
        (
            [standard_sets_key],
            [jurisdictions_key],
            [standard_sets_id],
            [standard_sets_subject],
            [standard_sets_title],
            [standard_sets_educationLevels]
        )
        VALUES
        (
            @standard_sets_key,
            @jurisdictions_key,
            @standard_sets_id,
            @standard_sets_subject,
            @standard_sets_title,
            @standard_sets_educationLevels
        );
        RETURN @@ROWCOUNT;
    END
GO
/********************************************************************************
        Update Procedures
********************************************************************************/

CREATE PROCEDURE [dbo].[sp_update_tbl_documents]
(
    @documents_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @documents_id NVARCHAR(50) = NULL,
    @documents_asnIdentifier NVARCHAR(8) = NULL,
    @documents_publicationStatus NVARCHAR(20) = NULL,
    @documents_title NVARCHAR(250) = NULL,
    @documents_valid INT = NULL,
    @documents_sourceURL NVARCHAR(500) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @documents_id IS NULL
            BEGIN
                SET @documents_id = (SELECT [documents_id] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        IF @documents_asnIdentifier IS NULL
            BEGIN
                SET @documents_asnIdentifier = (SELECT [documents_asnIdentifier] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        IF @documents_publicationStatus IS NULL
            BEGIN
                SET @documents_publicationStatus = (SELECT [documents_publicationStatus] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        IF @documents_title IS NULL
            BEGIN
                SET @documents_title = (SELECT [documents_title] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        IF @documents_valid IS NULL
            BEGIN
                SET @documents_valid = (SELECT [documents_valid] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        IF @documents_sourceURL IS NULL
            BEGIN
                SET @documents_sourceURL = (SELECT [documents_sourceURL] FROM [dbo].[tbl_documents] WHERE @documents_key = [documents_key]);
            END
        UPDATE [dbo].[tbl_documents]
        SET [standard_sets_key] = @standard_sets_key,
        [documents_id] = @documents_id,
        [documents_asnIdentifier] = @documents_asnIdentifier,
        [documents_publicationStatus] = @documents_publicationStatus,
        [documents_title] = @documents_title,
        [documents_valid] = @documents_valid,
        [documents_sourceURL] = @documents_sourceURL
        WHERE [documents_key] = @documents_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_update_tbl_jurisdictions]
(
    @jurisdictions_key UNIQUEIDENTIFIER,
    @jurisdictions_id NVARCHAR(250) = NULL,
    @jurisdictions_title NVARCHAR(100) = NULL,
    @jurisdictions_type NVARCHAR(20) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @jurisdictions_id IS NULL
            BEGIN
                SET @jurisdictions_id = (SELECT [jurisdictions_id] FROM [dbo].[tbl_jurisdictions] WHERE @jurisdictions_key = [jurisdictions_key]);
            END
        IF @jurisdictions_title IS NULL
            BEGIN
                SET @jurisdictions_title = (SELECT [jurisdictions_title] FROM [dbo].[tbl_jurisdictions] WHERE @jurisdictions_key = [jurisdictions_key]);
            END
        IF @jurisdictions_type IS NULL
            BEGIN
                SET @jurisdictions_type = (SELECT [jurisdictions_type] FROM [dbo].[tbl_jurisdictions] WHERE @jurisdictions_key = [jurisdictions_key]);
            END
        UPDATE [dbo].[tbl_jurisdictions]
        SET [jurisdictions_id] = @jurisdictions_id,
        [jurisdictions_title] = @jurisdictions_title,
        [jurisdictions_type] = @jurisdictions_type
        WHERE [jurisdictions_key] = @jurisdictions_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_update_tbl_standard_sets]
(
    @standard_sets_key UNIQUEIDENTIFIER,
    @jurisdictions_key UNIQUEIDENTIFIER,
    @standard_sets_id NVARCHAR(250) = NULL,
    @standard_sets_subject NVARCHAR(250) = NULL,
    @standard_sets_title NVARCHAR(250) = NULL,
    @standard_sets_educationLevels NVARCHAR(MAX) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @standard_sets_id IS NULL
            BEGIN
                SET @standard_sets_id = (SELECT [standard_sets_id] FROM [dbo].[tbl_standard_sets] WHERE @standard_sets_key = [standard_sets_key]);
            END
        IF @standard_sets_subject IS NULL
            BEGIN
                SET @standard_sets_subject = (SELECT [standard_sets_subject] FROM [dbo].[tbl_standard_sets] WHERE @standard_sets_key = [standard_sets_key]);
            END
        IF @standard_sets_title IS NULL
            BEGIN
                SET @standard_sets_title = (SELECT [standard_sets_title] FROM [dbo].[tbl_standard_sets] WHERE @standard_sets_key = [standard_sets_key]);
            END
        IF @standard_sets_educationLevels IS NULL
            BEGIN
                SET @standard_sets_educationLevels = (SELECT [standard_sets_educationLevels] FROM [dbo].[tbl_standard_sets] WHERE @standard_sets_key = [standard_sets_key]);
            END
        UPDATE [dbo].[tbl_standard_sets]
        SET [jurisdictions_key] = @jurisdictions_key,
        [standard_sets_id] = @standard_sets_id,
        [standard_sets_subject] = @standard_sets_subject,
        [standard_sets_title] = @standard_sets_title,
        [standard_sets_educationLevels] = @standard_sets_educationLevels
        WHERE [standard_sets_key] = @standard_sets_key;
        RETURN @@ROWCOUNT;
    END
GO
/********************************************************************************
        Delete Procedures
********************************************************************************/

CREATE PROCEDURE [dbo].[sp_delete_tbl_documents]
(
    @documents_key UNIQUEIDENTIFIER
)
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[tbl_documents] WHERE [documents_key] = @documents_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_delete_tbl_jurisdictions]
(
    @jurisdictions_key UNIQUEIDENTIFIER
)
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[tbl_jurisdictions] WHERE [jurisdictions_key] = @jurisdictions_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_delete_tbl_standard_sets]
(
    @standard_sets_key UNIQUEIDENTIFIER
)
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[tbl_standard_sets] WHERE [standard_sets_key] = @standard_sets_key;
        RETURN @@ROWCOUNT;
    END
GO

