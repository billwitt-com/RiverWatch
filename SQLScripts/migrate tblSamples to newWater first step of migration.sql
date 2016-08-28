USE [RiverWatch]
GO

--   delete [dbo].[NEWexpWater]   

INSERT INTO [dbo].[NEWexpWater]         
     (      
			[SampleNumber]
           ,[Event]  
           ,[StationNumber]

		   ,[SampleDate]      
           ,[CreatedBy]
          ,[CreateDate]        
		   ,[SampleComments]
           ,[Valid]		   
		   ,[OrganizationID]
	)

select  distinct 
      [SampleNumber]
      ,[NumberSample]
      ,[StationID]
	   , convert(datetime, (convert (varchar(10),[DateCollected],111)   + ' ' + isnull(convert (varchar(10),[TimeCollected],108), '00:00:00') ))
		,[UserCreated]
		,[DateCreated]
		,[Comment]
		,[Valid]      
		,[OrganizationID]
FROM		[dbRiverwatchWaterData].[dbo].[tblSample]
where	SampleNumber in 

(SELECT   [SampleNumber]  -- , COUNT(*) AS dupes  --, DateCollected
FROM		[dbRiverwatchWaterData].[dbo].[tblSample]
GROUP BY [SampleNumber]  
HAVING      (COUNT([SampleNumber]) >2)  ) 

GO

update [dbo].[NEWexpWater]  
set  [dbo].[NEWexpWater].[tblSampleID] = [dbRiverwatchWaterData].[dbo].[tblSample].[SampleID] 
FROM		[dbRiverwatchWaterData].[dbo].[tblSample]
 where [dbRiverwatchWaterData].[dbo].[tblSample].SampleNumber = [dbo].[NEWexpWater].SampleNumber
	





















           --(<tblSampleID, int,>
           --,<SampleNumber, varchar(50),>
           --,<Event, varchar(12),>
           --,<WaterShed, varchar(3),>
           --,<River_CD, decimal(14,4),>
           --,<RiverName, varchar(17),>
           --,<KitNumber, int,>
           --,<OrganizationName, varchar(50),>
           --,<StationNumber, int,>
           --,<TypeCode, varchar(5),>
           --,<StationName, varchar(50),>
           --,<MetalsBarCode, varchar(20),>
           --,<NutrientBarCode, varchar(20),>
           --,<FieldBarCode, varchar(20),>
           --,<BugsBarCode, varchar(20),>
           --,<SampleDate, datetime,>
           --,<USGS_Flow, decimal(10,4),>
           --,<PH, decimal(10,4),>
           --,<TempC, decimal(10,4),>
           --,<PHEN_ALK, decimal(10,4),>
           --,<TOTAL_ALK, decimal(10,4),>
           --,<TOTAL_HARD, decimal(10,4),>
           --,<DO_MGL, decimal(10,4),>
           --,<DOSAT, decimal(10,4),>
           --,<AL_D, decimal(14,4),>
           --,<AL_T, decimal(14,4),>
           --,<AS_D, decimal(14,4),>
           --,<AS_T, decimal(14,4),>
           --,<CA_D, decimal(14,4),>
           --,<CA_T, decimal(14,4),>
           --,<CD_D, decimal(14,4),>
           --,<CD_T, decimal(14,4),>
           --,<CU_D, decimal(14,4),>
           --,<CU_T, decimal(14,4),>
           --,<FE_D, decimal(14,4),>
           --,<FE_T, decimal(14,4),>
           --,<MG_D, decimal(14,4),>
           --,<MG_T, decimal(14,4),>
           --,<MN_D, decimal(14,4),>
           --,<MN_T, decimal(14,4),>
           --,<PB_D, decimal(14,4),>
           --,<PB_T, decimal(14,4),>
           --,<SE_D, decimal(14,4),>
           --,<SE_T, decimal(14,4),>
           --,<ZN_D, decimal(14,4),>
           --,<ZN_T, decimal(14,4),>
           --,<NA_D, decimal(14,4),>
           --,<NA_T, decimal(14,4),>
           --,<K_D, decimal(14,4),>
           --,<K_T, decimal(14,4),>
           --,<Rep, int,>
           --,<Ammonia, decimal(14,4),>
           --,<Chloride, decimal(14,4),>
           --,<ChlorophyllA, decimal(14,4),>
           --,<DOC, decimal(14,4),>
           --,<NN, decimal(14,4),>
           --,<OP, decimal(14,4),>
           --,<Sulfate, decimal(10,4),>
           --,<totN, decimal(14,4),>
           --,<totP, decimal(14,4),>
           --,<TKN, decimal(14,4),>
           --,<orgN, decimal(14,4),>
           --,<TSS, decimal(14,4),>
           --,<FieldComment, nvarchar(max),>
           --,<NutrientComment, nvarchar(max),>
           --,<MetalsComment, nvarchar(max),>
           --,<BugsComments, nvarchar(max),>
           --,<BenthicsComments, nvarchar(max),>
           --,<CreatedBy, varchar(50),>
           --,<CreateDate, datetime,>
           --,<BadBlank, bit,>
           --,<BadDuplicate, bit,>
           --,<BadSample, bit,>
           --,<Valid, bit,>
           --,<OrganizationID, int,>)
GO


