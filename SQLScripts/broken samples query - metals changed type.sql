SELECT 
    [Extent1].[SampleID] AS [SampleID]
    FROM (SELECT 
    [InboundSamples].[inbSampleID] AS [inbSampleID], 
    [InboundSamples].[StationNum] AS [StationNum], 
    [InboundSamples].[SampleID] AS [SampleID], 
    [InboundSamples].[txtSampleID] AS [txtSampleID], 
    [InboundSamples].[KitNum] AS [KitNum], 
    [InboundSamples].[Date] AS [Date], 
    [InboundSamples].[Time] AS [Time], 
    [InboundSamples].[USGSFlow] AS [USGSFlow], 
    [InboundSamples].[PH] AS [PH], 
    [InboundSamples].[TempC] AS [TempC], 
    [InboundSamples].[PhenAlk] AS [PhenAlk], 
    [InboundSamples].[TotalAlk] AS [TotalAlk], 
    [InboundSamples].[TotalHard] AS [TotalHard], 
    [InboundSamples].[DO] AS [DO], 
    [InboundSamples].[DOsat] AS [DOsat], 
    [InboundSamples].[Tag] AS [Tag], 
    [InboundSamples].[Chk] AS [Chk], 
    [InboundSamples].[EntryType] AS [EntryType], 
    [InboundSamples].[EntryStaff] AS [EntryStaff], 
    [InboundSamples].[Metals] AS [Metals], 
    [InboundSamples].[MetalsBlnk] AS [MetalsBlnk], 
    [InboundSamples].[MetalsDupe] AS [MetalsDupe], 
    [InboundSamples].[Bugs] AS [Bugs], 
    [InboundSamples].[BugsQA] AS [BugsQA], 
    [InboundSamples].[TSS] AS [TSS], 
    [InboundSamples].[CS] AS [CS], 
    [InboundSamples].[NP] AS [NP], 
    [InboundSamples].[TSSDupe] AS [TSSDupe], 
    [InboundSamples].[CSDupe] AS [CSDupe], 
    [InboundSamples].[NPDupe] AS [NPDupe], 
    [InboundSamples].[FieldValid] AS [FieldValid], 
    [InboundSamples].[MetalsStat] AS [MetalsStat], 
    [InboundSamples].[FinalCheck] AS [FinalCheck], 
    [InboundSamples].[Method] AS [Method], 
    [InboundSamples].[Comments] AS [Comments], 
    [InboundSamples].[DateReceived] AS [DateReceived], 
    [InboundSamples].[DataSheetIncluded] AS [DataSheetIncluded], 
    [InboundSamples].[MissingDataSheetReqDate] AS [MissingDataSheetReqDate], 
    [InboundSamples].[ChainOfCustody] AS [ChainOfCustody], 
    [InboundSamples].[MissingDataSheetReceived] AS [MissingDataSheetReceived], 
    [InboundSamples].[PassValStep] AS [PassValStep], 
    [InboundSamples].[tblSampleID] AS [tblSampleID]
    FROM [dbo].[InboundSamples] AS [InboundSamples]) AS [Extent1]
    WHERE [Extent1].[KitNum] = 8000 AND ([Extent1].[StationNum] = 9876)
	
	
	-- @p__linq__0) AND ([Extent1].[StationNum] = @p__linq__1)',N'@p__linq__0 int,@p__linq__1 int',@p__linq__0=8000,@p__linq__1=9876