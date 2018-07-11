--
--    Table [dbo].[tbl_standard_sets]
--
CREATE TABLE [dbo].[tbl_standard_sets]
(
    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_standard_sets_standard_sets_key] DEFAULT(NEWSEQUENTIALID()),

    [standard_sets_id] NVARCHAR(250) NULL,
    [standard_sets_subject] NVARCHAR(250) NULL,
    [standard_sets_title] NVARCHAR(250) NULL, 
    [standard_sets_educationLevels] NVARCHAR(MAX) NULL,
    [standard_sets_rightsHolder] NVARCHAR(250) NULL,

    CONSTRAINT [pk_tbl_standard_sets_id] PRIMARY KEY NONCLUSTERED ([standard_sets_key])
)
