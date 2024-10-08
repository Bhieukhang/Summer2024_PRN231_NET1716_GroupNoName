USE [master]
GO
/****** Object:  Database [JewelrySalesSystem]    Script Date: 6/7/2024 5:53:14 PM ******/
CREATE DATABASE [JewelrySalesSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JewelrySalesSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JewelrySalesSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JewelrySalesSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JewelrySalesSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JewelrySalesSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JewelrySalesSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JewelrySalesSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [JewelrySalesSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JewelrySalesSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JewelrySalesSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JewelrySalesSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JewelrySalesSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [JewelrySalesSystem] SET  MULTI_USER 
GO
ALTER DATABASE [JewelrySalesSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JewelrySalesSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JewelrySalesSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JewelrySalesSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JewelrySalesSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JewelrySalesSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [JewelrySalesSystem] SET QUERY_STORE = OFF
GO
USE [JewelrySalesSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Phone] [nchar](10) NOT NULL,
	[DOB] [date] NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[ImgURL] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[Deflag] [bit] NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[PricePressure] [float] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionWarranty]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionWarranty](
	[Id] [uniqueidentifier] NOT NULL,
	[Condition] [nvarchar](max) NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_ConditionWarranty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diamond]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diamond](
	[Id] [uniqueidentifier] NOT NULL,
	[DiamondName] [nvarchar](100) NULL,
	[Carat] [float] NULL,
	[Color] [nvarchar](50) NULL,
	[Clarity] [nvarchar](50) NULL,
	[Cut] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[JewelryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Diamond] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ManagerId] [uniqueidentifier] NOT NULL,
	[PercentDiscount] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ConditionDiscount] [float] NULL,
	[Status] [nvarchar](50) NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[MembershipId] [uniqueidentifier] NULL,
	[LevelDiscount] [int] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [uniqueidentifier] NOT NULL,
	[MaterialName] [nvarchar](100) NULL,
	[InsDate] [datetime] NULL,
	[ProductMaterialId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Level] [int] NULL,
	[Point] [int] NULL,
	[RedeemPoint] [int] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UsedMoney] [float] NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[PromotionId] [uniqueidentifier] NULL,
	[Type] [nvarchar](50) NULL,
	[InsDate] [datetime] NULL,
	[DiscountId] [uniqueidentifier] NULL,
	[TotalPrice] [float] NULL,
	[MaterialProcessPrice] [float] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [float] NULL,
	[Quantity] [int] NULL,
	[Discount] [float] NULL,
	[TotalPrice] [float] NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[ImportPrice] [float] NULL,
	[Size] [float] NULL,
	[TotalPrice] [float] NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[UpsDate] [datetime] NULL,
	[Quantity] [int] NULL,
	[ProcessPrice] [float] NULL,
	[ProductMaterialId] [uniqueidentifier] NULL,
	[Code] [nvarchar](10) NULL,
	[ImgProduct] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductConditionGroup]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductConditionGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
	[PromotionId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ProductConditionGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMaterial]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMaterial](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[MaterialId] [uniqueidentifier] NULL,
	[InsDate] [datetime] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ProductMaterial] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[Id] [uniqueidentifier] NOT NULL,
	[MembershipId] [uniqueidentifier] NOT NULL,
	[PromotionId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [uniqueidentifier] NOT NULL,
	[PromotionName] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ProductQuantity] [int] NULL,
	[Percentage] [int] NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stall]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stall](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Number] [int] NULL,
	[StaffId] [uniqueidentifier] NOT NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Stall] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Currency] [nvarchar](50) NULL,
	[TotalPrice] [float] NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warranty]    Script Date: 6/7/2024 5:53:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warranty](
	[Id] [uniqueidentifier] NOT NULL,
	[DateOfPurchase] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[Period] [int] NULL,
	[Deflag] [bit] NULL,
	[OrderDetailId] [uniqueidentifier] NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[ConditionWarrantyId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Warranty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4916c8d5-b606-40dc-a98b-057963b5149c', N'Nguyen Huu', N'0906697054', CAST(N'2000-07-11' AS Date), N'123', N'HCM', N'https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2020/8/21/829850/Bat-Cuoi-Truoc-Nhung-07.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'Tran Ngan', N'090667052 ', CAST(N'2000-04-11' AS Date), N'123', N'ahihi', N'https://mcdn.coolmate.me/uploads/November2021/meo-1_88.jpg', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-50c65a63e629', N'Staff', N'0333888257', CAST(N'1995-05-05' AS Date), N'Staff', N'Staff', N'https://inkythuatso.com/uploads/thumbnails/800/2022/06/anh-che-meo-dang-yeu-cho-may-tinh-6-02-14-24-47.jpg', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'b0e5f7f6-8c0a-4ac3-ad54-50c65a63e6f2', N'Admin User', N'0123456789', CAST(N'1990-01-01' AS Date), N'adminpassword', N'Admin Address', N'https://st.quantrimang.com/photos/image/2020/06/19/Hinh-Nen-Meo-Ngao-37.jpg', N'Active', 0, N'0f8fad5b-d9cb-469f-a165-70867728950e', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'e800ee81-281c-43e4-b35f-7cacc9610f82', N'Đỗ Hữu Thuận', N'0333888257', CAST(N'2000-05-03' AS Date), N'123', N'HCM', N'https://firebasestorage.googleapis.com/v0/b/webid-6c809.appspot.com/o/a.jpg?alt=media&token=ff242ea7-8a3a-4159-badb-1d1dbbd181a6', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'47056a6d-6cf7-4492-912f-7cf92f2b7ebc', N'Tran Binh', N'0906697056', CAST(N'2002-05-24' AS Date), N'123', N'HCM', N'https://blogchiasekienthuc.com/wp-content/uploads/2023/02/anh-meme-cho-meo-hai-huoc-15.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f6d88', N'Regular User', N'0987654321', CAST(N'1995-05-05' AS Date), N'userpassword', N'User Address', N'https://blog.maika.ai/wp-content/uploads/2024/02/anh-meo-meme-17.jpg', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', N'Staff', N'0333888257', CAST(N'1995-05-05' AS Date), N'Staff', N'Staff', N'https://e7.pngegg.com/pngimages/14/708/png-clipart-internet-meme-cat-humour-laughter-meme-mammal-pet-thumbnail.png', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'ab91f747-bbd9-43b0-ad6b-e9157048fe87', N'Nguyen Nghia', N'0906697055', CAST(N'2001-03-23' AS Date), N'123', N'HCM', N'https://media.ngoisao.vn/resize_580/news/2016/09/24/anh-hai-huoc-ve-loai-meo-2-ngoisao.vn.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', N'Tran Do', N'0906697053', CAST(N'2000-05-03' AS Date), N'123', N'HCM', N'https://cdn.tuoitre.vn/thumb_w/480/471584752817336320/2024/4/13/nguoi-nuoi-meo-de-bi-mac-benh-tam-than-phan-liet-1-1713018010154178075413.jpeg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-eb27de0be5a6', N'Đỗ Hữu Thuận', N'0333888257', CAST(N'2002-11-03' AS Date), N'123', N'HCM', N'https://firebasestorage.googleapis.com/v0/b/webid-6c809.appspot.com/o/a.jpg?alt=media&token=ff242ea7-8a3a-4159-badb-1d1dbbd181a6', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-ec27de0ae5a6', N'Thuận Họ Đỗ', N'0906697060', CAST(N'2002-08-23' AS Date), N'123', N'HCM', N'https://firebasestorage.googleapis.com/v0/b/webid-6c809.appspot.com/o/a.jpg?alt=media&token=ff242ea7-8a3a-4159-badb-1d1dbbd181a6', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'b2c3d4e5-6789-1011-1213-141516171819', N'Ring', N'Jewelry', N'Ring Category', 0.2)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'c3d4e5f6-7890-1011-1213-141516171819', N'Bracelet', N'Jewelry', N'Bracelet Category', 0.15)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'a1b2c3d4-5678-9101-1121-314151617181', N'Necklace', N'Jewelry', N'Necklace Category', 0.1)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Nhẫn', N'Nhẫn', NULL, 1)
GO
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'51142e3c-729c-49b4-b6cf-135d4df06ba5', N'Bảo hành bao gồm các vấn đề về chất lượng và lỗi sản xuất như:Lỗi về kim loại, hàn, khớp nối.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, NULL)
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', N'Giảm 15% dành cho trang sức thuộc cửa hàng trong trường hợp mua lại', NULL, NULL, NULL)
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'b1958280-788a-4bd8-95c3-eef953878098', N'Sản phẩm còn nguyên vẹn, không sức mẻ', NULL, NULL, NULL)
GO
INSERT [dbo].[Discount] ([Id], [OrderId], [ManagerId], [PercentDiscount], [Description], [ConditionDiscount], [Status], [InsDate], [UpsDate], [Note], [MembershipId], [LevelDiscount]) VALUES (N'f4c13373-7d09-4d0b-bd80-f8f2ca30bf02', N'a48ae106-d1a3-4972-bc9f-35f0369bc8e0', N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f6d88', 2, N'Giảm giá cho khách hàng thân thiết 15% tổng hóa đơn', 20000, N'Pending', CAST(N'2024-05-24T00:00:00.000' AS DateTime), CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'2520fbeb-3c2d-49de-be39-322a648095c1', N'Tran Ngan', 1, 0, 0, N'567f996e-7639-4bee-9c6e-2fb09cafbef9', 9090000, 1)
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'a168943f-b098-49d1-b34d-67a0e240bb6a', N'Tran Binh', 1, 0, 0, N'47056a6d-6cf7-4492-912f-7cf92f2b7ebc', 0, 0)
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'e1334b76-6996-494f-ae50-a61b027a0c93', N'Nguyen Huu', 1, 0, 0, N'4916c8d5-b606-40dc-a98b-057963b5149c', 0, 1)
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'63128083-59c9-4d3c-8f35-bdbd257bdd5d', N'Nguyen Nghia', 1, 0, 0, N'ab91f747-bbd9-43b0-ad6b-e9157048fe87', 0, 0)
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'a7fc7f01-9fe4-4df6-81e4-c63c28c519ee', N'Tran Do', 1, 0, 0, N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', 0, 1)
GO
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'1d7b2b2f-0505-4361-b852-148ff25446e9', N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', NULL, N'BUY', CAST(N'2024-06-05T18:57:30.680' AS DateTime), NULL, 0, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', NULL, N'BUY', CAST(N'2024-05-25T13:38:30.997' AS DateTime), NULL, 9000000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', NULL, N'BUY', CAST(N'2024-05-29T14:50:24.957' AS DateTime), NULL, 35000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'173c13df-908f-4399-951b-b1a53c0835c1', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', NULL, N'BUY', CAST(N'2024-05-25T13:25:42.233' AS DateTime), NULL, 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'd7416d34-9003-4bde-9b96-bf28c733f13c', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'e5b5e790-c100-4c7a-b980-16959f03ce4b', N'BUY', CAST(N'2024-05-25T12:06:16.557' AS DateTime), N'f4c13373-7d09-4d0b-bd80-f8f2ca30bf02', 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'444c92f3-4825-44b0-8d99-e04dfcc3565f', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', NULL, N'BUY', CAST(N'2024-05-25T12:26:01.810' AS DateTime), NULL, 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [PromotionId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'84d2e237-284f-4761-a2d1-ea506364848f', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', NULL, N'BUY', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL, 100000, NULL)
GO
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'407a9873-d979-4294-8270-01e95cadf169', 3000000, 2, 0, 6000000, N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:38:31.000' AS DateTime))
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'9cf3c2a5-5bf2-42c2-9022-09b769b844fa', 180000, 1, NULL, 180000, N'84d2e237-284f-4761-a2d1-ea506364848f', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'373451e3-096b-4f14-b2db-28ae900d75fe', 10000, 1, 0, 10000, N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-29T14:50:24.960' AS DateTime))
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'01517f8c-c1ac-4483-89cb-6efb48599072', 3000000, 1, 0, 3000000, N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:38:32.680' AS DateTime))
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'7c2da0f8-fbe5-4f81-8a0a-77b2f1ee6123', 25000, 1, 0, 25000, N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-29T14:50:26.023' AS DateTime))
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate]) VALUES (N'c54ef65e-10c5-4ceb-98a3-b55456bb5a0d', 3000000, 2, 0, 6000000, N'173c13df-908f-4399-951b-b1a53c0835c1', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:26:15.910' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [TotalPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [ProductMaterialId], [Code], [ImgProduct]) VALUES (N'8381a026-c8df-404a-b4aa-3f5b08ca5070', N'Vòng bạc', N'Vòng bạc', 3000000, 50, 38000000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, NULL, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 2, NULL, NULL, N'VB0001', NULL)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [TotalPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [ProductMaterialId], [Code], [ImgProduct]) VALUES (N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', N'Nhẫn bạc', N'Nhẫn bạc', 10000, 6, 180000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, NULL, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 10, NULL, NULL, N'NT0001', NULL)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [TotalPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [ProductMaterialId], [Code], [ImgProduct]) VALUES (N'6c0f7539-4768-4658-85d0-f5cff8ca53e9', N'Nhẫn Bạc đính đá PNJSilver XMXMW000135
', N'Nhẫn Bạc đính đá PNJSilver XMXMW000135
', 4000000, 11, 4950000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, NULL, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 10, NULL, NULL, N'NB0002', NULL)
GO
INSERT [dbo].[ProductConditionGroup] ([Id], [ProductId], [InsDate], [PromotionId], [Quantity]) VALUES (N'c84c0c37-7cea-4961-a551-898917377802', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-22T08:45:56.120' AS DateTime), N'e5b5e790-c100-4c7a-b980-16959f03ce4b', 2)
INSERT [dbo].[ProductConditionGroup] ([Id], [ProductId], [InsDate], [PromotionId], [Quantity]) VALUES (N'fe68a7ce-ed61-4750-bd40-d340e41b82c2', N'8381a026-c8df-404a-b4aa-3f5b08ca5070', CAST(N'2024-05-22T08:45:56.120' AS DateTime), N'ae583068-e2ee-4fdb-86b4-d669e0ca30cb', 1)
GO
INSERT [dbo].[Promotion] ([Id], [PromotionName], [Type], [Description], [ProductQuantity], [Percentage], [InsDate], [UpsDate], [StartDate], [EndDate]) VALUES (N'e5b5e790-c100-4c7a-b980-16959f03ce4b', N'Khuyến mãi cho khách hàng mới 15% cho sản phẩm đầu tiên', N'Product', N'Khuyến mãi cho khách hàng mới 15% cho sản phẩm đầu tiên', 2, 15, CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-30T08:45:56.120' AS DateTime), CAST(N'2024-06-15T08:45:56.120' AS DateTime))
INSERT [dbo].[Promotion] ([Id], [PromotionName], [Type], [Description], [ProductQuantity], [Percentage], [InsDate], [UpsDate], [StartDate], [EndDate]) VALUES (N'ae583068-e2ee-4fdb-86b4-d669e0ca30cb', N'Giảm 10% cho sản phẩm vòng bạc', N'Product', N'Giảm 10% cho sản phẩm vòng bạc', 1, 10, CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-30T08:45:56.120' AS DateTime), CAST(N'2024-06-15T08:45:56.120' AS DateTime))
GO
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'0f8fad5b-d9cb-469f-a165-70867728950e', N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae7', N'Manager')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae8', N'Staff')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae9', N'Customer')
GO
INSERT [dbo].[Stall] ([Id], [Name], [Number], [StaffId], [Deflag]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f7d22', N'Do Huu Thuan', 12, N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', 1)
GO
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [ConditionWarrantyId], [Status], [Note]) VALUES (N'f7b93f7f-cdaa-4fda-b3a8-5a8df174a233', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2025-05-22T08:45:56.120' AS DateTime), 12, 1, N'9cf3c2a5-5bf2-42c2-9022-09b769b844fa', N'0906697052', N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', N'Active', NULL)
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Diamond]  WITH CHECK ADD  CONSTRAINT [FK_Diamond_Product] FOREIGN KEY([JewelryId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Diamond] CHECK CONSTRAINT [FK_Diamond_Product]
GO
ALTER TABLE [dbo].[Discount]  WITH CHECK ADD  CONSTRAINT [FK_Discount_Membership] FOREIGN KEY([MembershipId])
REFERENCES [dbo].[Membership] ([Id])
GO
ALTER TABLE [dbo].[Discount] CHECK CONSTRAINT [FK_Discount_Membership]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Discount] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discount] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Discount]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Promotion]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[ProductConditionGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductConditionGroup_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductConditionGroup] CHECK CONSTRAINT [FK_ProductConditionGroup_Product]
GO
ALTER TABLE [dbo].[ProductConditionGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductConditionGroup_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[ProductConditionGroup] CHECK CONSTRAINT [FK_ProductConditionGroup_Promotion]
GO
ALTER TABLE [dbo].[ProductMaterial]  WITH CHECK ADD  CONSTRAINT [FK_ProductMaterial_Material] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
GO
ALTER TABLE [dbo].[ProductMaterial] CHECK CONSTRAINT [FK_ProductMaterial_Material]
GO
ALTER TABLE [dbo].[ProductMaterial]  WITH CHECK ADD  CONSTRAINT [FK_ProductMaterial_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductMaterial] CHECK CONSTRAINT [FK_ProductMaterial_Product]
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Membership] FOREIGN KEY([MembershipId])
REFERENCES [dbo].[Membership] ([Id])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Membership]
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Promotion]
GO
ALTER TABLE [dbo].[Stall]  WITH CHECK ADD  CONSTRAINT [FK_Stall_User] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Stall] CHECK CONSTRAINT [FK_Stall_User]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Order]
GO
ALTER TABLE [dbo].[Warranty]  WITH CHECK ADD  CONSTRAINT [FK_Warranty_ConditionWarranty] FOREIGN KEY([ConditionWarrantyId])
REFERENCES [dbo].[ConditionWarranty] ([Id])
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_ConditionWarranty]
GO
ALTER TABLE [dbo].[Warranty]  WITH CHECK ADD  CONSTRAINT [FK_Warranty_Order] FOREIGN KEY([OrderDetailId])
REFERENCES [dbo].[OrderDetail] ([Id])
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_Order]
GO
USE [master]
GO
ALTER DATABASE [JewelrySalesSystem] SET  READ_WRITE 
GO
