  -- add new "RowId" column, make it IDENTITY (= auto-incrementing)
--ALTER TABLE [dbRiverwatchWaterData].[dbo].[expStnMetal]
--ADD ID INT IDENTITY(1,1)
--GO

---- add new primary key constraint on new column   
--ALTER TABLE [dbRiverwatchWaterData].[dbo].[expStnMetal]
--ADD CONSTRAINT PK_expStnMetal
--PRIMARY KEY CLUSTERED (ID)
--GO

--ALTER TABLE [dbRiverwatchWaterData].[dbo].[tblsample]
--add [Valid] bit

--update [dbRiverwatchWaterData].[dbo].[tblsample]
--set [Valid] = 1