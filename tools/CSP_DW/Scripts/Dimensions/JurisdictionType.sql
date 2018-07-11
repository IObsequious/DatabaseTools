--
--    Table [dbo].[JurisdictionType]	
--
CREATE TABLE [dbo].[JurisdictionType]
(
    [Id] UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL CONSTRAINT [DF_JurisdictionType_id] DEFAULT(NEWSEQUENTIALID()),

    [Name]  NVARCHAR (20)  NOT NULL,
    [Value] INT            NOT NULL,
   
    CONSTRAINT [PK_JurisdictionType_Id] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [UQ_JurisdictionType_Name] UNIQUE ([Name] ASC),
    CONSTRAINT [UQ_JurisdictionType_Value] UNIQUE ([Value] ASC)   
)
