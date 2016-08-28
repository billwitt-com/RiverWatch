
-- this works well and puts brackets around column names to prevent t-sql from thinking they are key words
-- bwitt may 10, 2016

declare @colnames varchar(max) = ''
declare @Dontwant varchar(50) = 'organizationid' 
select  @colnames = @colnames + '],[' + COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE 
TABLE_NAME='tblOrganization' and COLUMN_NAME <> @Dontwant 

SET @colnames=RIGHT(@colnames,LEN(@colnames)-2)	-- this removes the leading junk

Set @colnames = @colnames + ']' 

select @colnames

