USE [InventoryTracker]
GO
ALTER TABLE [dbo].[PropertyValues] DROP CONSTRAINT [FK_PropertyValues_Properties]
GO
ALTER TABLE [dbo].[PropertyValues] DROP CONSTRAINT [FK_PropertyValues_Assets]
GO
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_Properties_DropDowns]
GO
ALTER TABLE [dbo].[DropDownValues] DROP CONSTRAINT [FK_DropDownValue_DropDowns]
GO
ALTER TABLE [dbo].[AssetTypeProperties] DROP CONSTRAINT [FK_AssetTypeProperties_Properties]
GO
ALTER TABLE [dbo].[AssetTypeProperties] DROP CONSTRAINT [FK_AssetTypeProperties_AssetTypes]
GO
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [DF_Properties_NonTracked]
GO
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [DF_Properties_Tracked]
GO
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [DF_Properties_Required]
GO
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [DF_Properties_Active]
GO
ALTER TABLE [dbo].[AssetTypes] DROP CONSTRAINT [DF_AssetTypes_Active]
GO
ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_DateLastModified]
GO
ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_DateCreated]
GO
/****** Object:  Table [dbo].[PropertyValues]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[PropertyValues]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[Properties]
GO
/****** Object:  Table [dbo].[DropDownValues]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[DropDownValues]
GO
/****** Object:  Table [dbo].[DropDowns]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[DropDowns]
GO
/****** Object:  Table [dbo].[AssetTypes]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[AssetTypes]
GO
/****** Object:  Table [dbo].[AssetTypeProperties]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[AssetTypeProperties]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 12/12/2017 7:22:49 PM ******/
DROP TABLE [dbo].[Assets]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[AssetID] [int] IDENTITY(1,1) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[AssetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetTypeProperties]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetTypeProperties](
	[AssetTypeID] [int] NOT NULL,
	[PropertyID] [int] NOT NULL,
 CONSTRAINT [PK_AssetTypeProperties] PRIMARY KEY CLUSTERED 
(
	[AssetTypeID] ASC,
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetTypes]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetTypes](
	[AssetTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Tracked] [tinyint] NOT NULL,
	[Active] [tinyint] NOT NULL,
 CONSTRAINT [PK_AssetTypes] PRIMARY KEY CLUSTERED 
(
	[AssetTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DropDowns]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DropDowns](
	[DropDownID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DropDowns] PRIMARY KEY CLUSTERED 
(
	[DropDownID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DropDownValues]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DropDownValues](
	[DropDownValueID] [int] IDENTITY(1,1) NOT NULL,
	[DropDownID] [int] NOT NULL,
	[Value] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_DropDownValue] PRIMARY KEY CLUSTERED 
(
	[DropDownValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[PropertyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[DropDownID] [int] NULL,
	[Active] [tinyint] NOT NULL,
	[Required] [tinyint] NOT NULL,
	[Tracked] [tinyint] NOT NULL,
	[NonTracked] [tinyint] NOT NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyValues]    Script Date: 12/12/2017 7:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyValues](
	[AssetID] [int] NOT NULL,
	[PropertyID] [int] NOT NULL,
	[Value] [nvarchar](300) NULL,
 CONSTRAINT [PK_PropertyValues] PRIMARY KEY CLUSTERED 
(
	[AssetID] ASC,
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Assets] ON 
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (117, 42, CAST(N'2017-12-12T19:13:58.990' AS DateTime), CAST(N'2017-12-12T19:13:58.990' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (118, 42, CAST(N'2017-12-12T19:14:00.810' AS DateTime), CAST(N'2017-12-12T19:14:00.810' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (119, 42, CAST(N'2017-12-12T19:14:02.677' AS DateTime), CAST(N'2017-12-12T19:14:02.677' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (120, 42, CAST(N'2017-12-12T19:14:04.567' AS DateTime), CAST(N'2017-12-12T19:14:04.567' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (121, 42, CAST(N'2017-12-12T19:14:06.400' AS DateTime), CAST(N'2017-12-12T19:14:06.400' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (122, 42, CAST(N'2017-12-12T19:14:08.327' AS DateTime), CAST(N'2017-12-12T19:14:08.327' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (123, 42, CAST(N'2017-12-12T19:14:10.230' AS DateTime), CAST(N'2017-12-12T19:14:10.230' AS DateTime))
GO
INSERT [dbo].[Assets] ([AssetID], [AssetTypeID], [DateAdded], [DateLastModified]) VALUES (124, 42, CAST(N'2017-12-12T19:14:12.190' AS DateTime), CAST(N'2017-12-12T19:14:12.190' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Assets] OFF
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 49)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 50)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 52)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 53)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 54)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 62)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 70)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 71)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 72)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 73)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 74)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 75)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 76)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 77)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 78)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 79)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 80)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 81)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 82)
GO
INSERT [dbo].[AssetTypeProperties] ([AssetTypeID], [PropertyID]) VALUES (42, 83)
GO
SET IDENTITY_INSERT [dbo].[AssetTypes] ON 
GO
INSERT [dbo].[AssetTypes] ([AssetTypeID], [Name], [Description], [Tracked], [Active]) VALUES (42, N'PACS Monitor', NULL, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[AssetTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DropDowns] ON 
GO
INSERT [dbo].[DropDowns] ([DropDownID], [Name]) VALUES (20, N'Status')
GO
SET IDENTITY_INSERT [dbo].[DropDowns] OFF
GO
SET IDENTITY_INSERT [dbo].[DropDownValues] ON 
GO
INSERT [dbo].[DropDownValues] ([DropDownValueID], [DropDownID], [Value]) VALUES (15, 20, N'Stock')
GO
INSERT [dbo].[DropDownValues] ([DropDownValueID], [DropDownID], [Value]) VALUES (16, 20, N'Deployed')
GO
SET IDENTITY_INSERT [dbo].[DropDownValues] OFF
GO
SET IDENTITY_INSERT [dbo].[Properties] ON 
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (46, N'High Tide', N'Number', NULL, NULL, 1, 1, 0, 1, 4)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (47, N'Low Tide', N'Number', NULL, NULL, 1, 1, 0, 1, 3)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (48, N'On Hand', N'Number', NULL, NULL, 1, 1, 0, 1, 2)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (49, N'Serial Number', N'String', NULL, NULL, 1, 1, 1, 0, 2)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (50, N'Building', N'String', NULL, NULL, 1, 1, 1, 0, 3)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (52, N'Floor', N'String', NULL, NULL, 1, 1, 1, 0, 4)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (53, N'Room', N'String', NULL, NULL, 1, 1, 1, 0, 5)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (54, N'Primary Owner', N'String', NULL, NULL, 1, 1, 1, 0, 6)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (62, N'Name', N'String', N'', NULL, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (66, N'Emails to Notify', N'String', NULL, NULL, 1, 0, 0, 1, 5)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (70, N'Category', N'String', N'', NULL, 1, 0, 0, 0, 6)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (71, N'Screen Resolution', N'String', N'', NULL, 1, 1, 0, 0, 7)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (72, N'Status', N'Drop Down', N'', 20, 1, 1, 0, 0, 8)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (73, N'Model', N'String', N'', NULL, 1, 0, 0, 0, 9)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (74, N'Manufacturer', N'String', N'', NULL, 1, 0, 0, 0, 10)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (75, N'Part', N'String', N'', NULL, 1, 0, 0, 0, 11)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (76, N'Notes', N'String', N'', NULL, 1, 0, 0, 0, 12)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (77, N'Department', N'String', N'', NULL, 1, 0, 0, 0, 13)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (78, N'Received By', N'String', N'', NULL, 1, 0, 0, 0, 14)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (79, N'Received Date', N'String', N'', NULL, 1, 0, 0, 0, 15)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (80, N'Last Inventoried By', N'String', N'', NULL, 1, 0, 0, 0, 16)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (81, N'Last Inventoried Date', N'String', N'', NULL, 1, 0, 0, 0, 17)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (82, N'Given To', N'String', N'', NULL, 1, 0, 0, 0, 18)
GO
INSERT [dbo].[Properties] ([PropertyID], [Name], [Type], [Unit], [DropDownID], [Active], [Required], [Tracked], [NonTracked], [Order]) VALUES (83, N'Given To Date', N'String', N'', NULL, 1, 0, 0, 0, 19)
GO
SET IDENTITY_INSERT [dbo].[Properties] OFF
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 49, N'1879057081')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 50, N'A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 52, N'Sb')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 53, N'ASb Cage')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 72, N'Stock')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 75, N'K9301201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 77, N'Radiology Informatics')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 80, N'Jim Nousek')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 81, N'12/07/2016 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (117, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 49, N'1879057114')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 50, N'Crile')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 52, N'Sb')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 53, N'ASb Cage')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 72, N'Stock')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 75, N'K9301201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 77, N'Radiology Informatics')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 80, N'Jim Nousek')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 81, N'12/07/2016 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (118, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 49, N'1879055548')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 50, N'crile')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 52, N'sub-basement')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 53, N'cage')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 72, N'Stock')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 75, N'K93011201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 77, N'Radiology Informatics')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 80, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 81, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (119, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 49, N'1879055556')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 50, N'J')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 52, N'B')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 53, N'Jb-280')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 72, N'Deployed')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 75, N'K93011201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 76, N'NucMed Reading Room')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 77, N'NUCMED')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 80, N'Jim Nousek')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 81, N'03/28/2017 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (120, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 49, N'1879057105')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 50, N'crile')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 52, N'sub-basement')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 53, N'cage')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 72, N'Stock')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 74, N'barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 75, N'K93011201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 77, N'Radiology Informatics')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 80, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 81, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (121, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 49, N'1879057086')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 50, N'crile')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 52, N'sub-basement')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 53, N'cage')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 62, N'Barco 2MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 70, N'Diagnostic Color')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 72, N'Stock')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 73, N'MDCC 2121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 74, N'barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 75, N'K93011201A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 77, N'Radiology Informatics')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 80, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 81, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (122, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 49, N'9382014093')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 50, N'Lutheran')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 52, N'1')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 53, N'Lutheran')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 62, N'Barco 2MP List')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 70, N'List')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 71, N'2MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 72, N'Deployed')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 73, N'MDRC 2120')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 75, N'K9301900A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 76, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 77, N'REGIONAL')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 78, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 79, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 80, N'Marva Bacon')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 81, N'12/20/2016 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (123, 83, N'11/30/-001 00:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 49, N'1890195874')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 50, N'LU')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 52, N'1')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 53, N'LUG-330A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 54, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 62, N'Barco 5MP Diagnostic')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 70, N'Diagnostic Grayscale')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 71, N'5MP')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 72, N'Deployed')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 73, N'MDMG-5121')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 74, N'Barco')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 75, N'K9300507A')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 76, N'9/25/15 - Deployed for use on RADAGLUBIDS001 /Decomissioned during Mammo move to Main Core')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 77, N'REGIONAL')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 78, N'Tom DiQuattro')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 79, N'07/18/2015 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 80, N'Tom DiQuattro')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 81, N'09/25/2015 0:00:00')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 82, N'')
GO
INSERT [dbo].[PropertyValues] ([AssetID], [PropertyID], [Value]) VALUES (124, 83, N'11/30/-001 00:00:00')
GO
ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_DateCreated]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_DateLastModified]  DEFAULT (getdate()) FOR [DateLastModified]
GO
ALTER TABLE [dbo].[AssetTypes] ADD  CONSTRAINT [DF_AssetTypes_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Properties] ADD  CONSTRAINT [DF_Properties_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Properties] ADD  CONSTRAINT [DF_Properties_Required]  DEFAULT ((0)) FOR [Required]
GO
ALTER TABLE [dbo].[Properties] ADD  CONSTRAINT [DF_Properties_Tracked]  DEFAULT ((0)) FOR [Tracked]
GO
ALTER TABLE [dbo].[Properties] ADD  CONSTRAINT [DF_Properties_NonTracked]  DEFAULT ((0)) FOR [NonTracked]
GO
ALTER TABLE [dbo].[AssetTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_AssetTypeProperties_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[AssetTypeProperties] CHECK CONSTRAINT [FK_AssetTypeProperties_AssetTypes]
GO
ALTER TABLE [dbo].[AssetTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_AssetTypeProperties_Properties] FOREIGN KEY([PropertyID])
REFERENCES [dbo].[Properties] ([PropertyID])
GO
ALTER TABLE [dbo].[AssetTypeProperties] CHECK CONSTRAINT [FK_AssetTypeProperties_Properties]
GO
ALTER TABLE [dbo].[DropDownValues]  WITH CHECK ADD  CONSTRAINT [FK_DropDownValue_DropDowns] FOREIGN KEY([DropDownID])
REFERENCES [dbo].[DropDowns] ([DropDownID])
GO
ALTER TABLE [dbo].[DropDownValues] CHECK CONSTRAINT [FK_DropDownValue_DropDowns]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_DropDowns] FOREIGN KEY([DropDownID])
REFERENCES [dbo].[DropDowns] ([DropDownID])
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_DropDowns]
GO
ALTER TABLE [dbo].[PropertyValues]  WITH CHECK ADD  CONSTRAINT [FK_PropertyValues_Assets] FOREIGN KEY([AssetID])
REFERENCES [dbo].[Assets] ([AssetID])
GO
ALTER TABLE [dbo].[PropertyValues] CHECK CONSTRAINT [FK_PropertyValues_Assets]
GO
ALTER TABLE [dbo].[PropertyValues]  WITH CHECK ADD  CONSTRAINT [FK_PropertyValues_Properties] FOREIGN KEY([PropertyID])
REFERENCES [dbo].[Properties] ([PropertyID])
GO
ALTER TABLE [dbo].[PropertyValues] CHECK CONSTRAINT [FK_PropertyValues_Properties]
GO
