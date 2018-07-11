USE master;
GO

EXEC master.dbo.sp_dropdevice @logicalname = [STANDARDBACKUP], -- sysname
                              @delfile = 'DELFILE'        -- varchar(7)


EXEC master.dbo.sp_addumpdevice  @devtype = N'disk', 
                                 @logicalname = N'STANDARDBACKUP', 
                                 @physicalname = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Backup\STANDARDBACKUP.bak'
GO

DROP PROC  [dbo].[sp_backup_databases];
GO

CREATE PROC [dbo].[sp_backup_databases] AS BEGIN SET NOCOUNT ON
DECLARE @JBackupName NVARCHAR(50) = CAST(CURRENT_TIMESTAMP AS NVARCHAR(50));
DECLARE @SBackupName NVARCHAR(50) = CAST(CURRENT_TIMESTAMP AS NVARCHAR(50));
BACKUP DATABASE [Jurisdictions_DB] TO [STANDARDBACKUP] WITH NAME = @JBackupName;
BACKUP DATABASE [Standards_DB] TO [STANDARDBACKUP] WITH NAME = @SBackupName;
RETURN @@ROWCOUNT
END
GO


EXEC dbo.sp_backup_databases;
GO