USE [OnlineStore]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 05/20/2017 15:54:47 ******/
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
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Viet na', N'adad', CAST(0x0000A7740016A544 AS DateTime), N'1', CAST(0x0000A774001784E6 AS DateTime), N'1')
INSERT [dbo].[Brand] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Trung quoc', NULL, CAST(0x0000A7740016AFD4 AS DateTime), N'1', CAST(0x0000A7740016AFD4 AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[Brand] OFF
/****** Object:  Table [dbo].[AutomaticValue]    Script Date: 05/20/2017 15:54:47 ******/
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
INSERT [dbo].[AutomaticValue] ([TableName], [Prefix], [Length], [LastValue], [Character]) VALUES (N'USER', N'US', 6, N'US-001', N'-')
/****** Object:  Table [dbo].[User]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Gender], [DateOfBirth], [Address], [Telephone], [CellPhone], [Fax], [Email], [EmailPassword], [GroupId], [TypeId], [TaxCode], [Active], [Image], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (N'1', N'admin', N'202cb962ac59075b964b07152d234b70', N'Quân Hiệu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, CAST(0x0000A774000CC569 AS DateTime), N'1')
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Gender], [DateOfBirth], [Address], [Telephone], [CellPhone], [Fax], [Email], [EmailPassword], [GroupId], [TypeId], [TaxCode], [Active], [Image], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (N'US-001', N'Hiuea', N'202cb962ac59075b964b07152d234b70', N'Jame', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'202cb962ac59075b964b07152d234b70', NULL, NULL, NULL, 1, NULL, CAST(0x0000A774000BE1B7 AS DateTime), N'1', CAST(0x0000A774000BE1B7 AS DateTime), N'1')
/****** Object:  Table [dbo].[Unit]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Unit] ON
INSERT [dbo].[Unit] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Hello', N'123', CAST(0x0000A775016C5125 AS DateTime), N'1', CAST(0x0000A775016C5125 AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[Unit] OFF
/****** Object:  Table [dbo].[Supplier]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 05/20/2017 15:54:47 ******/
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
/****** Object:  Table [dbo].[Photo]    Script Date: 05/20/2017 15:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Url] [nvarchar](50) NULL,
	[ThumbnailUrl] [nvarchar](50) NULL,
	[Tite] [nvarchar](50) NULL,
	[AlbumId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 05/20/2017 15:54:47 ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 05/20/2017 15:54:47 ******/
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
/****** Object:  Table [dbo].[Module]    Script Date: 05/20/2017 15:54:47 ******/
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
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Người dùng', N'Người dùng 3', N'Admin/Users', 2, NULL, NULL, CAST(0x0000A76F01150404 AS DateTime), N'dbo', CAST(0x0000A77301065D88 AS DateTime), N'1')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Sản phẩm', N'Sản phẩm  ', N'AdminAdmin/Products', 2, NULL, NULL, CAST(0x0000A76F01150404 AS DateTime), N'dbo', CAST(0x0000A77701753839 AS DateTime), N'1')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'Danh mục', N'Danh mục  ', N'Admin/Categories', 3, NULL, NULL, CAST(0x0000A76F01165C8C AS DateTime), N'dbo', CAST(0x0000A774001695EF AS DateTime), N'1')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'Danh mục con', N'Danh mục con', N'Admin/SubCategories', 4, NULL, NULL, CAST(0x0000A76F01179DDE AS DateTime), N'dbo', CAST(0x0000A76F01179DDE AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (7, N'Thương hiệu', N'Thương hiệu', N'Admin/Brands', 5, NULL, NULL, CAST(0x0000A76F01180BCC AS DateTime), N'dbo', CAST(0x0000A77400168F67 AS DateTime), N'1')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (8, N'Đơn hàng', N'Đơn hàng', N'Admin/Order', 6, NULL, NULL, CAST(0x0000A76F01181E2C AS DateTime), N'dbo', CAST(0x0000A76F01181E2C AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (9, N'Chức năng', N'Chức năng', N'Admin/Modules', 7, NULL, NULL, CAST(0x0000A76F0118574E AS DateTime), N'dbo', CAST(0x0000A76F0118574E AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (39, N'Đơn vị tính', N'Đơn vị tính', N'Admin/Units', 8, NULL, NULL, CAST(0x0000A775016BB67D AS DateTime), N'dbo', CAST(0x0000A775016BB67D AS DateTime), N'dbo')
INSERT [dbo].[Module] ([Id], [Name], [Description], [Link], [OrderNumber], [Icon], [StatusId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (40, N'Hóa đơn', N'Hóa đơn', N'Admin/Orders', 1, NULL, NULL, CAST(0x0000A7770175535F AS DateTime), N'1', CAST(0x0000A7770175535F AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[Module] OFF
/****** Object:  Table [dbo].[Group]    Script Date: 05/20/2017 15:54:47 ******/
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
/****** Object:  Table [dbo].[Function]    Script Date: 05/20/2017 15:54:47 ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 05/20/2017 15:54:47 ******/
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
	[CreatedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 05/20/2017 15:54:47 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'Đồ điện tử', N'Đồ điện tử', NULL, CAST(0x0000A7780187A4F0 AS DateTime), N'US-001', CAST(0x0000A77900FE6A68 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (4, N'Thời trang', N'Thời trang', NULL, CAST(0x0000A7780187B015 AS DateTime), N'1', CAST(0x0000A7780187B015 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (5, N'Điện thoại và máy tính', N'Điện thoại và máy tính', NULL, CAST(0x0000A7780187BA72 AS DateTime), N'1', CAST(0x0000A7780187BA72 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (6, N'Mẹ và bé', N'Mẹ và bé', NULL, CAST(0x0000A7780187C2A6 AS DateTime), N'1', CAST(0x0000A7780187C2A6 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (7, N'Thực phẩm', N'Thực phẩm', NULL, CAST(0x0000A7780187CDE4 AS DateTime), N'1', CAST(0x0000A7780187CDE4 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (8, N'Đồ gia dụng', N'Đồ gia dụng', NULL, CAST(0x0000A7780187D67C AS DateTime), N'1', CAST(0x0000A7780187D67C AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (9, N'Nội ngoại thất', N'Nội ngoại thất', NULL, CAST(0x0000A7780187E316 AS DateTime), N'1', CAST(0x0000A7780187E316 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (10, N'Nội ngoại thất', N'Nội ngoại thất', NULL, CAST(0x0000A7780187E3B6 AS DateTime), N'1', CAST(0x0000A7780187E3B6 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (11, N'Sách và văn phòng phẩm', N'Sách và văn phòng phẩm', NULL, CAST(0x0000A7780187F386 AS DateTime), N'1', CAST(0x0000A7780187F386 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (12, N'Dịch vụ', N'Dịch vụ', NULL, CAST(0x0000A7780187FEB9 AS DateTime), N'1', CAST(0x0000A7780187FEB9 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (13, N'Thể thao du lịch', N'Thể thao du lịch', NULL, CAST(0x0000A7780188088B AS DateTime), N'1', CAST(0x0000A7780188088B AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (14, N'Thể thao du lịch', N'Thể thao du lịch', NULL, CAST(0x0000A7780188092A AS DateTime), N'1', CAST(0x0000A7780188092A AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (15, N'Ô tô xe máy', N'Ô tô xe máy', NULL, CAST(0x0000A778018811E9 AS DateTime), N'1', CAST(0x0000A778018811E9 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (16, N'Ô tô xe máy', N'Ô tô xe máy', NULL, CAST(0x0000A77801881288 AS DateTime), N'1', CAST(0x0000A77801881288 AS DateTime), N'1')
INSERT [dbo].[Category] ([Id], [Name], [Description], [OrderNumber], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (17, N'Sức khỏe và làm đẹp', N'Sức khỏe và làm đẹp', NULL, CAST(0x0000A77801882734 AS DateTime), N'1', CAST(0x0000A77801882734 AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[Category] OFF
/****** Object:  Table [dbo].[SubCategory]    Script Date: 05/20/2017 15:54:47 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategory] ON
INSERT [dbo].[SubCategory] ([Id], [Name], [Description], [OrderNumber], [CategoryId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (1, N'Đầu ghi hình', N'Đầu ghi hình', NULL, 16, CAST(0x0000A77900B17614 AS DateTime), N'1', CAST(0x0000A77900CDB4F6 AS DateTime), N'1')
INSERT [dbo].[SubCategory] ([Id], [Name], [Description], [OrderNumber], [CategoryId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (2, N'Quần áo nam', N'Quần áo nam', NULL, 4, CAST(0x0000A77900CE08E5 AS DateTime), N'1', CAST(0x0000A77900CE08E5 AS DateTime), N'1')
INSERT [dbo].[SubCategory] ([Id], [Name], [Description], [OrderNumber], [CategoryId], [CreatedOn], [CreatedById], [ModifiedOn], [ModifiedById]) VALUES (3, N'Quần áo nữ', N'Quần áo nữ', NULL, 4, CAST(0x0000A77900CE2D5A AS DateTime), N'1', CAST(0x0000A77900CE2D5A AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
/****** Object:  Default [DF_Brand_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Brand_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Brand_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Brand_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Category_CreatedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Category_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
/****** Object:  Default [DF_Category_ModifiedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Category_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
/****** Object:  Default [DF_Customer_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Customer_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Customer_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Customer_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Function_CreatedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Function_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Function_ModifiedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Function_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Group_CreatedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Group_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Group_ModifiedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Group_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Module_CreatedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Module_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Module_ModifiedDate]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Module_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Order_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Order_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Order_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Order_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_OrderDetail_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_OrderDetail_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_OrderDetail_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_OrderDetail_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Image_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Image_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Image_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Image_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Image_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Product_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Product_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Product_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Product_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Shipment_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Shipment_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Shipment_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Shipment_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Stock_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Stock_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Stock_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Stock_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_SubCategory_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_SubCategory_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_CreatedBy]  DEFAULT (user_name()) FOR [CreatedById]
GO
/****** Object:  Default [DF_SubCategory_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_SubCategory_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory] ADD  CONSTRAINT [DF_SubCategory_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedById]
GO
/****** Object:  Default [DF_Supplier_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Supplier_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Supplier_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Supplier_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_Unit_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_Unit_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Unit_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Unit_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  Default [DF_User_CreatedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_User_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedBy]  DEFAULT (user_name()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_User_ModifiedOn]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_User_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ModifiedBy]  DEFAULT (user_name()) FOR [ModifiedBy]
GO
/****** Object:  ForeignKey [FK_Category_Category]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([Id])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
/****** Object:  ForeignKey [FK_Category_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Category_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_ModifiedBy]
GO
/****** Object:  ForeignKey [FK_SubCategory_Category]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
/****** Object:  ForeignKey [FK_SubCategory_CreatedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_CreatedBy] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_CreatedBy]
GO
/****** Object:  ForeignKey [FK_SubCategory_ModifiedBy]    Script Date: 05/20/2017 15:54:47 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_ModifiedBy] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_ModifiedBy]
GO
