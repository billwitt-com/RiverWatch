

-- script to move Active tables to new tables in RiverWatch and populate
-- adds column ID as PK and identity 
use riverwatch 

-- organization
IF OBJECT_ID('RiverWatch.[dbo].organization', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].organization
select *
	into  RiverWatch.[dbo].organization
	from [dbRiverwatchWaterData].[dbo].tblOrganization

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'organization' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].organization
	add [Valid] bit
END
go
update RiverWatch.[dbo].organization
set [Valid] = 1
go

-- add new "RowId" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'organization' AND COLUMN_NAME = 'ID')
Begin
ALTER TABLE organization DROP column OrganizationID		-- was the old primary key, but not idenity
ALTER TABLE RiverWatch.[dbo].organization
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].organization
ADD CONSTRAINT PK_organization
PRIMARY KEY CLUSTERED (ID)
end
GO

-- project
IF OBJECT_ID('RiverWatch.[dbo].project', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].project
select *
	into  RiverWatch.[dbo].project
	from [dbRiverwatchWaterData].[dbo].tblProject

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'project' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].project
	add [Valid] bit
END
go
update RiverWatch.[dbo].project
set [Valid] = 1
go

ALTER TABLE RiverWatch.[dbo].project
ADD CONSTRAINT PK_Project
PRIMARY KEY CLUSTERED (ProjectID)

-- project
IF OBJECT_ID('RiverWatch.[dbo].project', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].project
select *
	into  RiverWatch.[dbo].project
	from [dbRiverwatchWaterData].[dbo].tblProject

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'project' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].project
	add [Valid] bit
END
go
update RiverWatch.[dbo].project
set [Valid] = 1
go

ALTER TABLE RiverWatch.[dbo].project
ADD CONSTRAINT PK_Project
PRIMARY KEY CLUSTERED (ProjectID)
