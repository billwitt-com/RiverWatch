USE [RiverWatch]
GO

UPDATE [dbo].[NEWexpWater]
   SET
      [AL_D] = E.AL_D
      ,[AL_T] = E.AL_T
      ,[AS_D] = E.AS_D
      ,[AS_T] = E.AS_T
      ,[CA_D] = E.CA_D
      ,[CA_T] = E.CA_T
      ,[CD_D] = E.CD_D
      ,[CD_T] = E.CD_T
      ,[CU_D] = E.CU_D
      ,[CU_T] = E.CU_T
      ,[FE_D] = E.FE_D
      ,[FE_T] = E.FE_T
      ,[MG_D] = E.MG_D
      ,[MG_T] = E.MG_T
      ,[MN_D] = E.MN_D
      ,[MN_T] = E.MN_T
      ,[PB_D] = E.PB_D
      ,[PB_T] = E.PB_T
      ,[SE_D] = E.SE_D
      ,[SE_T] = E.SE_T
      ,[ZN_D] = E.ZN_D
      ,[ZN_T] = E.ZN_T
      ,[NA_D] = E.NA_D
      ,[NA_T] = E.NA_T
      ,[K_D] = E.K_D
      ,[K_T] = E.K_T
	  ,MetalsComment = E.comments	 
	   
	  FROM [dbo].[NEWexpWater],  [dbRiverwatchWaterData].[dbo].[expStnMetal] E     
 WHERE [dbo].[NEWexpWater].SampleNumber = E.SampleNumber    and E.ProjectID = 1 
 go

 UPDATE [dbo].[NEWexpWater]
   SET
   MetalsBarCode =  M.LabID
   , TypeCode = M.Code

   from  NEWexpWater W, MetalBarCode M
   where W.SampleNumber = M.SampleNumber 



   go
