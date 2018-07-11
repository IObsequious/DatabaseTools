--
--    Table [dbo].[tbl_standard_sets]
--
CREATE TABLE [dbo].[tbl_standard_sets]
(
    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_standard_sets_standard_sets_key] DEFAULT(NEWSEQUENTIALID()),
    [jurisdictions_key] UNIQUEIDENTIFIER NOT NULL,
    [standard_sets_id] NVARCHAR(250) NULL,
    [standard_sets_subject] NVARCHAR(250) NULL, 
    [standard_sets_title] NVARCHAR(250) NULL, 
    [standard_sets_educationLevels] NVARCHAR(MAX) NULL,
    CONSTRAINT [pk_tbl_standard_sets_standard_sets_key] PRIMARY KEY NONCLUSTERED ([standard_sets_key]),
    CONSTRAINT [pk_tbl_standard_sets_jurisdictions_key] FOREIGN KEY ([jurisdictions_key]) REFERENCES [tbl_jurisdictions]([jurisdictions_key])
)
