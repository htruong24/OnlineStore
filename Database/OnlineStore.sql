USE [OnlineStore]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/10/2017 00:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [varchar](30) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [varchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[FullName] [nvarchar](max) NULL,
	[Gender] [int] NULL,
	[DateOfBirth] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Telephone] [varchar](20) NULL,
	[CellPhone] [varchar](20) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[EmailPassword] [nvarchar](50) NULL,
	[GroupId] [varchar](30) NULL,
	[TypeId] [varchar](30) NULL,
	[TaxCode] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 05/10/2017 00:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[Id] [varchar](30) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nchar](10) NULL,
	[OrderNumber] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 05/10/2017 00:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [varchar](30) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 05/10/2017 00:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Function](
	[Id] [varchar](30) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[OrderNumber] [int] NULL,
	[ModuleId] [varchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AutomaticValue]    Script Date: 05/10/2017 00:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AutomaticValue](
	[TableName] [nvarchar](50) NOT NULL,
	[Prefix] [nvarchar](50) NULL,
	[Length] [int] NULL,
	[LastValue] [nvarchar](50) NULL,
	[Character] [char](1) NULL,
 CONSTRAINT [PK_AutomaticValue] PRIMARY KEY CLUSTERED 
(
	[TableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Function_CreatedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Function_CreatedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Function_ModifiedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Function_ModifiedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Group_CreatedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Group_CreatedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Group_ModifiedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Group_ModifiedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Module_CreatedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Module_CreatedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Module_ModifiedDate]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Module_ModifiedBy]    Script Date: 05/10/2017 00:43:23 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
