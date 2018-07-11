--
--    Table [dbo].[tbl_licenses]
--
CREATE TABLE [dbo].[tbl_licenses]
(
    [licenses_key] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [df_tbl_licenses_licenses_key] DEFAULT(NEWSEQUENTIALID()),

    [standard_sets_key] UNIQUEIDENTIFIER NOT NULL,

    [licenses_title] NVARCHAR(250) NULL,
    [licenses_url] NVARCHAR(500) NULL,
    [licenses_rightsHolder] NVARCHAR(250) NULL,

    CONSTRAINT [pk_tbl_licenses_id] PRIMARY KEY NONCLUSTERED ([licenses_key]),

    CONSTRAINT [fk_tbl_licenses_standard_sets_key] FOREIGN KEY ([standard_sets_key]) REFERENCES [tbl_standard_sets]([standard_sets_key])
)
