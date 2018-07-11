--
--    Table [dbo].[dim_jurisdictions]
--
CREATE TABLE [dbo].[tbl_jurisdictions]
(
    [jurisdictions_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_jurisdictions_jurisdictions_key] DEFAULT(NEWSEQUENTIALID()),

    [jurisdictions_id] NVARCHAR(250) NULL,
    [jurisdictions_title] NVARCHAR(100) NULL,
    [jurisdictions_type] NVARCHAR(20) NULL

    CONSTRAINT [pk_tbl_jurisdictions_id] PRIMARY KEY NONCLUSTERED ([jurisdictions_key])
)
