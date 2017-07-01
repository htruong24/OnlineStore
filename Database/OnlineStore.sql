USE [master]
GO
/****** Object:  Database [OnlineStore]    Script Date: 7/1/2017 3:35:11 PM ******/
CREATE DATABASE [OnlineStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineStore', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\OnlineStore.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OnlineStore_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\OnlineStore_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineStore] SET COMPATIBILITY_LEVEL = 110
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
ALTER DATABASE [OnlineStore] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineStore] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineStore] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineStore', N'ON'
GO
USE [OnlineStore]
GO
/****** Object:  Table [dbo].[AutomaticValue]    Script Date: 7/1/2017 3:35:11 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[OrderNumber] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Telephone] [varchar](20) NULL,
	[CellPhone] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[CreatedById] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[CreatedById] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](500) NULL,
	[OrderNumber] [int] NULL,
	[Active] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[StatusId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [varchar](30) NULL,
	[StatusId] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[ThumbnailUrl] [nvarchar](max) NULL,
	[FileSize] [int] NULL,
	[Extension] [varchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ShortDescrition] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 0) NULL,
	[Active] [bit] NULL,
	[Color] [nvarchar](50) NULL,
	[StatusId] [int] NULL,
	[SubCategoryId] [int] NULL,
	[BrandId] [int] NULL,
	[UnitId] [int] NULL,
	[SupplierId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductPhoto]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductPhoto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhotoId] [int] NULL,
	[ProductId] [int] NULL,
	[Featured] [bit] NULL,
	[OrderNumber] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_ProductPhoto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecommendProduct]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecommendProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[NumberOfClicks] [int] NULL,
 CONSTRAINT [PK_RecommendProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Telephone] [varchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[OrderId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[OrderNumber] [int] NULL,
	[CategoryId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Telephone] [varchar](20) NULL,
	[CellPhone] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 7/1/2017 3:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/1/2017 3:35:11 PM ******/
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
	[Name] [nvarchar](max) NULL,
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
	[CreatedOn] [datetime] NULL,
	[CreatedById] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedById] [varchar](30) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[AutomaticValue] ([TableName], [Prefix], [Length], [LastValue], [Character]) VALUES (N'USER', N'US', 6, N'US-001', N'-')
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

