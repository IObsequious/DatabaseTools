--
--	View [dbo].[v_standards]	
--
CREATE VIEW [dbo].[v_standards]
AS
(
SELECT                   [SS].[standard_sets_id],
                         [SS].[standard_sets_title],
                         [SS].[standard_sets_subject],
                         [SS].[standard_sets_educationLevels],
                         [L].[licenses_title],
                         [L].[licenses_url],
                         [L].[licenses_rightsHolder],
                         [J].[jurisdictions_id],
                         [J].[jurisdictions_title],
                         [D].[documents_id], 
                         [D].[documents_asnIdentifier], 
                         [D].[documents_publicationStatus], 
                         [D].[documents_title], 
                         [D].[documents_valid], 
                         [D].[documents_sourceURL],
                         [S].[standards_id],
                         [S].[standards_asnIdentifier],
                         [S].[standards_position],
                         [S].[standards_depth],
                         [S].[standards_statementNotation],
                         [S].[standards_statementLabel],
                         [S].[standards_listId],
                         [S].[standards_description],
                         [S].[standards_ancestorIds]


FROM                    [dbo].[tbl_standard_sets] AS [SS]
INNER JOIN              [dbo].[tbl_licenses]      AS [L]   ON [SS].[standard_sets_key] = [L].[standard_sets_key]
INNER JOIN              [dbo].[tbl_jurisdictions] AS [J]   ON [SS].[standard_sets_key] = [J].[standard_sets_key]
INNER JOIN              [dbo].[tbl_documents]     AS [D]   ON [SS].[standard_sets_key] = [D].[standard_sets_key]
INNER JOIN              [dbo].[tbl_standards]     AS [S]   ON [SS].[standard_sets_key] = [S].[standard_sets_key]


)
