--
--	View [dbo].[v_jurisdictions]	
--
CREATE VIEW [dbo].[v_jurisdictions]
AS
(
SELECT                      [J].[jurisdictions_id], 
                            [J].[jurisdictions_title], 
                            [J].[jurisdictions_type], 
                            [S].[standard_sets_id], 
                            [S].[standard_sets_subject], 
                            [S].[standard_sets_title], 
                            [S].[standard_sets_educationLevels], 
                            [D].[documents_id], 
                            [D].[documents_asnIdentifier], 
                            [D].[documents_publicationStatus], 
                            [D].[documents_title], 
                            [D].[documents_valid], 
                            [D].[documents_sourceURL]


FROM                      [dbo].[tbl_jurisdictions] AS [J]
               INNER JOIN [dbo].[tbl_standard_sets] AS [S] ON [J].[jurisdictions_key] = [S].[jurisdictions_key]
               INNER JOIN [dbo].[tbl_documents]     AS [D] ON [S].[standard_sets_key] = [D].[standard_sets_key]
               CROSS APPLY OPENJSON([s].[standard_sets_educationLevels]) WITH ([educationLevel] NVARCHAR(20))
); 
