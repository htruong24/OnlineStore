USE [master]
GO
/****** Object:  Database [OnlineStore]    Script Date: 05/10/2017 17:48:53 ******/
CREATE DATABASE [OnlineStore] ON  PRIMARY 
( NAME = N'OnlineStore', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\OnlineStore.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OnlineStore_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\OnlineStore_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineStore] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OnlineStore] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OnlineStore] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OnlineStore] SET ARITHABORT OFF
GO
ALTER DATABASE [OnlineStore] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [OnlineStore] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OnlineStore] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OnlineStore] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OnlineStore] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OnlineStore] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OnlineStore] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OnlineStore] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OnlineStore] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OnlineStore] SET  DISABLE_BROKER
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OnlineStore] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OnlineStore] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OnlineStore] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OnlineStore] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OnlineStore] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OnlineStore] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OnlineStore] SET  READ_WRITE
GO
ALTER DATABASE [OnlineStore] SET RECOVERY SIMPLE
GO
ALTER DATABASE [OnlineStore] SET  MULTI_USER
GO
ALTER DATABASE [OnlineStore] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OnlineStore] SET DB_CHAINING OFF
GO
USE [OnlineStore]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/10/2017 17:48:54 ******/
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
INSERT [dbo].[User] ([Id], [Username], [Password], [FirstName], [LastName], [FullName], [Gender], [DateOfBirth], [Address], [Telephone], [CellPhone], [Fax], [Email], [EmailPassword], [GroupId], [TypeId], [TaxCode], [Active], [Image]) VALUES (N'1', N'admin', N'202cb962ac59075b964b07152d234b70', N'Quan', N'Hieu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
/****** Object:  Table [dbo].[SubCategory]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[BrandId] [int] NULL,
	[CategoryId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] NOT NULL,
	[OrderId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Link] [nvarchar](50) NULL,
	[OrderNumber] [int] NULL,
	[Icon] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Module] ON
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Người dùng', N'Người dùng', N'Admin/User', 1, NULL, CAST(0x0000A76F0114F378 AS DateTime), N'dbo', CAST(0x0000A76F0114F378 AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Sản phẩm', N'Sản phẩm  ', N'AdminAdmin/Product', 2, NULL, CAST(0x0000A76F01150503 AS DateTime), N'dbo', CAST(0x0000A76F01150503 AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'Danh mục', N'Danh mục  ', N'Admin/Categories', 3, NULL, CAST(0x0000A76F01165CA7 AS DateTime), N'dbo', CAST(0x0000A76F01165CA7 AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'Danh mục con', N'Danh mục con', N'Admin/SubCategories', 4, NULL, CAST(0x0000A76F01179DDE AS DateTime), N'dbo', CAST(0x0000A76F01179DDE AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (7, N'Thương hiệu', N'Thương hiệu', N'Admin/Brand', 5, NULL, CAST(0x0000A76F01180CD6 AS DateTime), N'dbo', CAST(0x0000A76F01180CD6 AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (8, N'Đơn hàng', N'Đơn hàng', N'Admin/Order', 6, NULL, CAST(0x0000A76F01181E2C AS DateTime), N'dbo', CAST(0x0000A76F01181E2C AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (9, N'Chức năng', N'Chức năng', N'Admin/Modules', 7, NULL, CAST(0x0000A76F0118574E AS DateTime), N'dbo', CAST(0x0000A76F0118574E AS DateTime), N'dbo')
SET IDENTITY_INSERT [dbo].[Module] OFF
/****** Object:  Table [dbo].[Group]    Script Date: 05/10/2017 17:48:54 ******/
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
/****** Object:  Table [dbo].[Function]    Script Date: 05/10/2017 17:48:54 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'd', N'da', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
/****** Object:  Table [dbo].[Brand]    Script Date: 05/10/2017 17:48:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AutomaticValue]    Script Date: 05/10/2017 17:48:54 ******/
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
/****** Object:  Default [DF_SubCategory_CreatedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_SubCategory_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_SubCategory_ModifiedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_SubCategory_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Product_CreatedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Product_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Product_ModifiedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Product_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_OrderDetail_CreatedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_OrderDetail_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_OrderDetail_ModifiedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_OrderDetail_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Order_CreatedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Order_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Order_ModifiedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Order_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Module_CreatedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Module_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Module_ModifiedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Module_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Group_CreatedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Group_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Group_ModifiedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Group_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Function_CreatedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Function_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Function_ModifiedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Function_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Category_CreatedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Category_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Category_ModifiedDate]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Category_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Brand_CreatedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Brand_CreatedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Brand_ModifiedOn]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Brand_ModifiedBy]    Script Date: 05/10/2017 17:48:54 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
