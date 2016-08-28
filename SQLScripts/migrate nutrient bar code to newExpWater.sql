   USE [RiverWatch]
GO

 UPDATE [dbo].[NEWexpWater]
   SET
NutrientBarCode = N.LabID
, NutrientComment = N.Comment
   from  NEWexpWater W, NutrientBarCode N
   where W.SampleNumber = N.SampleNumber

   go


UPDATE [dbo].[NEWexpWater]
   SET 
  
      [Ammonia] = N.Ammonia
      ,[Chloride] = N.Chloride
      ,[ChlorophyllA] = N.ChlorophyllA
    --  ,[DOC] = N.-- these are new, so no data to migrate
      ,[NN] = N.NN
      ,[OP] = N.OP
      ,[Sulfate] = N.Sulfate
      ,[totN] = N.totN
      ,[totP] = N.totP
  -- ,[TKN] = N.	-- these are new, so no data to migrate
      --,[orgN] = N.	-- these are new, so no data to migrate
      --,[TSS] = N.TSS -- these are new, so no data to migrate
      
	  from [NEWexpWater] , [dbRiverwatchWaterData].[dbo].[expStnNutrient] N
	  where  [NEWexpWater].SampleNumber = N.SampleNumber and N.ProjectID = 1

	  go

