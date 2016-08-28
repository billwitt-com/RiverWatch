USE [RiverWatch]
GO

UPDATE [dbo].[NEWexpWater]
   SET  
       [USGS_Flow] = F.USGSFlow
      ,[PH] = F.PH
      ,[TempC] = F.TempC
      ,[PHEN_ALK] =F.PhenAlk
      ,[TOTAL_ALK] = F.TotalAlk
      ,[TOTAL_HARD] = F.TotalHard
      ,[DO_MGL] =F.DO
      ,[DOSAT] = F.DOsat   
      ,[FieldComment] = F.Comments   
   from  [NEWexpWater],  [dbRiverwatchWaterData].[dbo].[expStnField] F

WHERE  [NEWexpWater].SampleNumber = F.SampleNumber and F.ProjectID = 1
GO

