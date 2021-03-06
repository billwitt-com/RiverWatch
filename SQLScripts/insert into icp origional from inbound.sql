/****** Script for SelectTopNRows command from SSMS  ******/
declare @user varchar(40)
set @user = 'Bill'

 insert into [RiverWatch].[dbo].[InboundICPOrigional]
      ([CODE] 
      ,[tblSampleID]
      ,[DUPLICATE]
      ,[AL_D]
      ,[AL_T]
      ,[AS_D]
      ,[AS_T]
      ,[CA_D]
      ,[CA_T]
      ,[CD_D]
      ,[CD_T]
      ,[CU_D]
      ,[CU_T]
      ,[FE_D]
      ,[FE_T]
      ,[PB_D]
      ,[PB_T]
      ,[MG_D]
      ,[MG_T]
      ,[MN_D]
      ,[MN_T]
      ,[SE_D]
      ,[SE_T]
      ,[ZN_D]
      ,[ZN_T]
      ,[NA_D]
      ,[NA_T]
      ,[K_D]
      ,[K_T]
      ,[ANADATE]
      ,[COMPLETE]
      ,[DATE_SENT]
      ,[Comments]
      ,[CreatedBy] 
      ,[CreatedDate]
      ,[Valid]
      ,[Edited]
      ,[Saved]
	  )

select 
		[CODE]
       ,[tblSampleID]
      ,[DUPLICATE]
      ,[AL_D]
      ,[AL_T]
      ,[AS_D]
      ,[AS_T]
      ,[CA_D]
      ,[CA_T]
      ,[CD_D]
      ,[CD_T]
      ,[CU_D]
      ,[CU_T]
      ,[FE_D]
      ,[FE_T]
      ,[PB_D]
      ,[PB_T]
      ,[MG_D]
      ,[MG_T]
      ,[MN_D]
      ,[MN_T]
      ,[SE_D]
      ,[SE_T]
      ,[ZN_D]
      ,[ZN_T]
      ,[NA_D]
      ,[NA_T]
      ,[K_D]
      ,[K_T]
      ,[ANADATE]
      ,[COMPLETE]
      ,[DATE_SENT]
      ,[Comments]   
	  ,[CreatedBy] = @user
	  ,[CreatedDate] = GetDate()
	  ,[Valid] = 1
      ,[Edited] = 0
      ,[Saved] = 0

  FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] 

  where code not in 
  (
	select code  from   [RiverWatch].[dbo].[InboundICPOrigional]   
  ) 



   insert into [RiverWatch].[dbo].[InboundICPFinal]
      ([CODE] 
      ,[tblSampleID]
      ,[DUPLICATE]
      ,[AL_D]
      ,[AL_T]
      ,[AS_D]
      ,[AS_T]
      ,[CA_D]
      ,[CA_T]
      ,[CD_D]
      ,[CD_T]
      ,[CU_D]
      ,[CU_T]
      ,[FE_D]
      ,[FE_T]
      ,[PB_D]
      ,[PB_T]
      ,[MG_D]
      ,[MG_T]
      ,[MN_D]
      ,[MN_T]
      ,[SE_D]
      ,[SE_T]
      ,[ZN_D]
      ,[ZN_T]
      ,[NA_D]
      ,[NA_T]
      ,[K_D]
      ,[K_T]
      ,[ANADATE]
      ,[COMPLETE]
      ,[DATE_SENT]
      ,[Comments]
      ,[CreatedBy] 
      ,[CreatedDate]
      ,[Valid]
      ,[Edited]
      ,[Saved]
	  )

select 
		[CODE]
       ,[tblSampleID]
      ,[DUPLICATE]
      ,[AL_D]
      ,[AL_T]
      ,[AS_D]
      ,[AS_T]
      ,[CA_D]
      ,[CA_T]
      ,[CD_D]
      ,[CD_T]
      ,[CU_D]
      ,[CU_T]
      ,[FE_D]
      ,[FE_T]
      ,[PB_D]
      ,[PB_T]
      ,[MG_D]
      ,[MG_T]
      ,[MN_D]
      ,[MN_T]
      ,[SE_D]
      ,[SE_T]
      ,[ZN_D]
      ,[ZN_T]
      ,[NA_D]
      ,[NA_T]
      ,[K_D]
      ,[K_T]
      ,[ANADATE]
      ,[COMPLETE]
      ,[DATE_SENT]
      ,[Comments]   
	  ,[CreatedBy] = @user
	  ,[CreatedDate] = GetDate()
	  ,[Valid] = 1
      ,[Edited] = 0
      ,[Saved] = 0

  FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] 

  where code not in 
  (
	select code  from   [RiverWatch].[dbo].[InboundICPFinal]
  ) 
go


