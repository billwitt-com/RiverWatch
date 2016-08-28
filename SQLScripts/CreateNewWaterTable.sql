USE [RiverWatch]
GO

/****** Object:  Table [dbo].[expWater]    Script Date: 4/21/2016 8:31:43 AM ******/
-- bwitt April 21, 2016
-- first pass at creating a data structure that will support all obvious reporting needs
-- as well as hold ALL samples. 
-- 06/13 made decision to name chem fields as AL_D rather than [ALDISUG] for simplicity and readability 
-- used both ways in many tables, now will be the same
-- successful after making and populating tblSample
-- chaged kit number to int nullable
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NEWexpWater]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[tblSampleID] [int] Foreign Key References [tblSample] (SampleID),
	[SampleNumber] [varchar](50) Not NULL,	-- This is the made up sample number stationID + date time from data entry is KEY to combining results 
																-- should be indexed as will be used a lot 
	[Event] [varchar](12) NOT NULL,				-- station number and sequence number as assigned at data entry like 44.090
	[WaterShed] [varchar](3) NULL,				-- NOT Keyed to any other table.
	[River_CD] [float] NULL,							-- NOT Keyed to any other table.
	[RiverName] [varchar](17) NULL,				-- NOT Keyed to any other table.
	[KitNumber] [int]  NULL,					-- NOT Keyed to any other table.
	[OrganizationName] [varchar](50)  NULL,	-- NOT Keyed to any other table.
	[StationNumber] [smallint]  NULL,			-- not tblStationID but number assigned to station - id could change as details change
	
	[TypeCode] [varchar](5)  NULL,				-- code (Dups and Blanks) like 00 for normal nonfiltered 
	[StationName] [varchar](50) NULL,			-- NOT Keyed to any other table.

	[MetalsBarCode] [varchar](20) NULL,	-- null unless there is an associated bar code
	[NutrientBarCode] [varchar](20) NULL,	-- null unless there is an associated bar code
	[FieldBarCode] [varchar](20) NULL,		-- None at this time, but.... null unless there is an associated bar code
	[BugsBarCode] [varchar](20) NULL,		-- None at this time, but.... null unless there is an associated bar code
	
	[SampleDate] [datetime] NOT NULL,
	-- [TIME] [varchar](6) NULL,					-- not needed as time is in SampleDate

	[USGS_Flow] [float] NULL,					-- Field Data
	[PH] [float] NULL,
	[TempC] [float] NULL,
	[PHEN_ALK] [float] NULL,
	[TOTAL_ALK] [float] NULL,
	[TOTAL_HARD] [float] NULL,
	[DO_MGL] [float] NULL,				
	[DOSAT] [smallint] NULL,			

	[AL_D] [float] NULL,				-- Metals Data
	[AL_T] [float] NULL,
	[AS_D] [float] NULL,
	[AS_T] [float] NULL,
	[CA_D] [float] NULL,
	[CA_T] [float] NULL,
	[CD_D] [float] NULL,
	[CD_T] [float] NULL,
	[CU_D] [float] NULL,
	[CU_T] [float] NULL,
	[FE_D] [float] NULL,
	[FE_T] [float] NULL,
	[MG_D] [float] NULL,
	[MG_T] [float] NULL,
	[MN_D] [float] NULL,
	[MN_T] [float] NULL,
	[PB_D] [float] NULL,
	[PB_T] [float] NULL,
	[SE_D] [float] NULL,
	[SE_T] [float] NULL,
	[ZN_D] [float] NULL,
	[ZN_T] [float] NULL,
	[NA_D] [float] NULL,
	[NA_T] [float] NULL,
	[K_D] [float] NULL,
	[K_T] [float] NULL,

	[Rep] [int] NULL,				-- Nutrient Data
	[Ammonia] [float] NULL,
	[Chloride] [float] NULL,
	[ChlorophyllA] [float] NULL,
	[DOC] [float] NULL,
	[NN] [float] NULL,
	[OP] [float] NULL,
	[Sulfate] [float] NULL,
	[totN] [float] NULL,
	[totP] [float] NULL,
	[TKN] [float] NULL,						-- new, from Barb  April, 2016
	[orgN] [float] NULL,						-- new, from Barb
	TSS [float] NULL,						-- new, from Bill S

--	[Csampid] [int] NULL,						-- Replaced with FK to tblSample 
-- chose nvarchar max because it has very low overhead on small strings (char count * 2) + 2
-- char count is *2 because each char is 16 bits, good for international characters,etc. 
-- just in case... just say'n 

	[FieldComment] [nvarchar](Max) NULL,
	[NutrientComment] [nvarchar](Max) NULL,
	[MetalsComment] [nvarchar](Max) NULL,
	[BugsComments] [nvarchar](Max) NULL,
	[BenthicsComments] [nvarchar](Max) NULL,

	[CreatedBy] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[BadBlank] [bit] null,
	[BadDuplicate] [bit] null,
	[BadSample] [bit] null, 
	[Valid] [bit] Not Null		-- flag to allow table updates without ever erasing data

 CONSTRAINT [PK_NEWexpWater] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90
	) 
	ON [PRIMARY]

	
)	-- end of table create  
ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


