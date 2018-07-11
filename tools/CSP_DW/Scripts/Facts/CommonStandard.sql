--
--    Table [dbo].[CommonStandard]
--
CREATE TABLE [dbo].[CommonStandard]
(
    [RowId] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [DF_CommonStandard_RowId] DEFAULT(NEWSEQUENTIALID()),

    [Id] UNIQUEIDENTIFIER NOT NULL,
    
    [Description] NVARCHAR(4000) NOT NULL,

    CONSTRAINT [PK_CommonStandard_RowId] PRIMARY KEY NONCLUSTERED ([RowId])
)
