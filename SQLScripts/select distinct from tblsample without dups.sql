





select  distinct 
      [StationID]
      ,[OrganizationID]
      ,[SampleNumber]
      ,[NumberSample]
      ,[DateCollected]
      ,[TimeCollected]
      ,[DateReceived]
      ,[DataSheetIncluded]
      ,[MissingDataSheetReqDate]
      ,[ChainOfCustody]
      ,[MissingDataSheetReceived]
      ,[NoMetals]
      ,[PhysicalHabitat]
      ,[Bug]
      ,[NoNutrient]
      ,[TotalSuspendedSolids]
      ,[NitratePhosphorus]
      ,[DuplicatedTSS]
      ,[DuplicatedNP]
      ,[Comment]
      ,[DateCreated]
      ,[UserCreated]
      ,[DateLastModified]
      ,[UserLastModified]
      ,[ChlorideSulfate]
      ,[BlankMetals]
      ,[DuplicatedMetals]
      ,[BugsQA]
      ,[DuplicatedCS]
      ,[Valid]
FROM		[dbRiverwatchWaterData].[dbo].[tblSample]
where	SampleNumber in 

(SELECT   [SampleNumber]  -- , COUNT(*) AS dupes  --, DateCollected
FROM		[dbRiverwatchWaterData].[dbo].[tblSample]
GROUP BY [SampleNumber]  
HAVING      (COUNT([SampleNumber]) >2)  ) 
order by [dbRiverwatchWaterData].[dbo].[tblSample].[SampleNumber] desc

