/****** Script for SelectTopNRows command from SSMS  ******/





SELECT 
distinct
		S.[ID]
      ,[SampleID]
      ,[StationID]
      ,S.[OrganizationID]
      ,[SampleNumber]
      ,[NumberSample]
      , year([DateCollected]) as [DateCollected] 
	--  ,year([RiverWatch].[dbo].[OrgStatus].ContractStartDate) as [contractStart]
    
  FROM [RiverWatch].[dbo].[Samples] S,  [RiverWatch].[dbo].[OrgStatus]
  where
  S.DateCollected	NOT IN
(
select DateCollected
	from [RiverWatch].[dbo].[OrgStatus]
	where [RiverWatch].[dbo].[OrgStatus].OrganizationID = S.OrganizationID
	and  Year(DateCollected)  = year([RiverWatch].[dbo].[OrgStatus].ContractStartDate)
)


  --  SELECT ProductID, ProductName 
--FROM Northwind..Products p
--WHERE p.ProductID NOT IN (
--    SELECT ProductID 
--    FROM Northwind..[Order Details])