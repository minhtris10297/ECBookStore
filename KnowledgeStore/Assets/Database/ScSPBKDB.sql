USE KnowledgeStore
GO
CREATE PROCEDURE BackUpDatabaseBUD 
@ten varchar(50)
AS
BEGIN
DECLARE @name VARCHAR(50) -- database name  
DECLARE @path VARCHAR(256) -- path for backup files  
DECLARE @fileName VARCHAR(256) -- filename for backup  
DECLARE @fileDate VARCHAR(50) -- used for file name
SET @path = 'C:\\BackUpTest\\'  
SELECT @fileDate = CONVERT(VARCHAR(50),GETDATE(),112) 
DECLARE mydb CURSOR READ_ONLY FOR  
SELECT name 
FROM master.dbo.sysdatabases 
WHERE name = 'KnowledgeStore'  -- exclude these databases
OPEN mydb   
FETCH NEXT FROM mydb INTO @name   
WHILE @@FETCH_STATUS = 0   
BEGIN   
   SET @fileName = @path + @name + '_' + @fileDate + '_' + @ten + '.BAK'  
   BACKUP DATABASE @name TO DISK = @fileName  
 
   FETCH NEXT FROM mydb INTO @name   
END   
CLOSE mydb   
DEALLOCATE mydb
END
