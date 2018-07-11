--
--    Table [dbo].[tbl_jurisdictions]
--
CREATE TABLE [dbo].[tbl_jurisdictions]
(
    [jurisdictions_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_jurisdictions_jurisdictions_key] DEFAULT(NEWSEQUENTIALID()),

    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL,

    [jurisdictions_id] NVARCHAR(250) NOT NULL,
    [jurisdictions_title] NVARCHAR(100) NULL,

    CONSTRAINT [pk_tbl_jurisdictions_jurisdictions_key] PRIMARY KEY NONCLUSTERED ([jurisdictions_key]),

    CONSTRAINT [fk_tbl_jurisdictions_standard_sets_key] FOREIGN KEY ([standard_sets_key]) REFERENCES [tbl_standard_sets]([standard_sets_key])
)