GO
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (1, N'NOKIA', N'NOKIA', CAST(0x0000A7740016A544 AS DateTime), N'1', CAST(0x0000A774001783EC AS DateTime), N'1')
GO
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (2, N'SAMSUNG', N'SAMSUNG', CAST(0x0000A7740016AFD0 AS DateTime), N'1', CAST(0x0000A7740016AFD0 AS DateTime), N'1')
GO
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'APPLE', N'APPLE', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (4, N'HTC', N'HTC', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (5, N'XIAOMI', N'XIAOMI', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'Đồ điện tử', N'Đồ điện tử', NULL, CAST(0x0000A7780187A4F0 AS DateTime), N'US-001', CAST(0x0000A77900FE6A68 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (4, N'Thời trang', N'Thời trang', NULL, CAST(0x0000A7780187B015 AS DateTime), N'1', CAST(0x0000A7780187B015 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (5, N'Điện thoại và máy tính', N'Điện thoại và máy tính', NULL, CAST(0x0000A7780187BA72 AS DateTime), N'1', CAST(0x0000A7780187BA72 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (6, N'Mẹ và bé', N'Mẹ và bé', NULL, CAST(0x0000A7780187C2A6 AS DateTime), N'1', CAST(0x0000A7780187C2A6 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (7, N'Thực phẩm', N'Thực phẩm', NULL, CAST(0x0000A7780187CDE4 AS DateTime), N'1', CAST(0x0000A7780187CDE4 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (8, N'Đồ gia dụng', N'Đồ gia dụng', NULL, CAST(0x0000A7780187D67C AS DateTime), N'1', CAST(0x0000A7780187D67C AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (9, N'Nội ngoại thất', N'Nội ngoại thất', NULL, CAST(0x0000A7780187E316 AS DateTime), N'1', CAST(0x0000A7780187E316 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (11, N'Sách và văn phòng phẩm', N'Sách và văn phòng phẩm', NULL, CAST(0x0000A7780187F386 AS DateTime), N'1', CAST(0x0000A7780187F386 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (12, N'Dịch vụ', N'Dịch vụ', NULL, CAST(0x0000A7780187FEB9 AS DateTime), N'1', CAST(0x0000A7780187FEB9 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (14, N'Thể thao du lịch', N'Thể thao du lịch', NULL, CAST(0x0000A7780188092A AS DateTime), N'1', CAST(0x0000A7780188092A AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (15, N'Ô tô xe máy', N'Ô tô xe máy', NULL, CAST(0x0000A778018811E9 AS DateTime), N'1', CAST(0x0000A778018811E9 AS DateTime), N'1')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (17, N'Sức khỏe và làm đẹp', N'Sức khỏe và làm đẹp', NULL, CAST(0x0000A77801882734 AS DateTime), N'1', CAST(0x0000A77801882734 AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

GO
INSERT [dbo].[Menu] ([Id], [Title], [Description], [Url], [OrderNumber], [Active], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (1, N'Chính sách công ty', N'Chính sách công ty', N'/Home/Policy', 1, 1, CAST(0x0000A792015E3E80 AS DateTime), N'1', CAST(0x0000A792015E3E80 AS DateTime), N'1')
GO
INSERT [dbo].[Menu] ([Id], [Title], [Description], [Url], [OrderNumber], [Active], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (2, N'Về chúng tôi', N'Về chúng tôi', N'/Home/AboutUs', 2, 1, CAST(0x0000A792015E7DC8 AS DateTime), N'1', CAST(0x0000A792015E9664 AS DateTime), N'1')
GO
INSERT [dbo].[Menu] ([Id], [Title], [Description], [Url], [OrderNumber], [Active], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'Liên hệ', N'Liên hệ', N'/Home/Contact', 3, 1, CAST(0x0000A79201600530 AS DateTime), N'1', CAST(0x0000A79201600530 AS DateTime), N'1')
GO
INSERT [dbo].[Menu] ([Id], [Title], [Description], [Url], [OrderNumber], [Active], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (5, N'Giỏ hàng', N'Giỏ hàng', N'/ShoppingCart', 3, 1, CAST(0x0000A79A0154A91D AS DateTime), N'1', CAST(0x0000A79A0154A91D AS DateTime), N'1')
GO
INSERT [dbo].[Menu] ([Id], [Title], [Description], [Url], [OrderNumber], [Active], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (6, N'Checkout', N'Checkout', N'/ShoppingCart/Checkout', 4, 1, CAST(0x0000A79A0154BE55 AS DateTime), N'1', CAST(0x0000A79A0154BE55 AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Module] ON 

GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (1, N'Người dùng', N'Người dùng 3', N'Admin/Users', 2, NULL, NULL, CAST(0x0000A76F01150404 AS DateTime), N'dbo', CAST(0x0000A77301065D88 AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (2, N'Sản phẩm', N'Sản phẩm  ', N'Admin/Products', 2, NULL, NULL, CAST(0x0000A76F01150404 AS DateTime), N'dbo', CAST(0x0000A77701753824 AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'Danh mục', N'Danh mục  ', N'Admin/Categories', 3, NULL, NULL, CAST(0x0000A76F01165C8C AS DateTime), N'dbo', CAST(0x0000A774001695EF AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (6, N'Danh mục con', N'Danh mục con', N'Admin/SubCategories', 4, NULL, NULL, CAST(0x0000A76F01179DDE AS DateTime), N'dbo', CAST(0x0000A76F01179DDE AS DateTime), N'dbo')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (7, N'Thương hiệu', N'Thương hiệu', N'Admin/Brands', 5, NULL, NULL, CAST(0x0000A76F01180BCC AS DateTime), N'dbo', CAST(0x0000A77400168F67 AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (8, N'Đơn hàng', N'Đơn hàng', N'Admin/Order', 6, NULL, NULL, CAST(0x0000A76F01181E2C AS DateTime), N'dbo', CAST(0x0000A76F01181E2C AS DateTime), N'dbo')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (9, N'Chức năng', N'Chức năng', N'Admin/Modules', 7, NULL, NULL, CAST(0x0000A76F0118574E AS DateTime), N'dbo', CAST(0x0000A76F0118574E AS DateTime), N'dbo')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (39, N'Đơn vị tính', N'Đơn vị tính', N'Admin/Units', 8, NULL, NULL, CAST(0x0000A775016BB67D AS DateTime), N'dbo', CAST(0x0000A775016BB67D AS DateTime), N'dbo')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (40, N'Hóa đơn', N'Hóa đơn', N'Admin/Orders', 1, NULL, NULL, CAST(0x0000A7770175535F AS DateTime), N'1', CAST(0x0000A7770175535F AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (41, N'Thư viện ảnh', N'Thư viện ảnh', N'Admin/Photos', 1, NULL, NULL, CAST(0x0000A78301785C0C AS DateTime), N'1', CAST(0x0000A783017871E3 AS DateTime), N'1')
GO
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (42, N'Trình đơn', N'Trình đơn', N'Admin/Menus', 1, NULL, NULL, CAST(0x0000A792015DD67B AS DateTime), N'1', CAST(0x0000A792015DD67B AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Module] OFF
GO
SET IDENTITY_INSERT [dbo].[Photo] ON 

GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (109, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (110, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (111, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (112, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (113, N'non', N'', N'Content/upload/photos/products/non.jpg', N'Content/upload/photos/products/non.jpg', 42156, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (114, N'Xiaomi', N'', N'Content/upload/photos/products/Xiaomi(1).jpg', N'Content/upload/photos/products/Xiaomi(1).jpg', 13102, N'.jpg', CAST(0x0000A79A01717F79 AS DateTime), N'1', CAST(0x0000A79A01717F79 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (115, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (116, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (117, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (118, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (119, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (120, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (121, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (122, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (123, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (124, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (125, N'non', N'', N'Content/upload/photos/products/non.jpg', N'Content/upload/photos/products/non.jpg', 42156, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (126, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (127, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (128, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (129, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (130, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (131, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (132, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (133, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (134, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (135, N'Cap', N'', N'Content/upload/photos/products/Cap.jpg', N'Content/upload/photos/products/Cap.jpg', 73358, N'.jpg', CAST(0x0000A79A01717F70 AS DateTime), N'1', CAST(0x0000A79A01717F70 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (136, N'giay', N'', N'Content/upload/photos/products/giay.jpg', N'Content/upload/photos/products/giay.jpg', 30756, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (137, N'Xiaomi', N'Điện thoại Xiaomi', N'Content/upload/photos/products/Xiaomi.jpg', N'Content/upload/photos/products/Xiaomi.jpg', 13102, N'.jpg', CAST(0x0000A79A0170801C AS DateTime), N'1', CAST(0x0000A79A0170801C AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (138, N'apple', N'', N'Content/upload/photos/products/apple.jpg', N'Content/upload/photos/products/apple.jpg', 17762, N'.jpg', CAST(0x0000A79A01717F6F AS DateTime), N'1', CAST(0x0000A79A01717F6F AS DateTime), N'1')
GO
INSERT [dbo].[Photo] ([Id], [Title], [Description], [Url], [ThumbnailUrl], [FileSize], [Extension], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (139, N'non', N'', N'Content/upload/photos/products/non.jpg', N'Content/upload/photos/products/non.jpg', 42156, N'.jpg', CAST(0x0000A79A01717F71 AS DateTime), N'1', CAST(0x0000A79A01717F71 AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Photo] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (81, N'Quan', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A79F016C305B AS DateTime), N'1', CAST(0x0000A79F016C305B AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (82, N'Áo', N'Ahihi', NULL, NULL, 1, NULL, NULL, 5, 4, 1, NULL, CAST(0x0000A7A00161406C AS DateTime), N'1', CAST(0x0000A7A00162F9D1 AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (83, N'Nón', NULL, NULL, NULL, 1, NULL, NULL, 4, 4, 1, NULL, CAST(0x0000A7A001617163 AS DateTime), N'1', CAST(0x0000A7A001617163 AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (84, N'UHa', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A001618EF0 AS DateTime), N'1', CAST(0x0000A7A001633885 AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (85, N'Kenh', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A00161B6C8 AS DateTime), N'1', CAST(0x0000A7A0016342FC AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (86, N'Jean', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A00161D66C AS DateTime), N'1', CAST(0x0000A7A001635934 AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (87, N'Demo', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A0016216E0 AS DateTime), N'1', CAST(0x0000A7A00163643C AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (88, N'Test', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A001624F20 AS DateTime), N'1', CAST(0x0000A7A0016371A1 AS DateTime), N'1')
GO
INSERT [dbo].[Product] ([Id], [Name], [ShortDescrition], [Description], [Price], [Active], [Color], [StatusId], [SubCategoryId], [BrandId], [UnitId], [SupplierId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (89, N'Yahoo', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7A00162E520 AS DateTime), N'1', CAST(0x0000A7A001637DD2 AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductPhoto] ON 

GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (40, 116, 81, 1, NULL, CAST(0x0000A79F016C306D AS DateTime), N'1', CAST(0x0000A79F016C306D AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (41, 117, 81, 0, NULL, CAST(0x0000A79F016C306D AS DateTime), N'1', CAST(0x0000A79F016C306D AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (42, 118, 82, 1, NULL, CAST(0x0000A7A0016141CA AS DateTime), N'1', CAST(0x0000A7A00162F9ED AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (43, 119, 82, 0, NULL, CAST(0x0000A7A0016141CA AS DateTime), N'1', CAST(0x0000A7A00162F9ED AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (44, 121, 83, 1, NULL, CAST(0x0000A7A001617166 AS DateTime), N'1', CAST(0x0000A7A001617166 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (45, 120, 83, 0, NULL, CAST(0x0000A7A001617166 AS DateTime), N'1', CAST(0x0000A7A001617166 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (46, 122, 83, 0, NULL, CAST(0x0000A7A001617166 AS DateTime), N'1', CAST(0x0000A7A001617166 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (47, 125, 84, 1, NULL, CAST(0x0000A7A001618FB5 AS DateTime), N'1', CAST(0x0000A7A001633891 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (48, 124, 84, 0, NULL, CAST(0x0000A7A001618FB5 AS DateTime), N'1', CAST(0x0000A7A001633891 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (49, 123, 84, 0, NULL, CAST(0x0000A7A001618FB5 AS DateTime), N'1', CAST(0x0000A7A001633891 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (50, 126, 85, 1, NULL, CAST(0x0000A7A00161B6D7 AS DateTime), N'1', CAST(0x0000A7A001634302 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (51, 127, 85, 0, NULL, CAST(0x0000A7A00161B6D7 AS DateTime), N'1', CAST(0x0000A7A001634302 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (52, 128, 85, 0, NULL, CAST(0x0000A7A00161B6D7 AS DateTime), N'1', CAST(0x0000A7A001634302 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (53, 131, 86, 1, NULL, CAST(0x0000A7A00161D76A AS DateTime), N'1', CAST(0x0000A7A001635935 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (54, 130, 86, 0, NULL, CAST(0x0000A7A00161D76A AS DateTime), N'1', CAST(0x0000A7A001635935 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (55, 129, 86, 0, NULL, CAST(0x0000A7A00161D76A AS DateTime), N'1', CAST(0x0000A7A001635935 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (56, 133, 87, 1, NULL, CAST(0x0000A7A00162174A AS DateTime), N'1', CAST(0x0000A7A00163643E AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (57, 134, 87, 0, NULL, CAST(0x0000A7A00162174A AS DateTime), N'1', CAST(0x0000A7A00163643E AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (58, 132, 87, 0, NULL, CAST(0x0000A7A00162174A AS DateTime), N'1', CAST(0x0000A7A00163643E AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (59, 135, 88, 1, NULL, CAST(0x0000A7A001625026 AS DateTime), N'1', CAST(0x0000A7A0016371A3 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (60, 136, 88, 0, NULL, CAST(0x0000A7A001625026 AS DateTime), N'1', CAST(0x0000A7A0016371A3 AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (61, 139, 89, 1, NULL, CAST(0x0000A7A00162E5D5 AS DateTime), N'1', CAST(0x0000A7A001637DDE AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (62, 137, 89, 0, NULL, CAST(0x0000A7A00162E5D5 AS DateTime), N'1', CAST(0x0000A7A001637DDE AS DateTime), N'1')
GO
INSERT [dbo].[ProductPhoto] ([Id], [PhotoId], [ProductId], [Featured], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (63, 138, 89, 0, NULL, CAST(0x0000A7A00162E5D5 AS DateTime), N'1', CAST(0x0000A7A001637DDE AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[ProductPhoto] OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategory] ON 

GO
INSERT [dbo].[SubCategory] ([Id], [Name], [Description], [OrderNumber], [CategoryId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (4, N'Điện thoại di động', N'Điện thoại di động', 1, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubCategory] ([Id], [Name], [Description], [OrderNumber], [CategoryId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (5, N'Điện thoại bàn', N'Điện thoại bàn', 2, 5, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Unit] ON 

GO
INSERT [dbo].[Unit] ([Id], [Name], [Description], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (1, N'Cái', N'Cái', CAST(0x0000A775016C50D8 AS DateTime), N'1', CAST(0x0000A775016C50D8 AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Unit] OFF
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Gender], [DateOfBirth], [Address], [Telephone], [CellPhone], [Fax], [Email], [EmailPassword], [GroupId], [TypeId], [TaxCode], [Active], [Image], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (N'1', N'admin', N'202cb962ac59075b964b07152d234b70', N'Quân Hiệu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, CAST(0x0000A774000CC569 AS DateTime), N'1')
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Gender], [DateOfBirth], [Address], [Telephone], [CellPhone], [Fax], [Email], [EmailPassword], [GroupId], [TypeId], [TaxCode], [Active], [Image], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (N'US-001', N'Hiuea', N'202cb962ac59075b964b07152d234b70', N'Jame', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'202cb962ac59075b964b07152d234b70', NULL, NULL, NULL, 1, NULL, CAST(0x0000A774000BE1B7 AS DateTime), N'1', CAST(0x0000A774000BE1B7 AS DateTime), N'1')
GO
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_CreatedById]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_ModifiedById]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[ProductPhoto] ADD  CONSTRAINT [DF_ProductPhoto_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[ProductPhoto] ADD  CONSTRAINT [DF_ProductPhoto_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Brand]  WITH CHECK ADD  CONSTRAINT [FK_Brand_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Brand] CHECK CONSTRAINT [FK_Brand_CreatedBy]
GO
ALTER TABLE [dbo].[Brand]  WITH CHECK ADD  CONSTRAINT [FK_Brand_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Brand] CHECK CONSTRAINT [FK_Brand_ModifiedBy]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_CreatedBy]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_ModifiedBy]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CreatedBy]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_ModifiedBy]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_CreatedBy]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_ModifiedBy]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_CreatedBy]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_ModifiedBy]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_Shipment_CreatedBy]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_Shipment_ModifiedBy]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_CreatedBy]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_ModifiedBy]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_CreatedBy]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_ModifiedBy]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_CreatedBy]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_ModifiedBy]
GO
ALTER TABLE [dbo].[Unit]  WITH CHECK ADD  CONSTRAINT [FK_Unit_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Unit] CHECK CONSTRAINT [FK_Unit_CreatedBy]
GO
ALTER TABLE [dbo].[Unit]  WITH CHECK ADD  CONSTRAINT [FK_Unit_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Unit] CHECK CONSTRAINT [FK_Unit_ModifiedBy]
GO
USE [master]
GO
ALTER DATABASE [OnlineStore] SET  READ_WRITE 
GO
