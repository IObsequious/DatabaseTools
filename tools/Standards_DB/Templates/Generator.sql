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
    @standard_sets_key UNIQUEIDENTIFIER,
    @jurisdictions_id NVARCHAR(250),
    @jurisdictions_title NVARCHAR(100) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_jurisdictions]
        (
            [jurisdictions_key],
            [standard_sets_key],
            [jurisdictions_id],
            [jurisdictions_title]
        )
        VALUES
        (
            @jurisdictions_key,
            @standard_sets_key,
            @jurisdictions_id,
            @jurisdictions_title
        );
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_insert_tbl_licenses]
(
    @licenses_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @licenses_title NVARCHAR(250) = NULL,
    @licenses_url NVARCHAR(500) = NULL,
    @licenses_rightsHolder NVARCHAR(250) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_licenses]
        (
            [licenses_key],
            [standard_sets_key],
            [licenses_title],
            [licenses_url],
            [licenses_rightsHolder]
        )
        VALUES
        (
            @licenses_key,
            @standard_sets_key,
            @licenses_title,
            @licenses_url,
            @licenses_rightsHolder
        );
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_insert_tbl_standard_sets]
(
    @standard_sets_key UNIQUEIDENTIFIER,
    @standard_sets_id NVARCHAR(250) = NULL,
    @standard_sets_subject NVARCHAR(250) = NULL,
    @standard_sets_title NVARCHAR(250) = NULL,
    @standard_sets_educationLevels NVARCHAR(MAX) = NULL,
    @standard_sets_rightsHolder NVARCHAR(250) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_standard_sets]
        (
            [standard_sets_key],
            [standard_sets_id],
            [standard_sets_subject],
            [standard_sets_title],
            [standard_sets_educationLevels],
            [standard_sets_rightsHolder]
        )
        VALUES
        (
            @standard_sets_key,
            @standard_sets_id,
            @standard_sets_subject,
            @standard_sets_title,
            @standard_sets_educationLevels,
            @standard_sets_rightsHolder
        );
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_insert_tbl_standards]
(
    @standards_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @standards_id NVARCHAR(250) = NULL,
    @standards_asnIdentifier NVARCHAR(250) = NULL,
    @standards_position INT = NULL,
    @standards_depth INT = NULL,
    @standards_statementNotation NVARCHAR(250) = NULL,
    @standards_statementLabel NVARCHAR(250) = NULL,
    @standards_listId NVARCHAR(250) = NULL,
    @standards_description NVARCHAR(MAX) = NULL,
    @standards_ancestorIds NVARCHAR(MAX) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO [dbo].[tbl_standards]
        (
            [standards_key],
            [standard_sets_key],
            [standards_id],
            [standards_asnIdentifier],
            [standards_position],
            [standards_depth],
            [standards_statementNotation],
            [standards_statementLabel],
            [standards_listId],
            [standards_description],
            [standards_ancestorIds]
        )
        VALUES
        (
            @standards_key,
            @standard_sets_key,
            @standards_id,
            @standards_asnIdentifier,
            @standards_position,
            @standards_depth,
            @standards_statementNotation,
            @standards_statementLabel,
            @standards_listId,
            @standards_description,
            @standards_ancestorIds
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
    @standard_sets_key UNIQUEIDENTIFIER,
    @jurisdictions_id NVARCHAR(250),
    @jurisdictions_title NVARCHAR(100) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @jurisdictions_title IS NULL
            BEGIN
                SET @jurisdictions_title = (SELECT [jurisdictions_title] FROM [dbo].[tbl_jurisdictions] WHERE @jurisdictions_key = [jurisdictions_key]);
            END
        UPDATE [dbo].[tbl_jurisdictions]
        SET [standard_sets_key] = @standard_sets_key,
        [jurisdictions_id] = @jurisdictions_id,
        [jurisdictions_title] = @jurisdictions_title
        WHERE [jurisdictions_key] = @jurisdictions_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_update_tbl_licenses]
(
    @licenses_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @licenses_title NVARCHAR(250) = NULL,
    @licenses_url NVARCHAR(500) = NULL,
    @licenses_rightsHolder NVARCHAR(250) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @licenses_title IS NULL
            BEGIN
                SET @licenses_title = (SELECT [licenses_title] FROM [dbo].[tbl_licenses] WHERE @licenses_key = [licenses_key]);
            END
        IF @licenses_url IS NULL
            BEGIN
                SET @licenses_url = (SELECT [licenses_url] FROM [dbo].[tbl_licenses] WHERE @licenses_key = [licenses_key]);
            END
        IF @licenses_rightsHolder IS NULL
            BEGIN
                SET @licenses_rightsHolder = (SELECT [licenses_rightsHolder] FROM [dbo].[tbl_licenses] WHERE @licenses_key = [licenses_key]);
            END
        UPDATE [dbo].[tbl_licenses]
        SET [standard_sets_key] = @standard_sets_key,
        [licenses_title] = @licenses_title,
        [licenses_url] = @licenses_url,
        [licenses_rightsHolder] = @licenses_rightsHolder
        WHERE [licenses_key] = @licenses_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_update_tbl_standard_sets]
(
    @standard_sets_key UNIQUEIDENTIFIER,
    @standard_sets_id NVARCHAR(250) = NULL,
    @standard_sets_subject NVARCHAR(250) = NULL,
    @standard_sets_title NVARCHAR(250) = NULL,
    @standard_sets_educationLevels NVARCHAR(MAX) = NULL,
    @standard_sets_rightsHolder NVARCHAR(250) = NULL
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
        IF @standard_sets_rightsHolder IS NULL
            BEGIN
                SET @standard_sets_rightsHolder = (SELECT [standard_sets_rightsHolder] FROM [dbo].[tbl_standard_sets] WHERE @standard_sets_key = [standard_sets_key]);
            END
        UPDATE [dbo].[tbl_standard_sets]
        SET [standard_sets_id] = @standard_sets_id,
        [standard_sets_subject] = @standard_sets_subject,
        [standard_sets_title] = @standard_sets_title,
        [standard_sets_educationLevels] = @standard_sets_educationLevels,
        [standard_sets_rightsHolder] = @standard_sets_rightsHolder
        WHERE [standard_sets_key] = @standard_sets_key;
        RETURN @@ROWCOUNT;
    END
GO
CREATE PROCEDURE [dbo].[sp_update_tbl_standards]
(
    @standards_key UNIQUEIDENTIFIER,
    @standard_sets_key UNIQUEIDENTIFIER,
    @standards_id NVARCHAR(250) = NULL,
    @standards_asnIdentifier NVARCHAR(250) = NULL,
    @standards_position INT = NULL,
    @standards_depth INT = NULL,
    @standards_statementNotation NVARCHAR(250) = NULL,
    @standards_statementLabel NVARCHAR(250) = NULL,
    @standards_listId NVARCHAR(250) = NULL,
    @standards_description NVARCHAR(MAX) = NULL,
    @standards_ancestorIds NVARCHAR(MAX) = NULL
)
AS
    BEGIN
        SET NOCOUNT ON;
        IF @standards_id IS NULL
            BEGIN
                SET @standards_id = (SELECT [standards_id] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_asnIdentifier IS NULL
            BEGIN
                SET @standards_asnIdentifier = (SELECT [standards_asnIdentifier] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_position IS NULL
            BEGIN
                SET @standards_position = (SELECT [standards_position] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_depth IS NULL
            BEGIN
                SET @standards_depth = (SELECT [standards_depth] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_statementNotation IS NULL
            BEGIN
                SET @standards_statementNotation = (SELECT [standards_statementNotation] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_statementLabel IS NULL
            BEGIN
                SET @standards_statementLabel = (SELECT [standards_statementLabel] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_listId IS NULL
            BEGIN
                SET @standards_listId = (SELECT [standards_listId] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_description IS NULL
            BEGIN
                SET @standards_description = (SELECT [standards_description] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        IF @standards_ancestorIds IS NULL
            BEGIN
                SET @standards_ancestorIds = (SELECT [standards_ancestorIds] FROM [dbo].[tbl_standards] WHERE @standards_key = [standards_key]);
            END
        UPDATE [dbo].[tbl_standards]
        SET [standard_sets_key] = @standard_sets_key,
        [standards_id] = @standards_id,
        [standards_asnIdentifier] = @standards_asnIdentifier,
        [standards_position] = @standards_position,
        [standards_depth] = @standards_depth,
        [standards_statementNotation] = @standards_statementNotation,
        [standards_statementLabel] = @standards_statementLabel,
        [standards_listId] = @standards_listId,
        [standards_description] = @standards_description,
        [standards_ancestorIds] = @standards_ancestorIds
        WHERE [standards_key] = @standards_key;
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
CREATE PROCEDURE [dbo].[sp_delete_tbl_licenses]
(
    @licenses_key UNIQUEIDENTIFIER
)
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[tbl_licenses] WHERE [licenses_key] = @licenses_key;
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
CREATE PROCEDURE [dbo].[sp_delete_tbl_standards]
(
    @standards_key UNIQUEIDENTIFIER
)
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[tbl_standards] WHERE [standards_key] = @standards_key;
        RETURN @@ROWCOUNT;
    END
GO

