USE [RiverWatch]
GO

update  [dbo].[NEWexpWater] 
          set  
		  [NEWexpWater].StationName = [RiverWatch].[dbo].Station.StationName
           ,  [NEWexpWater].RiverName = [RiverWatch].[dbo].Station.River
           ,[NEWexpWater].WaterShed = [RiverWatch].[dbo].Station.RWWaterShed
		   , [NEWexpWater].StationNumber =  [RiverWatch].[dbo].Station.StationNumber


  FROM [dbo].[NEWexpWater] ,  [RiverWatch].[dbo].Station

  where 
  [NEWexpWater].StationID = [RiverWatch].[dbo].Station.ID

  
GO

--alter table [dbo].[NEWexpWater] 
--add StationNumber int
