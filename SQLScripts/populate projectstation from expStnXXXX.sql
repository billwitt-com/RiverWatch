/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT TOP 1000 [ID]
--      ,[ProjectID]
--      ,[StationNumber]
--      ,[DateCreated]
--      ,[UserCreated]
--      ,[Valid]
--  FROM
  
  insert into 
   [RiverWatch].[dbo].[ProjectStation]
(       
	   [ProjectID]
      ,[StationNumber]
      ,[DateCreated]
      ,[UserCreated]
      ,[Valid]
)
select
distinct
	[ProjectID]
	,[StationNum]
	  ,[CreatedDate] = GetDate()
	   ,[CreatedBy] = 'System Migration'
	  ,1
	    FROM [dbRiverwatchWaterData].[dbo].[expStnField]


 -- delete  [RiverWatch].[dbo].[ProjectStation]

