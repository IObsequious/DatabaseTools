--
--    Table [dbo].[tbl_documents]
--
CREATE TABLE [dbo].[tbl_documents]
(
    [documents_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_documents_documents_key] DEFAULT(NEWSEQUENTIALID()),

    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL,

    [documents_id] NVARCHAR(50) NULL,
    [documents_asnIdentifier] NVARCHAR(8) NULL,
    [documents_publicationStatus] NVARCHAR(20) NULL,
    [documents_title] NVARCHAR(250) NULL,
    [documents_valid] INT NULL,
    [documents_sourceURL] NVARCHAR(500) NULL,

    CONSTRAINT [pk_tbl_documents_documents_key] PRIMARY KEY NONCLUSTERED ([documents_key]),
    CONSTRAINT [fk_tbl_documents_standard_sets_key] FOREIGN KEY ([standard_sets_key]) REFERENCES [tbl_standard_sets]([standard_sets_key])
)
