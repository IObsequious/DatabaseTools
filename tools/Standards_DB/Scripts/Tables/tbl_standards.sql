--
--    Table [dbo].[tbl_standards]
--
CREATE TABLE [dbo].[tbl_standards]
(
    [standards_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_standards_standards_key] DEFAULT(NEWSEQUENTIALID()),

    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL,

    [standards_id] NVARCHAR(250) NULL,
    [standards_asnIdentifier] NVARCHAR(250) NULL,
    [standards_position] INT NULL,
    [standards_depth] INT NULL,
    [standards_statementNotation] NVARCHAR(250) NULL,
    [standards_statementLabel] NVARCHAR(250) NULL,
    [standards_listId] NVARCHAR(250) NULL,
    [standards_description] NVARCHAR(MAX),
    [standards_ancestorIds] NVARCHAR(MAX)



    CONSTRAINT [pk_tbl_standards_standards_key] PRIMARY KEY NONCLUSTERED ([standards_key]),

    CONSTRAINT [fk_tbl_standards_standard_sets_key] FOREIGN KEY ([standard_sets_key]) REFERENCES [tbl_standard_sets]([standard_sets_key])
)
