USE [RiverWatch]
GO

/****** Object:  Table [dbo].[Samples]    Script Date: 7/6/2016 4:08:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Samples](
	[ID] [int] identity(1,1), 
	[SampleID] [int] NOT NULL,
	[StationID] [int] NOT NULL,
	[OrganizationID] [int] NULL,
	[SampleNumber] [varchar](30) NOT NULL,
	[NumberSample] [varchar](10) NULL,
	[DateCollected] [datetime] NOT NULL,
	[TimeCollected] [datetime] NULL,
	[DateReceived] [datetime] NULL,
	[DataSheetIncluded] [bit] NOT NULL,
	[MissingDataSheetReqDate] [datetime] NULL,
	[ChainOfCustody] [bit] NOT NULL,
	[MissingDataSheetReceived] [bit] NOT NULL,
	[NoMetals] [bit] NOT NULL,
	[PhysicalHabitat] [bit] NOT NULL,
	[Bug] [bit] NOT NULL,
	[NoNutrient] [bit] NOT NULL,
	[TotalSuspendedSolids] [bit] NOT NULL,
	[NitratePhosphorus] [bit] NOT NULL,
	[DuplicatedTSS] [bit] NOT NULL,
	[DuplicatedNP] [bit] NOT NULL,
	[Comment] [varchar](1000) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [varchar](25) NULL,
	[DateLastModified] [datetime] NULL,
	[UserLastModified] [varchar](25) NULL,
	[ChlorideSulfate] [bit] NULL,
	[BlankMetals] [bit] NULL,
	[DuplicatedMetals] [bit] NULL,
	[BugsQA] [bit] NULL,
	[DuplicatedCS] [bit] NULL,
	[Valid] [bit] NULL,
 CONSTRAINT [Samples_PK] PRIMARY KEY CLUSTERED 
(
	[SampleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


