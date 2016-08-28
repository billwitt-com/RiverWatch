
-- script to move contants tables to new tables in RiverWatch and populate
-- adds column ID as PK and identity 
use riverwatch 

-- tlkActivityCategory ****
IF OBJECT_ID('RiverWatch.[dbo].tlkActivityCategory', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkActivityCategory
select *
	into  RiverWatch.[dbo].tlkActivityCategory
	from [dbRiverwatchWaterData].[dbo].tlkActivityCategory

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkActivityCategory' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkActivityCategory
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkActivityCategory
set [Valid] = 1
go

-- add new "RowId" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkActivityCategory' AND COLUMN_NAME = 'ID')
Begin
ALTER TABLE RiverWatch.[dbo].tlkActivityCategory
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkActivityCategory
ADD CONSTRAINT PK_tlkActivityCategory
PRIMARY KEY CLUSTERED (ID)
end
GO

-- tblPartInfo
IF OBJECT_ID('RiverWatch.[dbo].tblPartInfo', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblPartInfo
select *
	into  RiverWatch.[dbo].tblPartInfo
	from [dbRiverwatchWaterData].[dbo].tblPartInfo

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblPartInfo' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblPartInfo
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblPartInfo
set [Valid] = 1
go

-- add new "RowId" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblPartInfo' AND COLUMN_NAME = 'ID')
Begin
ALTER TABLE RiverWatch.[dbo].tblPartInfo
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblPartInfo
ADD CONSTRAINT PK_tblPartInfo
PRIMARY KEY CLUSTERED (ID)
end
GO

-- tblParticipant  ****
 IF OBJECT_ID('RiverWatch.[dbo].tblParticipant', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblParticipant
select *
	into  RiverWatch.[dbo].tblParticipant
	from [dbRiverwatchWaterData].[dbo].tblParticipant

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblParticipant' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblParticipant
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblParticipant
set [Valid] = 1
go

-- add new "RowId" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblParticipant' AND COLUMN_NAME = 'ID')

Begin
 -- ALTER TABLE tblParticipant DROP CONSTRAINT PK_ParticipantID	-- unique case for this table
  ALTER TABLE tblParticipant DROP column ParticipantID

ALTER TABLE RiverWatch.[dbo].tblParticipant
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblParticipant
ADD CONSTRAINT PK_ID
PRIMARY KEY CLUSTERED (ID)
  end
GO

-- tblPhysHabPara ***** 
IF OBJECT_ID('RiverWatch.[dbo].tblPhysHabPara', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblPhysHabPara
select *
	into  RiverWatch.[dbo].tblPhysHabPara
	from [dbRiverwatchWaterData].[dbo].tblPhysHabPara

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblPhysHabPara' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblPhysHabPara
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblPhysHabPara
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblPhysHabPara' AND COLUMN_NAME = 'ID')

Begin
-- ALTER TABLE tblPhysHabPara DROP CONSTRAINT PK_PhysHabPara
  ALTER TABLE tblPhysHabPara DROP column PhysHabParaID

ALTER TABLE RiverWatch.[dbo].tblPhysHabPara
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblPhysHabPara
ADD CONSTRAINT PK_tblPhysHabPara
PRIMARY KEY CLUSTERED (ID)
end

-- tblProject ****
 IF OBJECT_ID('RiverWatch.[dbo].tblProject', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblProject
select *
	into  RiverWatch.[dbo].tblProject
	from [dbRiverwatchWaterData].[dbo].tblProject

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblProject' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblProject
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblProject
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblProject' AND COLUMN_NAME = 'ID')

Begin
 
-- ALTER TABLE tblProject DROP CONSTRAINT tblProject_PK
ALTER TABLE tblProject DROP column ProjectID
ALTER TABLE RiverWatch.[dbo].tblProject
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblProject
ADD CONSTRAINT [PK_tblProject_1]
PRIMARY KEY CLUSTERED (ID)
end

-- tblRegistration  ****
IF OBJECT_ID('RiverWatch.[dbo].tblRegistration', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblRegistration
select *
	into  RiverWatch.[dbo].tblRegistration
	from [dbRiverwatchWaterData].[dbo].tblRegistration

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblRegistration' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblRegistration
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblRegistration
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblRegistration' AND COLUMN_NAME = 'ID')

Begin
 -- ALTER TABLE tblRegistration DROP CONSTRAINT [RegistrationID]
  ALTER TABLE tblRegistration DROP column RegistrationID
ALTER TABLE RiverWatch.[dbo].tblRegistration
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblRegistration
ADD CONSTRAINT PK_tblRegistration
PRIMARY KEY CLUSTERED (ID)
end

-- tblWatercodes  ****
IF OBJECT_ID('RiverWatch.[dbo].tblWatercodes', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblWatercodes
select *
	into  RiverWatch.[dbo].tblWatercodes
	from [dbRiverwatchWaterData].[dbo].tblWatercodes

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWatercodes' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblWatercodes
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblWatercodes
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWatercodes' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tblWatercodes
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblWatercodes
ADD CONSTRAINT PK_tblWatercodes
PRIMARY KEY CLUSTERED (ID)
end
 
-- tblWatershedGrp  ****
IF OBJECT_ID('RiverWatch.[dbo].tblWatershedGrp', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblWatershedGrp
select *
	into  RiverWatch.[dbo].tblWatershedGrp
	from [dbRiverwatchWaterData].[dbo].tblWatershedGrp

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWatershedGrp' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblWatershedGrp
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblWatershedGrp
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWatershedGrp' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tblWatershedGrp
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblWatershedGrp
ADD CONSTRAINT PK_tblWatershedGrp
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkCounty
  IF OBJECT_ID('RiverWatch.[dbo].tlkCounty', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkCounty
select *
	into  RiverWatch.[dbo].tlkCounty
	from [dbRiverwatchWaterData].[dbo].tlkCounty

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkCounty' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkCounty
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkCounty
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkCounty' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tlkCounty
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkCounty
ADD CONSTRAINT PK_tlkCounty
PRIMARY KEY CLUSTERED (ID)
end
  
  -- tblWBKey
  IF OBJECT_ID('RiverWatch.[dbo].tblWBKey', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblWBKey
select *
	into  RiverWatch.[dbo].tblWBKey
	from [dbRiverwatchWaterData].[dbo].tblWBKey

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWBKey' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblWBKey
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblWBKey
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblWBKey' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tblWBKey
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblWBKey
ADD CONSTRAINT PK_tblWBKey
PRIMARY KEY CLUSTERED (ID)
end
  

  -- tblXSReps ****
  IF OBJECT_ID('RiverWatch.[dbo].tblXSReps', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblXSReps
select *
	into  RiverWatch.[dbo].tblXSReps
	from [dbRiverwatchWaterData].[dbo].tblXSReps

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblXSReps' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblXSReps
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblXSReps
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblXSReps' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tblXSReps
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblXSReps
ADD CONSTRAINT PK_tblXSReps
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkActivityType ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkActivityType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkActivityType
select *
	into  RiverWatch.[dbo].tlkActivityType
	from [dbRiverwatchWaterData].[dbo].tlkActivityType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkActivityType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkActivityType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkActivityType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkActivityType' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkActivityType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkActivityType
ADD CONSTRAINT PK_tlkActivityType
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkBioResultsType ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkBioResultsType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkBioResultsType
select *
	into  RiverWatch.[dbo].tlkBioResultsType
	from [dbRiverwatchWaterData].[dbo].tlkBioResultsType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkBioResultsType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkBioResultsType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkBioResultsType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkBioResultsType' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkBioResultsType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkBioResultsType
ADD CONSTRAINT PK_tlkBioResultsType
PRIMARY KEY CLUSTERED (ID)
end
 

 -- tlkCommunities *****
 IF OBJECT_ID('RiverWatch.[dbo].tlkCommunities', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkCommunities
select *
	into  RiverWatch.[dbo].tlkCommunities
	from [dbRiverwatchWaterData].[dbo].tlkCommunities

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkCommunities' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkCommunities
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkCommunities
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkCommunities' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkCommunities
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkCommunities
ADD CONSTRAINT PK_tlkCommunities
PRIMARY KEY CLUSTERED (ID)
end
  
 -- tblUser 
  IF OBJECT_ID('RiverWatch.[dbo].tblUser', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tblUser
select *
	into  RiverWatch.[dbo].tblUser
	from [dbRiverwatchWaterData].[dbo].tblUser

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblUser' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tblUser
	add [Valid] bit
END
go
update RiverWatch.[dbo].tblUser
set [Valid] = 1

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblUser' AND COLUMN_NAME = 'ID')

Begin

-- ALTER TABLE tblPhysHabPara DROP CONSTRAINT UserID
 ALTER TABLE tblUser DROP column UserID

ALTER TABLE RiverWatch.[dbo].tblUser
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tblUser
ADD CONSTRAINT PK_tblUser
PRIMARY KEY CLUSTERED (ID)
end

-- tlkEcoRegion 
IF OBJECT_ID('RiverWatch.[dbo].tlkEcoRegion', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkEcoRegion
select *
	into  RiverWatch.[dbo].tlkEcoRegion
	from [dbRiverwatchWaterData].[dbo].tlkEcoRegion

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkEcoRegion' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkEcoRegion
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkEcoRegion
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblProject' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkEcoRegion
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkEcoRegion
ADD CONSTRAINT PK_tlkEcoRegion
PRIMARY KEY CLUSTERED (ID)
end
  
  -- tlkEquipCategory ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkEquipCategory', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkEquipCategory
select *
	into  RiverWatch.[dbo].tlkEquipCategory
	from [dbRiverwatchWaterData].[dbo].tlkEquipCategory

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkEquipCategory' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkEquipCategory
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkEquipCategory
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkEquipCategory' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkEquipCategory
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkEquipCategory
ADD CONSTRAINT PK_tlkEquipCategory
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkEquipItems ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkEquipItems', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkEquipItems
select *
	into  RiverWatch.[dbo].tlkEquipItems
	from [dbRiverwatchWaterData].[dbo].tlkEquipItems

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkEquipItems' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkEquipItems
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkEquipItems
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkEquipItems' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkEquipItems
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkEquipItems
ADD CONSTRAINT PK_tlkEquipItems
PRIMARY KEY CLUSTERED (ID)
end 

  -- tlkFieldGear ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkFieldGear', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkFieldGear
select *
	into  RiverWatch.[dbo].tlkFieldGear
	from [dbRiverwatchWaterData].[dbo].tlkFieldGear

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkFieldGear' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkFieldGear
	add [Valid] bit	
END
go
update RiverWatch.[dbo].tlkFieldGear
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkFieldGear' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkFieldGear
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkFieldGear
ADD CONSTRAINT PK_tlkFieldGear
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkFieldProcedure *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkFieldProcedure', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkFieldProcedure
select *
	into  RiverWatch.[dbo].tlkFieldProcedure
	from [dbRiverwatchWaterData].[dbo].tlkFieldProcedure

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkFieldProcedure' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkFieldProcedure
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkFieldProcedure
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkFieldProcedure' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tlkFieldProcedure
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkFieldProcedure
ADD CONSTRAINT PK_tlkFieldProcedure
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkGearConfig ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkGearConfig', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkGearConfig
select *
	into  RiverWatch.[dbo].tlkGearConfig
	from [dbRiverwatchWaterData].[dbo].tlkGearConfig

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkGearConfig' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkGearConfig
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkGearConfig
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkGearConfig' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkGearConfig
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkGearConfig
ADD CONSTRAINT PK_tlkGearConfig
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkGrid ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkGrid', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkGrid
select *
	into  RiverWatch.[dbo].tlkGrid
	from [dbRiverwatchWaterData].[dbo].tlkGrid

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkGrid' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkGrid
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkGrid
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkGrid' AND COLUMN_NAME = 'ID')

Begin
 --ALTER TABLE tblPhysHabPara DROP CONSTRAINT PK_tblPhysHabPara
 -- ALTER TABLE tblPhysHabPara DROP column PhysHabParaID

ALTER TABLE RiverWatch.[dbo].tlkGrid
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkGrid
ADD CONSTRAINT PK_tlkGrid
PRIMARY KEY CLUSTERED (ID)
end
  
  -- tlkHydroUnit ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkHydroUnit', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkHydroUnit
select *
	into  RiverWatch.[dbo].tlkHydroUnit
	from [dbRiverwatchWaterData].[dbo].tlkHydroUnit

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkHydroUnit' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkHydroUnit
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkHydroUnit
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkHydroUnit' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkHydroUnit
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkHydroUnit
ADD CONSTRAINT PK_tlkHydroUnitt
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkMetalBarCodeType ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkMetalBarCodeType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkMetalBarCodeType
select *
	into  RiverWatch.[dbo].tlkMetalBarCodeType
	from [dbRiverwatchWaterData].[dbo].tlkMetalBarCodeType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkMetalBarCodeType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkMetalBarCodeType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkMetalBarCodeType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkMetalBarCodeType' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkMetalBarCodeType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkMetalBarCodeType
ADD CONSTRAINT PK_tlkMetalBarCodeType
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkNutrientBarCodeType  *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkNutrientBarCodeType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkNutrientBarCodeType
select *
	into  RiverWatch.[dbo].tlkNutrientBarCodeType
	from [dbRiverwatchWaterData].[dbo].tlkNutrientBarCodeType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkNutrientBarCodeType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkNutrientBarCodeType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkNutrientBarCodeType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkNutrientBarCodeType' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkNutrientBarCodeType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkNutrientBarCodeType
ADD CONSTRAINT PK_tlkNutrientBarCodeType
PRIMARY KEY CLUSTERED (ID)
end
  
  -- tlkOrganizationType *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkOrganizationType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkOrganizationType
select *
	into  RiverWatch.[dbo].tlkOrganizationType
	from [dbRiverwatchWaterData].[dbo].tlkOrganizationType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkOrganizationType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkOrganizationType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkOrganizationType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkOrganizationType' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tlkOrganizationType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkOrganizationType
ADD CONSTRAINT PK_tlkOrganizationType
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkQUADI *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkQUADI', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkQUADI
select *
	into  RiverWatch.[dbo].tlkQUADI
	from [dbRiverwatchWaterData].[dbo].tlkQUADI

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkQUADI' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkQUADI
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkQUADI
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkQUADI' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkQUADI
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkQUADI
ADD CONSTRAINT PK_tlkQUADI
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkQuarterSection *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkQuarterSection', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkQuarterSection
select *
	into  RiverWatch.[dbo].tlkQuarterSection
	from [dbRiverwatchWaterData].[dbo].tlkQuarterSection

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkQuarterSection' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkQuarterSection
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkQuarterSection
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkQuarterSection' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkQuarterSection
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkQuarterSection
ADD CONSTRAINT PK_tlkQuarterSection
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkSection ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkSection', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkSection
select *
	into  RiverWatch.[dbo].tlkSection
	from [dbRiverwatchWaterData].[dbo].tlkSection

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkSection' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkSection
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkSection
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkSection' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkSection
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkSection
ADD CONSTRAINT PK_tlkSection
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkRange *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkRange', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkRange
select *
	into  RiverWatch.[dbo].tlkRange
	from [dbRiverwatchWaterData].[dbo].tlkRange

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkRange' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkRange
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkRange
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkRange' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkRange
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkRange
ADD CONSTRAINT PK_tlkRange
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkRiverWatchWaterShed ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkRiverWatchWaterShed', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkRiverWatchWaterShed
select *
	into  RiverWatch.[dbo].tlkRiverWatchWaterShed
	from [dbRiverwatchWaterData].[dbo].tlkRiverWatchWaterShed

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkRiverWatchWaterShed' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkRiverWatchWaterShed
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkRiverWatchWaterShed
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkRiverWatchWaterShed' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkRiverWatchWaterShed
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkRiverWatchWaterShed
ADD CONSTRAINT PK_tlkRiverWatchWaterShed
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkSampleType ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkSampleType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkSampleType
select *
	into  RiverWatch.[dbo].tlkSampleType
	from [dbRiverwatchWaterData].[dbo].tlkSampleType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkSampleType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkSampleType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkSampleType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkSampleType' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tlkSampleType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkSampleType
ADD CONSTRAINT PK_tlkSampleType
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkState ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkState', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkState
select *
	into  RiverWatch.[dbo].tlkState
	from [dbRiverwatchWaterData].[dbo].tlkState

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkState' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkState
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkState
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkState' AND COLUMN_NAME = 'ID')
Begin

ALTER TABLE RiverWatch.[dbo].tlkState
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkState
ADD CONSTRAINT PK_tlkState
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkStationQUAD ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkStationQUAD', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkStationQUAD
select *
	into  RiverWatch.[dbo].tlkStationQUAD
	from [dbRiverwatchWaterData].[dbo].tlkStationQUAD

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationQUAD' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkStationQUAD
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkStationQUAD
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationQUAD' AND COLUMN_NAME = 'ID')

Begin
ALTER TABLE RiverWatch.[dbo].tlkStationQUAD
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkStationQUAD
ADD CONSTRAINT PK_tlkStationQUAD
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkStationType *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkStationType', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkStationType
select *
	into  RiverWatch.[dbo].tlkStationType
	from [dbRiverwatchWaterData].[dbo].tlkStationType

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationType' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkStationType
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkStationType
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationType' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkStationType
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkStationType
ADD CONSTRAINT PK_tlkStationType
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkTownship *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkTownship', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkTownship
select *
	into  RiverWatch.[dbo].tlkTownship
	from [dbRiverwatchWaterData].[dbo].tlkTownship

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkTownship' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkTownship
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkTownship
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkTownship' AND COLUMN_NAME = 'ID')
BEGIN
ALTER TABLE RiverWatch.[dbo].tlkTownship
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkTownship
ADD CONSTRAINT PK_tlkTownship
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkWQCCWaterShed *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkWQCCWaterShed', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkWQCCWaterShed
select *
	into  RiverWatch.[dbo].tlkWQCCWaterShed
	from [dbRiverwatchWaterData].[dbo].tlkWQCCWaterShed
	go

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWQCCWaterShed' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkWQCCWaterShed
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkWQCCWaterShed
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWQCCWaterShed' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkWQCCWaterShed
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkWQCCWaterShed
ADD CONSTRAINT PK_tlkWQCCWaterShed
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkWSG *****
  IF OBJECT_ID('RiverWatch.[dbo].tlkWSG', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkWSG
select *
	into  RiverWatch.[dbo].tlkWSG
	from [dbRiverwatchWaterData].[dbo].tlkWSG

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWSG' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkWSG
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkWSG
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWSG' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkWSG
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkWSG
ADD CONSTRAINT PK_tlkWSG
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkWSR ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkWSR', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkWSR
select *
	into  RiverWatch.[dbo].tlkWSR
	from [dbRiverwatchWaterData].[dbo].tlkWSR

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWSR' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkWSR
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkWSR
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkWSR' AND COLUMN_NAME = 'ID')

Begin

ALTER TABLE RiverWatch.[dbo].tlkWSR
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkWSR
ADD CONSTRAINT PK_tlkWSR
PRIMARY KEY CLUSTERED (ID)
end

  -- trsChemParaMapColumns ****
  IF OBJECT_ID('RiverWatch.[dbo].trsChemParaMapColumns', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].trsChemParaMapColumns
select *
	into  RiverWatch.[dbo].trsChemParaMapColumns
	from [dbRiverwatchWaterData].[dbo].trsChemParaMapColumns
go
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'trsChemParaMapColumns' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].trsChemParaMapColumns
	add [Valid] bit
END
go
update RiverWatch.[dbo].trsChemParaMapColumns
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'trsChemParaMapColumns' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].trsChemParaMapColumns
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].trsChemParaMapColumns
ADD CONSTRAINT PK_trsChemParaMapColumns
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkStationStatus ****
  IF OBJECT_ID('RiverWatch.[dbo].tlkStationStatus', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkStationStatus
select *
	into  RiverWatch.[dbo].tlkStationStatus
	from [dbRiverwatchWaterData].[dbo].tlkStationStatus

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationStatus' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkStationStatus
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkStationStatus
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkStationStatus' AND COLUMN_NAME = 'ID')

Begin
 
ALTER TABLE RiverWatch.[dbo].tlkStationStatus
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkStationStatus
ADD CONSTRAINT PK_tblProject
PRIMARY KEY CLUSTERED (ID)
end

  -- tlkregion ****
  IF OBJECT_ID('RiverWatch.[dbo].tblProject', 'U') IS NOT NULL 
  DROP TABLE RiverWatch.[dbo].tlkregion
select *
	into  RiverWatch.[dbo].tlkregion
	from [dbRiverwatchWaterData].[dbo].tlkregion

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkregion' AND COLUMN_NAME = 'Valid')
BEGIN
	ALTER TABLE RiverWatch.[dbo].tlkregion
	add [Valid] bit
END
go
update RiverWatch.[dbo].tlkregion
set [Valid] = 1
go

-- add new "ID" column, make it IDENTITY (= auto-incrementing)
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tlkregion' AND COLUMN_NAME = 'ID')

Begin


ALTER TABLE RiverWatch.[dbo].tlkregion
ADD ID INT IDENTITY(1,1)
ALTER TABLE RiverWatch.[dbo].tlkregion
ADD CONSTRAINT PK_tlkregion
PRIMARY KEY CLUSTERED (ID)
end
