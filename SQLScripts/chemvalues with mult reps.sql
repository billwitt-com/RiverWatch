/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ChemSampID]
      ,[RepNum]
      ,[ChemParamID]
      ,[ChemValue]
      ,[Comments]
      ,[EnterDate]
      ,[StoretUploaded]
  FROM [dbRiverwatchWaterData].[dbo].[tblChemValues]
  where ChemSampID = 27345