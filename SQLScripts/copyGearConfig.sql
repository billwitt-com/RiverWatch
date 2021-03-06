USE [Riverwatch]
GO
/****** Object:  Table [dbo].[GearConfig]    Script Date: 5/10/2016 4:12:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GearConfig](
	[ID] [int] Not null identity , 
	[Code] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[FieldGearID] [varchar](10) NULL,
	[DateCreated] [datetime] not NULL,
	[CreatedBy] [varchar](50) not NULL,
	[Valid] [bit] not null default 1

 CONSTRAINT [GearConfig_PK] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (1, N'2 oz Nalgene', N'Water Sampler', N'WSWB', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (2, N'16 oz Plastic', N'Water Sampler', N'WSWB', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid])  VALUES (3, N'32 oz Plastic Jug', N'Water Sampler', N'WSWB', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid])  VALUES (4, N'8 oz Plastic', N'Water Sampler', N'WSWB', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid])  VALUES (5, N'Syringe', N'Water Sampler', N'WSOTH', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (6, N'Filter', N'Water Sampler', N'WSOTH', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid])  VALUES (7, N'18" x 8" Modified D-net', N'Net/Non-Tow', N'NNDF', CAST(0x0000964800000000 AS DateTime), N'system',1)
INSERT [dbo].[GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (8, N'600 Micron Sieve', N'Net/Non-Tow', N'NNOTH', CAST(0x0000964800000000 AS DateTime), N'system',1)
