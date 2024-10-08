USE [master]
GO
/****** Object:  Database [JewelrySalesSystem]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionWarranty]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Diamond]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diamond](
	[Id] [uniqueidentifier] NOT NULL,
	[DiamondName] [nvarchar](max) NULL,
	[Code] [nvarchar](50) NULL,
	[Carat] [float] NULL,
	[Color] [nvarchar](50) NULL,
	[Clarity] [nvarchar](50) NULL,
	[Cut] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[ImageDiamond] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
	[PeriodWarranty] [int] NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Diamond] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[GoldRate]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoldRate](
	[Id] [uniqueidentifier] NOT NULL,
	[Rate] [float] NULL,
	[UpsDate] [datetime] NULL,
 CONSTRAINT [PK_GoldRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [uniqueidentifier] NOT NULL,
	[MaterialName] [nvarchar](100) NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Point] [int] NULL,
	[RedeemPoint] [int] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UsedMoney] [float] NULL,
	[Deflag] [bit] NULL,
	[MemberTypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberType]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberType](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](100) NULL,
	[Levels] [int] NULL,
	[NextLevel] [float] NULL,
	[InsDate] [datetime] NULL,
	[PercentDiscount] [float] NULL,
 CONSTRAINT [PK_MemberType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/14/2024 11:38:12 AM ******/
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
	[PromotionId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessPrice]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessPrice](
	[ProcesspriceId] [int] NOT NULL,
	[ProcessPrice] [float] NULL,
	[Size] [float] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[UpsDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_ProcessPrice] PRIMARY KEY CLUSTERED 
(
	[ProcesspriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/14/2024 11:38:12 AM ******/
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
	[SellingPrice] [float] NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[UpsDate] [datetime] NULL,
	[Quantity] [int] NULL,
	[ProcessPrice] [float] NULL,
	[MaterialId] [uniqueidentifier] NULL,
	[Code] [nvarchar](10) NULL,
	[ImgProduct] [nvarchar](max) NULL,
	[Tax] [float] NULL,
	[SubId] [uniqueidentifier] NULL,
	[PeriodWarranty] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductConditionGroup]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Program]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Promotion]    Script Date: 7/14/2024 11:38:12 AM ******/
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
	[ImgURL] [nvarchar](max) NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchasePrice]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasePrice](
	[PurchasePriceId] [int] NOT NULL,
	[PurchasePrice] [nchar](10) NULL,
	[Size] [float] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[UpsDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_PurchasePrice] PRIMARY KEY CLUSTERED 
(
	[PurchasePriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Stall]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[SubProducts]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubProducts](
	[Id] [uniqueidentifier] NOT NULL,
	[TitleProductName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_SubProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 7/14/2024 11:38:12 AM ******/
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
/****** Object:  Table [dbo].[Warranty]    Script Date: 7/14/2024 11:38:12 AM ******/
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
	[Status] [nvarchar](50) NULL,
	[Note] [nvarchar](max) NULL,
	[CodeWarranty] [nchar](5) NULL,
 CONSTRAINT [PK_Warranty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarrantyMappingCondition]    Script Date: 7/14/2024 11:38:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarrantyMappingCondition](
	[Id] [uniqueidentifier] NOT NULL,
	[WarrantyId] [uniqueidentifier] NULL,
	[ConditionWarrantyId] [uniqueidentifier] NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_WarrantyMappingCondition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4916c8d5-b606-40dc-a98b-057963b5149c', N'Nguyen Huu', N'0906697054', CAST(N'2000-07-11' AS Date), N'123', N'HCM', N'https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2020/8/21/829850/Bat-Cuoi-Truoc-Nhung-07.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'1ae65ea3-2abd-463a-9f93-0df14e4658c7', N'Ab', N'024578963 ', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-06-24T08:56:45.680' AS DateTime), CAST(N'2024-06-24T08:56:45.680' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'43963465-03be-4b11-bed7-234bf8ba746a', N'Ngan', N'0906697051', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-06-24T10:47:35.293' AS DateTime), CAST(N'2024-06-24T10:47:35.293' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'Tran Ngan', N'0906697052', CAST(N'2000-04-11' AS Date), N'123', N'Man Thiện, Thủ Đức', N'https://mcdn.coolmate.me/uploads/November2021/meo-1_88.jpg', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'9e848019-0bd6-4b6a-8979-3935b604ac76', N'Đồ', N'0748596321', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-07-02T16:19:56.477' AS DateTime), CAST(N'2024-07-02T16:19:56.477' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-50c65a63e629', N'Staff', N'0901234567', CAST(N'1995-05-05' AS Date), N'123', N'HCM', N'https://inkythuatso.com/uploads/thumbnails/800/2022/06/anh-che-meo-dang-yeu-cho-may-tinh-6-02-14-24-47.jpg', N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'b0e5f7f6-8c0a-4ac3-ad54-50c65a63e6f2', N'Admin ', N'0123456789', CAST(N'1990-01-01' AS Date), N'adminpassword', N'Nhà Văn hóa sinh viên Thủ Đức', N'https://st.quantrimang.com/photos/image/2020/06/19/Hinh-Nen-Meo-Ngao-37.jpg', N'Active', 0, N'0f8fad5b-d9cb-469f-a165-70867728950e', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'95bd2d25-53e4-4c20-b460-60b0c75f26fc', N'Bùi Hiểu Khang', N'0913769909', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-06-20T07:32:40.810' AS DateTime), CAST(N'2024-06-20T07:32:40.810' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'26e37551-529e-43d6-8f5b-61ea03a1423d', N'Thảo Uyên', N'0147852369', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-06-24T08:22:50.980' AS DateTime), CAST(N'2024-06-24T08:22:50.980' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'e800ee81-281c-43e4-b35f-7cacc9610f82', N'Đỗ Hữu Thuận', N'0333888257', CAST(N'2000-05-03' AS Date), N'1234', N'HCM', N'https://firebasestorage.googleapis.com/v0/b/webid-6c809.appspot.com/o/a.jpg?alt=media&token=ff242ea7-8a3a-4159-badb-1d1dbbd181a6', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'47056a6d-6cf7-4492-912f-7cf92f2b7ebc', N'Tran Binh', N'0906697056', CAST(N'2002-05-24' AS Date), N'123', N'HCM', N'https://blogchiasekienthuc.com/wp-content/uploads/2023/02/anh-meme-cho-meo-hai-huoc-15.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'6ca95547-6677-4d93-8786-924c761baeaa', N'LaMo', N'0906082024', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-06-08T11:25:59.137' AS DateTime), CAST(N'2024-06-08T11:25:59.137' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'568988eb-cfc6-411f-bd1e-b19010eb7921', N'aaa', N'0748596327', NULL, N'000', NULL, NULL, N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-07-04T16:19:58.843' AS DateTime), CAST(N'2024-07-04T16:19:58.843' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f6d88', N'Manager', N'0987654321', CAST(N'1995-05-05' AS Date), N'managerpassword', N'User Addressss', N'https://blog.maika.ai/wp-content/uploads/2024/02/anh-meo-meme-17.jpg', N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-06-06T01:45:19.827' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', N'Staff', N'0333888257', CAST(N'1995-05-05' AS Date), N'Staff', N'Staff', N'https://e7.pngegg.com/pngimages/14/708/png-clipart-internet-meme-cat-humour-laughter-meme-mammal-pet-thumbnail.png', N'UnActive', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'ab91f747-bbd9-43b0-ad6b-e9157048fe87', N'Nguyen Nghia', N'0906697055', CAST(N'2001-03-23' AS Date), N'123', N'HCM', N'https://media.ngoisao.vn/resize_580/news/2016/09/24/anh-hai-huoc-ve-loai-meo-2-ngoisao.vn.jpg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', N'Tran Do', N'0906697053', CAST(N'2000-05-03' AS Date), N'123', N'HCM', N'https://cdn.tuoitre.vn/thumb_w/480/471584752817336320/2024/4/13/nguoi-nuoi-meo-de-bi-mac-benh-tam-than-phan-liet-1-1713018010154178075413.jpeg', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-eb27de0be5a6', N'Đỗ Hữu Thuận', N'0333888257', CAST(N'2002-11-03' AS Date), N'123', N'HCM', N'https://firebasestorage.googleapis.com/v0/b/webid-6c809.appspot.com/o/a.jpg?alt=media&token=ff242ea7-8a3a-4159-badb-1d1dbbd181a6', N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae8', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (N'b2c3d4e5-6789-1011-1213-141516171819', N'Nhẫn')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (N'c3d4e5f6-7890-1011-1213-141516171819', N'Vòng Tay')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (N'a1b2c3d4-5678-9101-1121-314151617181', N'Dây Chuyền')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Bông Tai')
GO
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'51142e3c-729c-49b4-b6cf-135d4df06ba5', N'Bảo hành bao gồm các vấn đề về chất lượng và lỗi sản xuất như:Lỗi về kim loại, hàn, khớp nối.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', N'Phạm vi bảo hàng: Bao gồm các lỗi như lỏng gắn đá, thiếu đá do lỗi gắn kết, và các móc hoặc khoá bị hỏng do lỗi sản xuất.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'9e0db4bd-cfe1-40ed-82f0-7a9e77dc1603', N'Phạm vi bảo hành:Bảo hành bao gồm các lỗi do nhà sản xuất về vật liệu và tay nghề.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'f3ee84d4-886e-4830-a323-8b16b8fc13ed', N'Loại trừ bảo hành:Ố màu hoặc đổi màu do tiếp xúc với hóa chất, nước hoa, hoặc kem dưỡng da.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'83877137-10b8-44d4-abb5-9594f22e52ce', N'Loại trừ bảo hành: Hư hỏng do sử dụng sai cách, tai nạn, hoặc các thay đổi do người không được ủy quyền thực hiện', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'2ffcee25-d9a8-4236-96c8-a27ae08f7ec8', N'Yêu cầu hoá đơn gốc hoặc chứng từ mua hàng để xác nhận yêu cầu bảo hành.
', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag], [Type]) VALUES (N'b1958280-788a-4bd8-95c3-eef953878098', N'Loại trừ bảo hành: Hư hỏng do sử dụng sai cách, tai nạn, hoặc các thay đổi do người không được ủy quyền thực hiện.', CAST(N'2024-05-25T14:40:13.290' AS DateTime), 1, N'Product')
GO
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'0f257898-5289-493a-9601-2e43fc715109', N'Oval Diamond', N'KCNN0003', 1.3, N'F', N'VVS2', N'Very Good', 280000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F8e6bb73c-f6f9-45d7-9708-faa4fe86759f?alt=media&token=11cd658e-2967-4db1-b5e4-eda422afa0d9', 3, CAST(N'2024-07-06T22:23:20.063' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'45234d7d-d6db-43e3-9b6a-358ac80cbcf5', N'Pear Shaped Diamond', N'KCNN0007', 1.3, N'E', N'VVS1', N'Excellent', 270000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fd3f0ae90-3ee6-4a3d-befb-fb9469fb36fc?alt=media&token=d5d9e369-2cc4-4f3a-941a-50ef8d1fb349', 2, CAST(N'2024-07-06T23:12:58.953' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7cd', N'Brilliant Diamond ', N'KCNN0001', 1.5, N'D', N'VVS1', N'Excellent', 300000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F7ac921b8-ecea-479f-8f5c-f6a2634cae25?alt=media&token=9fa84d98-0b14-4c2d-8eb5-2c17a898b5e2', 2, CAST(N'2024-06-05T23:15:15.723' AS DateTime), CAST(N'2024-07-05T15:02:40.383' AS DateTime), 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'ea671522-4f79-41d8-888f-3a20ac6052d1', N'Heart Shaped Diamond', N'KCNN0009', 1.5, N'G', N'VS1', N'Excellent', 300000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F22880a58-c23f-4c7a-8026-b2d02e45a195?alt=media&token=249408db-6cc2-4f31-bbc8-8d4ae267f847', 3, CAST(N'2024-07-06T23:14:37.363' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'1eb89ee1-54b8-4a05-97e3-46d1808a9f83', N'Radiant Cut Diamond', N'KCNN0005', 1.1, N'D', N'VS2', N'Excellent', 220000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fa6d72a90-4bf6-41e8-a0bb-23201af4ad87?alt=media&token=30099de7-4b5f-4d7b-84b5-25391f969973', 3, CAST(N'2024-07-06T22:49:49.437' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'5230cfef-f437-40c3-a0d9-98e2eb0a1572', N'Asscher Cut Diamond', N'KCNN00010', 1.1, N'D', N'VVS2', N'Excellent', 230000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fff8c2359-ab50-4fce-a33a-94184c2394f1?alt=media&token=8084bd30-fdfe-4564-9004-864b930fdeb7', 3, CAST(N'2024-07-06T23:15:44.003' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'b06e68e1-0ff3-49bf-8bb6-a31893303915', N'Emerald Cut Diamond', N'KCNN0004', 1.5, N'G', N'IF', N'Excellent', 320000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fc5bea4f1-b0a0-4078-8ed9-b29d0c978c7b?alt=media&token=73501224-765c-4e1c-97b6-234044d68ee9', 3, CAST(N'2024-07-06T22:47:01.970' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'e91fe691-d2eb-4625-b123-bef5a3f9e1d8', N'Cushion Cut Diamond', N'KCNN0006', 1.4, N'H', N'SI1', N'Very Good', 260000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F94e31b25-d2af-4fd7-b66d-081057e808fb?alt=media&token=c1df6cb1-9142-4bc2-8cd9-5ddfedbf63af', 3, CAST(N'2024-07-06T23:10:57.480' AS DateTime), NULL, 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'6f0fc740-c465-4fd0-b071-bf726a8f9c4f', N'Princess Cut Diamond', N'KCNN0002', 1.2, N'E ', N'VS1', N'Excellent', 250000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fee4711b4-e9d9-49d0-8323-5e2749df8825?alt=media&token=b347b1af-2820-4917-afbd-9641dc0a7039', 2, CAST(N'2024-06-20T01:05:28.047' AS DateTime), CAST(N'2024-07-06T22:39:50.380' AS DateTime), 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'05c7f8ce-d9af-4c8f-8bd8-c889216ccafe', N'Trillion Cut Diamond', N'KCNN00011', 1.3, N'E', N'VS1', N'Very Good', 240000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fe5b428b1-028b-43d8-8d1b-3cf7abfd5d67?alt=media&token=d6334587-5536-47bf-8290-f8d6f61bc4eb', 2, CAST(N'2024-07-06T23:16:27.397' AS DateTime), CAST(N'2024-07-07T00:05:54.460' AS DateTime), 6, 1)
INSERT [dbo].[Diamond] ([Id], [DiamondName], [Code], [Carat], [Color], [Clarity], [Cut], [Price], [ImageDiamond], [Quantity], [InsDate], [UpsDate], [PeriodWarranty], [Deflag]) VALUES (N'cbade611-5f76-4036-86d1-ddadafdb8b9a', N'Marquise Cut Diamond', N'KCNN0008', 1.2, N'F', N'IF', N'Excellent', 255000000, N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fedda213f-3bf3-46c4-8471-68bea12bf6ca?alt=media&token=c33fb6c4-259e-4da3-a4df-7cdb200603a7', 2, CAST(N'2024-07-06T23:13:57.930' AS DateTime), NULL, 6, 1)
GO
INSERT [dbo].[Discount] ([Id], [OrderId], [ManagerId], [PercentDiscount], [Description], [ConditionDiscount], [Status], [InsDate], [UpsDate], [Note], [MembershipId], [LevelDiscount]) VALUES (N'0a15d388-ee57-4109-9385-9c1a5adffe4b', N'c098ddb9-d335-47e5-819d-7d5335135436', N'00000000-0000-0000-0000-000000000000', 1, N'Chiết khấu đơnc098ddb9-d335-47e5-819d-7d5335135436', 0, N'Accepted', CAST(N'2024-07-07T22:10:18.883' AS DateTime), CAST(N'2024-07-07T22:10:18.883' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Discount] ([Id], [OrderId], [ManagerId], [PercentDiscount], [Description], [ConditionDiscount], [Status], [InsDate], [UpsDate], [Note], [MembershipId], [LevelDiscount]) VALUES (N'844966ef-f8e0-48a0-b370-e0653e74c8a0', N'85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'00000000-0000-0000-0000-000000000000', 1, N'Chiết khấu đơn85f32cee-cf14-499e-8ec9-ebd70ffd05a8', 0, N'Rejected', CAST(N'2024-07-07T21:23:36.923' AS DateTime), CAST(N'2024-07-07T21:23:36.923' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Discount] ([Id], [OrderId], [ManagerId], [PercentDiscount], [Description], [ConditionDiscount], [Status], [InsDate], [UpsDate], [Note], [MembershipId], [LevelDiscount]) VALUES (N'f4c13373-7d09-4d0b-bd80-f8f2ca30bf02', N'a48ae106-d1a3-4972-bc9f-35f0369bc8e0', N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f6d88', 2, N'Giảm giá cho khách hàng thân thiết 15% tổng hóa đơn', 20000, N'Pending', CAST(N'2024-05-24T00:00:00.000' AS DateTime), CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'bb228c40-588a-4685-8fc8-0fd9e3111bd3', 1.2, CAST(N'2024-06-19T19:14:52.560' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'cf7fd302-56cf-46fb-84d6-1664c080a5be', 1, CAST(N'2024-06-23T17:05:49.907' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'5533a7f1-d354-4256-854e-28ff907026e2', 1, CAST(N'2024-06-12T11:12:15.750' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'c6445a3a-6d0b-41a8-9396-2ff3946bf25e', 1.5, CAST(N'2024-06-19T19:46:23.040' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'367c204f-5409-4208-850f-36048236bb4c', 1.2, CAST(N'2024-06-20T15:00:02.077' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'be4e536d-aa18-4bcc-af4c-510e8bf47486', 0, CAST(N'2024-06-19T19:28:09.250' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'6f8d0174-1f58-4b1b-b5b5-5bb9bc76ba01', 1.2, CAST(N'2024-06-23T16:13:01.083' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'52d2f3a4-93dd-4168-b7de-662fe2ded81d', 0, CAST(N'2024-06-19T19:25:28.047' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'12c54121-3968-4271-81a3-6694593a3720', 1.2, CAST(N'2024-06-19T19:49:40.160' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'36d98fb8-96b3-4183-a124-811def5b20b1', 1.6, CAST(N'2024-06-20T00:23:49.840' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'b54d01c5-9e9a-4692-ba95-8d3fdd87dfcf', 1.1, CAST(N'2024-06-19T19:54:28.573' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'8965fafb-78a7-4bb8-a43b-9774c2274675', 1.2, CAST(N'2024-06-24T00:21:34.490' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'747ba76d-4852-4330-8aa6-a49a54603eab', 1.2, CAST(N'2024-06-19T19:55:28.287' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'7f9b0123-2ff2-4dff-b2cd-a731722febb5', 1.3, CAST(N'2024-06-19T19:56:22.190' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'3f23c48f-2629-4fd2-aa70-b04695f83072', 1.25, CAST(N'2024-06-20T15:00:11.393' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'00a147be-a155-4258-a7b4-bda794e07ce2', 1.2, CAST(N'2024-06-23T16:17:09.827' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'3697dfbf-f46a-4a8a-a2a1-c328ce9b58a0', 0, CAST(N'2024-06-19T19:26:14.717' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'3c807a4d-3cd6-437e-9701-d1acaca76a42', 0.9, CAST(N'2024-06-23T16:12:36.230' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'24fd67d5-0df6-4b2e-83c9-ddef2b499b0d', 0, CAST(N'2024-06-19T19:44:02.323' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'ba4ad8b7-96a8-4768-87ff-f1b98feca4da', 0, CAST(N'2024-06-19T19:30:19.423' AS DateTime))
INSERT [dbo].[GoldRate] ([Id], [Rate], [UpsDate]) VALUES (N'1d9441b9-9fb9-49ea-9af6-fb69c6dd2d66', 1, CAST(N'2024-06-20T15:00:29.903' AS DateTime))
GO
INSERT [dbo].[Material] ([Id], [MaterialName], [InsDate]) VALUES (N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'Platinum', CAST(N'2024-06-21T23:24:13.807' AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialName], [InsDate]) VALUES (N'6deabe63-d453-49d2-aba9-4acf763170e8', N'Vàng Hồng', CAST(N'2024-06-21T23:24:04.690' AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialName], [InsDate]) VALUES (N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'Vàng', CAST(N'2024-06-21T23:19:45.100' AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialName], [InsDate]) VALUES (N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'Vàng Trắng', CAST(N'2024-06-21T23:23:47.643' AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialName], [InsDate]) VALUES (N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'Bạc', CAST(N'2024-06-21T23:24:23.610' AS DateTime))
GO
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'f9d52cdf-d443-412a-a404-168d5714ad85', N'aaa', 0, 0, N'568988eb-cfc6-411f-bd1e-b19010eb7921', 79940000, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'5fba9954-7278-416e-b5e8-4dae8040b411', N'Đồ', 0, 0, N'9e848019-0bd6-4b6a-8979-3935b604ac76', 111600000, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'2a90578c-4b60-411f-bd4e-4e892599c049', N'Thảo Uyên', 0, 0, N'26e37551-529e-43d6-8f5b-61ea03a1423d', 0, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'a165856e-6a97-4929-9294-af767435995a', N'Ab', 0, 0, N'1ae65ea3-2abd-463a-9f93-0df14e4658c7', 0, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'56c8fce1-7ca7-4a87-8061-c3f3fb7b6cf1', N'Ngan', 0, 0, N'43963465-03be-4b11-bed7-234bf8ba746a', 196161200, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
INSERT [dbo].[Membership] ([Id], [Name], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag], [MemberTypeId]) VALUES (N'472e91c0-abdd-476d-b88e-e1307ab53670', N'Nguyen Huu', 0, 0, N'4916c8d5-b606-40dc-a98b-057963b5149c', NULL, 1, N'18095960-acb3-4fd4-bcd3-646d9df3e6e1')
GO
INSERT [dbo].[MemberType] ([Id], [Type], [Levels], [NextLevel], [InsDate], [PercentDiscount]) VALUES (N'18095960-acb3-4fd4-bcd3-646d9df3e6e1', N'NewMember', 0, 10000000, CAST(N'2024-05-02T00:00:00.000' AS DateTime), 0.01)
INSERT [dbo].[MemberType] ([Id], [Type], [Levels], [NextLevel], [InsDate], [PercentDiscount]) VALUES (N'cac7bd96-530a-4184-989e-71218b2aaa0d', N'Gold', 3, 500000000, CAST(N'2024-05-02T00:00:00.000' AS DateTime), 0.05)
INSERT [dbo].[MemberType] ([Id], [Type], [Levels], [NextLevel], [InsDate], [PercentDiscount]) VALUES (N'bbc11f4d-06f4-4a43-976e-99bbd01adab4', N'Bronze', 1, 50000000, CAST(N'2024-05-02T00:00:00.000' AS DateTime), 0.02)
INSERT [dbo].[MemberType] ([Id], [Type], [Levels], [NextLevel], [InsDate], [PercentDiscount]) VALUES (N'37fb42ff-f7d7-4952-8c3f-b10e72445a67', N'Silver', 2, 100000000, CAST(N'2024-05-02T00:00:00.000' AS DateTime), 0.04)
GO
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'3020ec58-50af-4824-8c68-00d6c9b7c22b', N'95bd2d25-53e4-4c20-b460-60b0c75f26fc', N'BUY', CAST(N'2024-06-24T00:39:54.520' AS DateTime), NULL, 48700000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'7d71ade9-f33c-414a-89c6-13069d908a56', N'43963465-03be-4b11-bed7-234bf8ba746a', N'BUY', CAST(N'2024-06-24T10:48:26.290' AS DateTime), NULL, 38501200, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'af87c852-3eb3-4912-b35d-4271793fb615', N'1ae65ea3-2abd-463a-9f93-0df14e4658c7', N'BUY', CAST(N'2024-06-24T08:57:54.763' AS DateTime), NULL, 65401200, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'0efc9e98-9e2d-44d0-98fe-4b4e0f38ca8b', N'6ca95547-6677-4d93-8786-924c761baeaa', N'BUY', CAST(N'2024-06-11T11:57:35.167' AS DateTime), NULL, 28500000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'BUY', CAST(N'2024-05-25T13:38:30.997' AS DateTime), NULL, 9000000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'2dc3dfc9-5f75-440d-9117-686dcc23cc2e', N'6ca95547-6677-4d93-8786-924c761baeaa', N'BUY', CAST(N'2024-06-08T21:36:19.300' AS DateTime), NULL, 13500000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'15ec8001-e125-4746-ad95-7c047b37d7c2', N'95bd2d25-53e4-4c20-b460-60b0c75f26fc', N'BUY', CAST(N'2024-06-24T00:36:09.720' AS DateTime), NULL, 48700000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'c098ddb9-d335-47e5-819d-7d5335135436', N'43963465-03be-4b11-bed7-234bf8ba746a', N'BUY', CAST(N'2024-07-07T22:09:56.787' AS DateTime), NULL, 295400000, 3000000)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', N'BUY', CAST(N'2024-05-29T14:50:24.957' AS DateTime), NULL, 35000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'da2fb219-1936-4264-9603-7ff5a78b1427', N'6ca95547-6677-4d93-8786-924c761baeaa', N'BUY', CAST(N'2024-06-08T21:51:15.463' AS DateTime), NULL, 13500000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'da6dbad0-dd2c-46e4-8409-8d3d945cdf74', N'43963465-03be-4b11-bed7-234bf8ba746a', N'BUY', CAST(N'2024-06-24T10:55:46.053' AS DateTime), NULL, 38501200, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'8d804979-1809-4466-bb43-92f2dadc0dc2', N'9e848019-0bd6-4b6a-8979-3935b604ac76', N'BUY', CAST(N'2024-07-02T16:20:19.793' AS DateTime), NULL, 111600000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'eee6c363-51ff-43f8-a279-95544c177324', N'6ca95547-6677-4d93-8786-924c761baeaa', N'BUY', CAST(N'2024-06-10T02:14:30.103' AS DateTime), NULL, 175750000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'173c13df-908f-4399-951b-b1a53c0835c1', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'BUY', CAST(N'2024-05-25T13:25:42.233' AS DateTime), NULL, 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'd7416d34-9003-4bde-9b96-bf28c733f13c', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'BUY', CAST(N'2024-05-25T12:06:16.557' AS DateTime), N'f4c13373-7d09-4d0b-bd80-f8f2ca30bf02', 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'26e37551-529e-43d6-8f5b-61ea03a1423d', N'BUY', CAST(N'2024-06-24T08:22:56.983' AS DateTime), NULL, 156200000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'444c92f3-4825-44b0-8d99-e04dfcc3565f', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'BUY', CAST(N'2024-05-25T12:26:01.810' AS DateTime), NULL, 300000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'84d2e237-284f-4761-a2d1-ea506364848f', N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'BUY', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL, 100000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'43963465-03be-4b11-bed7-234bf8ba746a', N'BUY', CAST(N'2024-07-07T12:27:42.650' AS DateTime), NULL, 650260000, 0)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'dcd4f0a5-e923-45bc-81e2-ef1e1832c7d3', N'6ca95547-6677-4d93-8786-924c761baeaa', N'BUY', CAST(N'2024-06-10T07:33:11.537' AS DateTime), NULL, 15000000, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'915292b1-e804-4d7a-b663-f2f11ca0cb09', N'95bd2d25-53e4-4c20-b460-60b0c75f26fc', N'BUY', CAST(N'2024-06-24T00:40:57.473' AS DateTime), NULL, 39001200, NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Type], [InsDate], [DiscountId], [TotalPrice], [MaterialProcessPrice]) VALUES (N'c5082309-2994-4f03-ba90-f5837684831e', N'568988eb-cfc6-411f-bd1e-b19010eb7921', N'BUY', CAST(N'2024-07-04T16:20:09.410' AS DateTime), NULL, 79940000, 0)
GO
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'407a9873-d979-4294-8270-01e95cadf169', 3000000, 2, 0, 6000000, N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:38:31.000' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'9cf3c2a5-5bf2-42c2-9022-09b769b844fa', 180000, 1, 0, 180000, N'84d2e237-284f-4761-a2d1-ea506364848f', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-22T08:45:56.120' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'bc960be1-5596-4875-8cb0-0e95cfa6a138', 15000000, 1, 0, 15000000, N'0efc9e98-9e2d-44d0-98fe-4b4e0f38ca8b', N'10100449-e27b-40c9-906c-c9918221e2dc', CAST(N'2024-06-11T11:57:35.167' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'8385ea92-d7c9-4dd0-94e0-18c95bae6945', 38501200, 1, 0, 38501200, N'da6dbad0-dd2c-46e4-8409-8d3d945cdf74', N'49773e12-a168-4361-b9c2-fd57f858d6f8', CAST(N'2024-06-24T10:55:46.053' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'83a2e9b5-d024-4769-ad5a-1d29f58fc1ce', 48700000, 1, 0, 48700000, N'3020ec58-50af-4824-8c68-00d6c9b7c22b', N'7ece3dd3-a195-4b03-90b6-3c21ca5b651b', CAST(N'2024-06-24T00:39:54.520' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'373451e3-096b-4f14-b2db-28ae900d75fe', 10000, 1, 0, 10000, N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-29T14:50:24.960' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'60c1211f-635c-4f27-9c57-3e4808bacf8f', 39001200, 1, 0, 39001200, N'915292b1-e804-4d7a-b663-f2f11ca0cb09', N'59723d45-fdcd-45a7-a099-038f277f8353', CAST(N'2024-06-24T00:40:57.477' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'6b3de4c4-2442-4e3e-ba7b-4bc320e191cf', 15000000, 1, 0, 15000000, N'eee6c363-51ff-43f8-a279-95544c177324', N'10100449-e27b-40c9-906c-c9918221e2dc', CAST(N'2024-06-10T02:14:48.933' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'e8642dab-73ab-405a-b08c-4fc5195d5c4b', 38501200, 1, 0, 38501200, N'7d71ade9-f33c-414a-89c6-13069d908a56', N'49773e12-a168-4361-b9c2-fd57f858d6f8', CAST(N'2024-06-24T10:48:26.290' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'da1bd002-7eca-45dd-94c9-5721d5b8b44b', 111600000, 2, 0, 223200000, N'8d804979-1809-4466-bb43-92f2dadc0dc2', N'33d7bdb5-9e71-49f1-89b5-0e0f528a3ff6', CAST(N'2024-07-02T16:20:19.797' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'cd25d715-64d7-4c18-b1c1-669b34c35090', 35000000, 1, 0, 35000000, N'eee6c363-51ff-43f8-a279-95544c177324', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-06-10T02:14:54.980' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'3cbf70d2-8c4b-4aa3-a3e2-6addff75e85d', 62, 1, 0, 62, N'c098ddb9-d335-47e5-819d-7d5335135436', N'de52ccb2-823a-41ba-9572-8766a0041342', CAST(N'2024-07-07T22:09:56.793' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'01517f8c-c1ac-4483-89cb-6efb48599072', 3000000, 1, 0, 3000000, N'ac4bc1dc-700a-4421-8bcc-58463aa3a7ed', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:38:32.680' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'5eaaf3b5-a5d2-47e4-abb0-6fb2e57f3d33', 15000000, 1, 0, 15000000, N'2dc3dfc9-5f75-440d-9117-686dcc23cc2e', N'10100449-e27b-40c9-906c-c9918221e2dc', CAST(N'2024-06-08T21:36:19.300' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'7c2da0f8-fbe5-4f81-8a0a-77b2f1ee6123', 25000, 1, 0, 25000, N'4fc517e2-e314-4019-9d63-7d71cdbeb42e', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-29T14:50:26.023' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'b6556f6c-e94d-487c-b4b3-7c4708684e7a', 44, 1, 0, 44, N'85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'560c3651-29c0-484b-8aaf-197ff0f19557', CAST(N'2024-07-07T12:27:42.653' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'33e9b431-3a9a-4c1c-a160-888c9eda50b5', 48700000, 1, 0, 48700000, N'15ec8001-e125-4746-ad95-7c047b37d7c2', N'7ece3dd3-a195-4b03-90b6-3c21ca5b651b', CAST(N'2024-06-24T00:36:09.723' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'3646ce7b-691b-48ad-91ce-9d223601a360', 15000000, 1, 0, 15000000, N'da2fb219-1936-4264-9603-7ff5a78b1427', N'10100449-e27b-40c9-906c-c9918221e2dc', CAST(N'2024-06-08T21:51:15.463' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'51f4b1dc-509d-4f71-8609-a28eb87cadd1', 55800000, 1, 0, 55800000, N'c5082309-2994-4f03-ba90-f5837684831e', N'33d7bdb5-9e71-49f1-89b5-0e0f528a3ff6', CAST(N'2024-07-04T16:20:09.410' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'9ca4eeb3-8311-4abb-ad88-a64d349731ec', 39001200, 1, 0, 39001200, N'af87c852-3eb3-4912-b35d-4271793fb615', N'59723d45-fdcd-45a7-a099-038f277f8353', CAST(N'2024-06-24T08:57:54.767' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'70b87574-a6dc-41d3-ae86-a898e1474629', 55800000, 1, 0, 55800000, N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'73424805-5f2d-466f-a87e-c7a7cb758143', CAST(N'2024-06-24T08:22:57.013' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'1f670cf4-c7c9-4fb3-a888-ada4ee3ccc50', 28400000, 1, 0, 28400000, N'c5082309-2994-4f03-ba90-f5837684831e', N'34d5a90f-055e-4d96-a2b9-7a9c0ac75e73', CAST(N'2024-07-04T16:20:09.443' AS DateTime), N'e5b5e790-c100-4c7a-b980-16959f03ce4b')
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'c54ef65e-10c5-4ceb-98a3-b55456bb5a0d', 3000000, 2, 0, 6000000, N'173c13df-908f-4399-951b-b1a53c0835c1', N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', CAST(N'2024-05-25T13:26:15.910' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'9c9681d0-7081-433f-aff8-b96b4a4cb8c2', 40200000, 1, 0, 40200000, N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7ab', CAST(N'2024-06-24T08:22:57.013' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'a8850bfa-7cbe-4836-a631-c396fd8f125f', 15000000, 1, 0, 15000000, N'dcd4f0a5-e923-45bc-81e2-ef1e1832c7d3', N'10100449-e27b-40c9-906c-c9918221e2dc', CAST(N'2024-06-10T07:33:11.540' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'1f8900c6-1a87-492e-8c6f-c78cbf215768', 48700000, 1, 0, 48700000, N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'7ece3dd3-a195-4b03-90b6-3c21ca5b651b', CAST(N'2024-06-24T08:22:56.983' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'd6974dea-0f4d-4b96-a86f-c7b2e56ef168', 26400000, 1, 0, 26400000, N'af87c852-3eb3-4912-b35d-4271793fb615', N'deb22f5f-4489-48df-8bdb-841070a9b9c2', CAST(N'2024-06-24T08:57:54.847' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'e135f1c7-5866-42ef-a1b2-d3a24754d9e5', 50, 1, 0, 50, N'85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'405426c1-ed73-456b-832e-32a856dcd703', CAST(N'2024-07-07T12:27:42.733' AS DateTime), NULL)
INSERT [dbo].[OrderDetail] ([Id], [Amount], [Quantity], [Discount], [TotalPrice], [OrderId], [ProductId], [InsDate], [PromotionId]) VALUES (N'fd254a72-904a-4f79-8bc6-e2de2b583a02', 11500000, 1, 0, 11500000, N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'6afd5d64-c841-431b-b6c9-51034c6fe194', CAST(N'2024-06-24T08:22:57.013' AS DateTime), NULL)
GO
INSERT [dbo].[ProcessPrice] ([ProcesspriceId], [ProcessPrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (1, 1500000, 3.5, N'b2c3d4e5-6789-1011-1213-141516171819', N'Ring', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[ProcessPrice] ([ProcesspriceId], [ProcessPrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (2, 2000000, 6.5, N'c3d4e5f6-7890-1011-1213-141516171819', N'Bracelet', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[ProcessPrice] ([ProcesspriceId], [ProcessPrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (3, 2500000, 7.6, N'a1b2c3d4-5678-9101-1121-314151617181', N'Necklace', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[ProcessPrice] ([ProcesspriceId], [ProcessPrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (4, 1900000, 5.5, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Nhẫn', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'e2d3082a-0c99-4d87-832e-01529e82218d', N'YELLOW PETITE TWIST', N'14K Gold Petite Twist Ring', 28000000, 9, 39460000, CAST(N'2024-06-21T01:55:15.663' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T22:05:55.240' AS DateTime), 5, 2500000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0006', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fe0846f80-924a-447f-bd9b-3dfdd2d99684?alt=media&token=50244b4d-3bfe-4c55-ad64-e5adc7969894', 2800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'59723d45-fdcd-45a7-a099-038f277f8353', N'WHITE OVAL CLUSTER', N'14K White Gold Oval Diamond Cluster Ring', 30000000, 7, 39001200, CAST(N'2024-06-18T12:16:55.717' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T22:05:46.117' AS DateTime), 1, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0001', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Ff1caa6d9-93b7-400f-a533-66890c03ea99?alt=media&token=86667c9a-472e-4a56-8b1d-216a948f7f90', 1000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'672c89a3-0106-4316-82ca-0d8033f19d8b', N'SWAROVSKI HEART', N'Swarovski Heart Earings', 4000000, 0, 4400000, CAST(N'2024-06-21T16:35:27.137' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T22:05:00.437' AS DateTime), 4, 0, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'BTNN0008', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F33f25ded-5287-45f3-ad23-39cd248f33bb?alt=media&token=ce44524f-ddd5-411a-9ae3-75b9b6cb9635', 400000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'33d7bdb5-9e71-49f1-89b5-0e0f528a3ff6', N'18 WHITE DIAMOND', N'18K White Gold Petite Twist Ring', 40000000, 8, 55800000, CAST(N'2024-06-21T01:57:51.493' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T22:04:45.117' AS DateTime), 1, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0007', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fac859555-486c-4f7a-be93-a501348097eb?alt=media&token=243be374-653c-4780-9cc8-001c0d50ec91', 4000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'96ef9370-232c-4556-82c1-1307e50a1971', N'WHITE FLORAL 2', N'14K White Gold Floral Diamond Earings ', 30000000, 0, 48200000, CAST(N'2024-06-21T16:20:09.540' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T22:04:35.213' AS DateTime), 3, 5000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'BTNN0005', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fba6c3dd6-8c6a-49fe-a57d-ee81a078fe17?alt=media&token=89983e69-3662-4ec7-970c-bfb830c565aa', 6000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'560c3651-29c0-484b-8aaf-197ff0f19557', N'WHITE PETITE TWIST', N'14K White Gold Petite Twist Ring', 32000000, 8, 44740000, CAST(N'2024-06-21T01:45:41.590' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T22:03:41.653' AS DateTime), 4, 2500000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0005', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F09a24970-cdcf-4c39-be93-4d25c4a39e55?alt=media&token=5c46061c-713d-4ea3-a5cf-bd72a32095b5', 3200000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'e3ede694-8dbc-4bc6-a727-1dce609e59f8', N'18 YELLOW HIDDEN HALO', N'18K Gold Hidden Halo Diamond Ring', 38000000, 8, 53160000, CAST(N'2024-06-21T02:09:33.983' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T22:03:27.957' AS DateTime), 3, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0013', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F91470af4-768c-4636-b02b-3b205bca3deb?alt=media&token=461ea188-ae14-415c-ae97-98148ea20136', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'96a18b11-0cde-4279-b35b-21b8bc61c985', N'B.ZERO 1 YELLOW', N'18K Yellow Gold Bangle', 38000000, 18, 50160000, CAST(N'2024-06-21T21:26:14.353' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T22:02:34.147' AS DateTime), 3, 0, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'VTNN0006', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F730c0e22-7bed-42f3-b911-d44a7e044138?alt=media&token=3fc2128c-f2c1-4777-b9f8-40be65845ee5', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'455e3e06-dfc9-4de4-b6f2-230f005298d1', N'3PCS ', N'3PCS Rose Gold Diamond Earings ', 35000000, 0, 50500000, CAST(N'2024-06-21T15:59:49.000' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T22:02:20.437' AS DateTime), 2, 2500000, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'BTNN0001', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fe3078871-824c-42ee-9e47-30aeaa417cae?alt=media&token=1b5d33f6-d77e-4497-9fa4-473901f78cec', 5000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'f709845a-c14a-4cd2-8095-260a6c2d8252', N'DOUBLE STONE CZ', N'Silver Double Stone CZ Earings ', 15000000, 0, 18000000, CAST(N'2024-06-21T16:22:02.440' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:59:38.973' AS DateTime), 3, 1500000, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'BTNN0006', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F4e175978-a1b3-4245-8a09-fce655454d95?alt=media&token=fddfc0c1-073f-4010-923c-3badc4e44ca2', 1500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'405426c1-ed73-456b-832e-32a856dcd703', N'18 YELLOW DIAMOND', N'18K Gold Petite Twist Ring', 36000000, 8, 50520000, CAST(N'2024-06-21T01:58:58.510' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:57:46.727' AS DateTime), 3, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0008', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F176a2597-2ef2-48f0-9d65-eea46c2b00c0?alt=media&token=f25487c2-e2ee-4cce-adab-f3deeed03a50', 3600000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'd655b5a4-8fa1-4140-b744-35786fb473a6', N'THREE STONE ', N'Platinum Three Stone Diamond Ring', 35000000, 8, 40500000, CAST(N'2024-06-21T15:32:24.433' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:57:18.570' AS DateTime), 3, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0024', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F2e4f6289-3bad-4500-95c9-6137608b8770?alt=media&token=c4b8e5a7-2fbc-4a74-a5e6-7d3ed52f7f4d', 3500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7ab', N'GOLDEN K', N'14K Gold Necklace', 30000000, 40, 40200000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:50:03.283' AS DateTime), 3, 0, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'DCNN0009', N'https://cdn.pnj.io/images/detailed/162/sp-gd0000y000845-day-chuyen-nam-vang-18k-pnj-1.png', 3500000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'0e354f56-4c44-4aaa-bb06-3996a118d74a', N'WHITE ARROW', N'18K White Gold Bracelet', 38000000, 18, 51660000, CAST(N'2024-06-21T21:33:33.487' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:49:46.770' AS DateTime), 3, 1500000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0008', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F03328562-7aad-46cd-9254-590d1fd1a442?alt=media&token=275d2130-cbad-4d4b-9b12-7dc1ea6204e5', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'c1f68504-d733-4648-ad48-3bf24498e1eb', N'WHITE PEAR CLUSTER', N'14K White Gold Pear Diamond Cluster Ring', 30000000, 8, 41800000, CAST(N'2024-06-21T01:35:01.027' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:45:47.117' AS DateTime), 6, 2200000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0004', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F4b4aeef2-cce4-4923-9dd4-0c7aa718de52?alt=media&token=bb2970cf-2150-412d-8986-cac3ae45311b', 3000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'7ece3dd3-a195-4b03-90b6-3c21ca5b651b', N'BLUE SAPHIERE', N'14K White Gold Blue Saphiere Bracelet', 35000000, 18, 48700000, CAST(N'2024-06-21T17:04:35.617' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:45:39.173' AS DateTime), 1, 2500000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0001', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F314307ae-a461-4daa-a448-0ea8d9dc6478?alt=media&token=3cddc524-8d57-4a79-962b-00f939f7d39c', 3500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'8f7a4c80-867d-4c44-8bac-3e26872ab585', N'SOLITAIRE MOKUME', N'Platinum Solitaire Diamond Mokume Ring', 38000000, 7, 43800000, CAST(N'2024-06-21T15:36:28.777' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:44:46.500' AS DateTime), 3, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0026', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F9cc23f7f-f4c2-46dd-ac3e-aa1b7f77cfd7?alt=media&token=2185a7f8-9fd6-4f5f-8b92-9408c350c0b1', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'02f1864f-27ff-45d4-9d5d-3fff85066af3', N'DIVA''S DREAM', N'18K Rose Gold Bracelet', 38000000, 18, 52360000, CAST(N'2024-06-21T21:40:21.917' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:44:02.560' AS DateTime), 3, 2200000, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'VTNN0009', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F7cf8c8b4-8056-4c96-b41a-03ff5ba95979?alt=media&token=47214e71-d61e-4638-a97c-25d70b80cea0', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'4a9a051a-f23e-4e55-b8b7-40b3aefa98d2', N'DOUBLE RUBELLITE WHITE', N'18K White Gold Bracelet', 65000000, 18, 90800000, CAST(N'2024-06-21T21:57:14.583' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:43:52.307' AS DateTime), 4, 5000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0014', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Ff2c12f2f-46c7-48b4-b697-82f8e47d1b1a?alt=media&token=750145c4-3602-4744-b5e8-30a783f652e7', 6500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'd02873fe-8d73-4fd4-9001-416b91d45925', N'WHITE FLORAL 1', N'14K White Gold Floral Diamond Earings ', 30000000, 0, 48200000, CAST(N'2024-06-21T16:17:44.420' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:43:25.480' AS DateTime), 3, 5000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'BTNN0004', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F8b4f1a06-d3ea-4e03-9654-e424a39c0919?alt=media&token=622b1f43-a1e2-4f6e-9e21-33abfb3b6c8e', 6000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'6afd5d64-c841-431b-b6c9-51034c6fe194', N'WHITE FLOWER ', N'White Diamond Silver Earings', 10000000, 0, 11500000, CAST(N'2024-06-21T16:37:29.050' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:42:48.377' AS DateTime), 3, 0, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'BTNN0009', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F2a363686-5f58-405f-b597-fd7d48088e24?alt=media&token=39cbbf23-875a-4de6-9ca0-c3819ea8eaff', 1500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'36faf9db-c310-49aa-9683-64925f93ee29', N'WHITE EMERALD', N'14K White Gold Diamond Emerald Bracelet', 35000000, 18, 47900000, CAST(N'2024-06-21T21:20:10.160' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:42:29.417' AS DateTime), 3, 3500000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0004', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F6cdb9023-82b0-4f95-ad86-59b707272da3?alt=media&token=1085fd9d-fd3c-4daa-882d-a1ad4b5c32cb', 2000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'ee564182-b101-4e25-988e-6b8e58e35155', N'GOLD SUMMER', N'Gold Summer Earings ', 15000000, 0, 20200000, CAST(N'2024-06-21T16:23:04.887' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:42:14.107' AS DateTime), 3, 1000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'BTNN0007', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fa02908bd-372a-4412-8221-8bba7758a01a?alt=media&token=634a4237-2b01-45a1-a6d1-702253f48863', 1000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'495f8c92-0752-42c2-ad6c-6eb914eb9a5f', N'OPEN HEART LARIAT', N'Open Heart Lariat Silver Necklace', 5000000, 42, 5001000, CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:41:38.873' AS DateTime), 4, 0, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'DCNN0008', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F6bf0d6cb-2f47-4e9a-9c4c-e12710d2abae?alt=media&token=527bb030-a7e7-4e7a-a49c-44d7f9b59111', 1000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'5c2abd51-46df-44c9-8807-6fe20cc0b21b', N'ROSE FLORAL', N'Platinum Rose Floral Diamond Ring', 33000000, 7, 38300000, CAST(N'2024-06-21T15:34:04.847' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:41:04.460' AS DateTime), 3, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0025', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fb45e07c0-2372-4437-bf3a-cc67293e4f12?alt=media&token=73e52d29-76da-4d65-bf36-fc70adb3b4ff', 3300000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'a82d88bb-c78a-46f0-8a8f-74a8a83bc34e', N'AQUAMARINE', N'Platinum Aquamarine Blue Saphiere Diamond Ring', 50000000, 8, 58000000, CAST(N'2024-06-21T02:17:33.423' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:40:55.893' AS DateTime), 3, 3000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0016', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F58e98fb8-ee88-4037-8c84-08b9c667efc5?alt=media&token=f12a0be0-05bc-4a1d-abdc-a2efcbcbfc63', 5000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'671d2ef6-30a9-45e1-ad16-75c98ae12aaa', N'HAND - ENGRAVED BAND', N'Platinum Hand - Engraved Band', 20000000, 7, 24000000, CAST(N'2024-06-21T02:25:33.353' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:40:45.633' AS DateTime), 4, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0020', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fcae3644d-0fd8-409e-a0a9-4e1f143c238f?alt=media&token=1a1ee190-cd5e-4f6a-b93d-5805f97359c9', 2000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'03b7fe69-9959-4c40-aa95-76103bd0cd06', N'SERPENTI ROSE EMERALD', N'18K Rose Gold Bracelet', 48000000, 18, 68360000, CAST(N'2024-06-21T21:49:06.453' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:40:34.747' AS DateTime), 4, 5000000, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'VTNN0011', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fc6720e3d-a84f-4e98-8664-12926a574dda?alt=media&token=6f226ddc-1360-469c-bcca-f9de1082e8f1', 4800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'34d5a90f-055e-4d96-a2b9-7a9c0ac75e73', N'BLUE - WHITE CRYSTAL', N'Blue White Gold Crystal Earings ', 20000000, 0, 28400000, CAST(N'2024-06-21T16:02:14.723' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:40:18.623' AS DateTime), 1, 2000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'BTNN0002', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fa117cb57-d2a5-493c-b7de-7b43cd289ae3?alt=media&token=7f9b223c-562b-439d-86c7-9e645b9f74a9', 2000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'deb22f5f-4489-48df-8bdb-841070a9b9c2', N'SIMPLE BANGLE', N'14K White Gold Diamond Bangle ', 20000000, 18, 26400000, CAST(N'2024-06-21T17:09:29.960' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:40:07.980' AS DateTime), 2, 0, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0002', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fee1184cb-2015-411b-854e-0955d9c3f019?alt=media&token=f031664b-5f5c-482a-9e8f-5f4536165b61', 2000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'de52ccb2-823a-41ba-9572-8766a0041342', N'18 WHITE EMERALD', N'18K White Gold Emerald Diamond Ring', 45000000, 7, 62400000, CAST(N'2024-06-21T02:01:22.040' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:39:58.423' AS DateTime), 3, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0009', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F18bceedb-a8fa-47af-851a-a935adb19293?alt=media&token=33301d53-a0af-409c-b504-d95b0b51fe45', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'bbdeea7f-56e3-43f1-8dad-8e7e8cb1df00', N'RADIANT ETERNITY', N'Platinum Radient Eternity Band', 47000000, 8, 54700000, CAST(N'2024-06-21T02:30:51.633' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:39:42.187' AS DateTime), 3, 3000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0022', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F0015e2fa-97e1-4b0b-bac0-00492c67f70a?alt=media&token=1e11770e-fdb9-4fdb-aee9-383908b4e3e4', 4700000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'dcaa7278-c266-43ea-9b1b-931cd5864754', N'18 YELLOW EMERALD', N'18K Gold Emerald Diamond Ring', 40000000, 7, 55800000, CAST(N'2024-06-21T02:02:01.440' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:39:33.843' AS DateTime), 4, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0010', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F8d4e6437-23e3-4757-a4bc-f050c5884041?alt=media&token=e72ca195-8abd-4e33-9711-ee203035ac79', 4000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'c3e73c10-31ff-4433-841c-9d898f677178', N'18 WHITE MONTANA ', N'18K White Gold Montana Saphiere Halo Diamond Ring', 45000000, 8, 62400000, CAST(N'2024-06-21T02:11:18.477' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:39:20.857' AS DateTime), 3, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0014', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fa757bd5d-3b62-487e-b5da-73f4b893ef24?alt=media&token=bf9b8b07-a80e-43f0-b68a-04f5a04b3044', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'542b638d-590f-4047-9642-9f0973277f0b', N'YELLOW PEAR CLUSTER', N'14K Gold Pear Diamond Cluster Ring', 28000000, 8, 39160000, CAST(N'2024-06-21T01:30:49.640' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:39:12.830' AS DateTime), 5, 2200000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0003', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F746aba6e-c64e-4074-8b65-6aefdec5bfbd?alt=media&token=629b6294-de1e-4d6b-8475-6f9972857945', 2800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'208a7784-78fa-4c6d-9746-a7df8059a822', N'SERPENTI WHITE BLUE SAPHIERE', N'18K White Gold Bracelet', 45000000, 18, 64400000, CAST(N'2024-06-21T21:48:03.447' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:39:05.500' AS DateTime), 4, 5000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0010', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fab03f529-2111-4964-a8a5-833b5bc676d3?alt=media&token=3a9eec02-db2c-4e84-8d5f-466fb41bb1d8', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'b6c03c8b-0aff-4ce4-99f0-bb632a010a88', N'B.ZERO CHARM', N'18K White Gold Bracelet', 45000000, 18, 59400000, CAST(N'2024-06-21T21:30:32.537' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:38:57.920' AS DateTime), 3, 0, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0007', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F74033e72-5a15-4cf3-b106-d29f76877201?alt=media&token=2702b6fa-e04a-45e7-9cde-c4beaaa372d5', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'b2e97911-7fcb-40bd-acaf-be61425eb606', N'THREE STONE ALEXANDRITE & PEAR', N'Platinum Three Stone Alexandrite And Pear Diamond Ring', 40000000, 7, 46500000, CAST(N'2024-06-21T15:38:13.937' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:38:48.653' AS DateTime), 3, 2500000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0027', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fdd87180a-6796-4c0e-a0c3-cff917ab435e?alt=media&token=aa93d661-d0a5-4720-a674-e763d386525c', 4000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'744e2263-7f4e-4f0e-960b-c0a19a17b503', N'18 YELLOW FLORAL ', N'18K Gold Floral Diamond Ring', 38000000, 8, 53160000, CAST(N'2024-06-21T02:07:21.430' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:38:39.857' AS DateTime), 4, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0012', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F59e09cbb-979a-4d7e-83c0-6e64ce1d8b18?alt=media&token=70a7c061-e0b6-4574-9b9a-38f36ca50334', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'46921f15-d898-4d57-b9b1-c418d932fc33', N'18 WHITE HIDDEN HALO', N'18K White Gold Hidden Halo Diamond Ring', 42000000, 8, 58440000, CAST(N'2024-06-21T02:09:00.760' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:38:30.113' AS DateTime), 3, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0013', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F209dd8c2-c94a-44fd-89af-cd00c5de01ef?alt=media&token=e6946569-4abc-4066-b814-43f6a464d24b', 4200000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'6cf13da5-f172-45e3-9084-c685d03e0816', N'B.ZERO 1 WHITE', N'18K White Gold Bangle', 38000000, 18, 50160000, CAST(N'2024-06-21T21:23:54.407' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:38:19.227' AS DateTime), 3, 0, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0005', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F4b687097-edac-490c-91d6-aecc617158d9?alt=media&token=39d26481-3ebd-400e-93c7-66069863abc4', 3800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'73424805-5f2d-466f-a87e-c7a7cb758143', N'18 YELLOW MONTANA ', N'18K Gold Montana Saphiere Halo Diamond Ring', 40000000, 8, 55800000, CAST(N'2024-06-21T02:12:09.327' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:38:10.520' AS DateTime), 2, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0015', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Ffb816715-4e78-48f2-b55d-21b60c0d3e77?alt=media&token=997603a2-b799-4e14-9b31-833ece311bbb', 4000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'307bb076-2d4f-4d17-9974-c95c5d2eca4c', N'WHITE BEZEL', N'14K White Gold Diamond Bezel ', 20000000, 18, 26400000, CAST(N'2024-06-21T21:15:55.653' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:38:00.087' AS DateTime), 3, 0, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0003', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F5ca380ce-d9c3-4151-ac24-a7d379d7b8fb?alt=media&token=e8934597-1ea9-497e-8217-939b06317109', 2000000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'10100449-e27b-40c9-906c-c9918221e2dc', N'DOUBLE CIRCLE PANDENT', N'18K Gold Double Circle Pandent Necklace', 25000000, 40, 34800000, CAST(N'2024-06-05T23:15:15.723' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:37:30.843' AS DateTime), 8, 1800000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'DCNN0007', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F126e0b36-013e-476c-b93b-78dfdce56065?alt=media&token=cf010211-ef97-4151-976b-189ec9c2e859', 2500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'aadabb8e-cc02-4fc3-af0e-cb5903878d1a', N'CROWN PRONG', N'Platinum Crown Prong Crystal Earings ', 22500000, 0, 27250000, CAST(N'2024-06-21T16:14:12.913' AS DateTime), 1, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', CAST(N'2024-06-22T21:37:16.533' AS DateTime), 3, 2500000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'BTNN0003', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Ffa1f9a7c-8062-4e9e-b607-368181c74d14?alt=media&token=0c6be741-b0c8-4e0c-8d99-001f1912e611', 2250000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'a818b3e9-75e6-4350-8a89-cb6276ca3755', N'ORBIT ETERNITY', N'Platinum Orbit Eternity Band', 26000000, 7, 30600000, CAST(N'2024-06-21T02:28:38.543' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:37:05.523' AS DateTime), 3, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0021', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F256db788-509d-4b21-b09c-339577b37813?alt=media&token=5ebc8641-9eb5-45ed-a98d-c8ea398cbedb', 2600000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'3f54a54b-2f7e-410f-990e-cdf0021a2b3c', N'SOLITAIRE ', N'14K Gold Diamond Solitaire Necklace', 30000000, 40, 47400000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:36:50.260' AS DateTime), 6, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'DCNN0006', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F1c70355d-2620-4c2f-a41a-3c3692b8c858?alt=media&token=ede81ba2-4a7d-4249-b0fd-a37446f4d921', 7000000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'3f54a54b-2f7e-410f-990e-cdf0021e2b3c', N'RED DIAMOND HEART', N'Red Diamond Heart Silver Necklace', 15000000, 40, 19000000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:36:33.387' AS DateTime), 6, 1500000, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'DCNN0005', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F0259c989-a9d1-4e0d-aaf6-238dd2acec45?alt=media&token=d2a1ee7c-3bbe-4165-bc6f-5cd01e31a576', 2500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'3f54a54b-2f7e-410f-990e-cdf0121e2b3c', N'PURPLE CRYSTAL ', N'Silver Purple Crystal Necklace', 15000000, 40, 19000000, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:36:41.933' AS DateTime), 5, 1500000, N'b7dbd466-02aa-4831-a1c1-ee871ccbd7eb', N'DCNN0004', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F0cc3b5db-44a6-411c-896e-de688d73893e?alt=media&token=6ef45035-8d22-4ca3-984f-dffd2401b55b', 2500000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'aa4f6921-6338-4919-821f-cf022a30a632', N'18 WHITE FLORAL ', N'18K White Gold Floral Diamond Ring', 42000000, 8, 58440000, CAST(N'2024-06-21T02:05:10.950' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:33:27.153' AS DateTime), 4, 3000000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'N0NN0011', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F7185e734-803f-4fb3-925a-18875c3ade1c?alt=media&token=598d1ef8-c01f-42ac-a548-5eb364f81c21', 4200000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'36e148b6-982f-47b4-abb3-cf431d90aa69', N'STACKABLE GEOMETRIC', N'Platinum Stackable Geometric Band', 28000000, 8, 32800000, CAST(N'2024-06-21T02:32:20.100' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:30:18.797' AS DateTime), 3, 2000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0023', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fcc947daa-1573-4bd9-96ef-5ca165342343?alt=media&token=c2164065-6759-4940-bc50-97ae99af053b', 2800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'73d3c69b-3f19-4e34-a13a-d301c144aa15', N'HAND ENGRAVE', N'Platinum Hand Engrave Ruby Diamond Ring', 45000000, 7, 52500000, CAST(N'2024-06-21T02:20:27.467' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:30:11.693' AS DateTime), 3, 3000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0017', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fe6480d76-0b19-4d08-8286-c55ee75257ff?alt=media&token=df268d25-4767-4a39-8c3b-38bdbf6d1cf8', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'c1a596c1-8a7b-47ed-a1ce-de5b41715eb8', N'CONTOURED BLACK RHODIUM BAND', N'Platinum Contoured Black Rhodium Band', 22000000, 8, 27200000, CAST(N'2024-06-21T02:24:00.147' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:30:05.960' AS DateTime), 4, 3000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0019', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fba2f0508-38a3-4d73-91db-2446228c5990?alt=media&token=fb347009-e2a4-4fa6-a39d-62e6ecd3a95f', 2200000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'bb24413a-a254-438f-8d11-e0e1fbae1961', N'HAND - HEART OCEAN', N'14K Gold Diamond Hand - Heart Necklace', 35000000, 20, 45001200, CAST(N'2024-06-18T12:20:20.413' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:29:58.190' AS DateTime), 5, 3000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'DCNN0003', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F30675bd5-9ec2-490f-9896-8f551da4dc07?alt=media&token=c0452d85-d89f-4574-8330-5b436dcce93c', 1000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'bb954ee6-b7e4-4dd0-9274-e49ea17751db', N'ROSE SERPENTI VIPER', N'18K Rose Gold Bracelet', 35000000, 18, 46200000, CAST(N'2024-06-21T21:58:44.047' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:29:46.033' AS DateTime), 4, 0, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'VTNN0016', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fb381b878-f024-45db-ae9c-595309c772b9?alt=media&token=62e5bdbc-8282-4e5d-a537-5b53adf81d19', 3500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'30c71f0c-6914-4680-a0d5-e69aceea2d58', N'WHITE SERPENTI VIPER', N'18K White Gold Bracelet', 35000000, 18, 46200000, CAST(N'2024-06-21T21:58:13.127' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:29:40.883' AS DateTime), 4, 0, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'VTNN0015', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F8e899979-1fdd-4d51-806a-fea78ee33779?alt=media&token=3a67604f-dc5c-41e7-899d-56d3fc906ebc', 3500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'714adc42-d8ab-4271-a692-e87ff9045834', N'KNIFE EDGED BAND', N'Platinum Knife Edged Band', 32000000, 8, 38200000, CAST(N'2024-06-21T02:22:10.750' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:29:31.557' AS DateTime), 4, 3000000, N'd0813279-2ba5-4ec8-b544-466f8d29cca3', N'N0NN0018', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fefcd2f60-0dfc-4929-b448-f1ffccb65af6?alt=media&token=17e8737f-4338-48b1-b4fb-143ff11a8f43', 3200000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'bfc8ec95-7b69-4eae-ba7f-ede76449eb1c', N'DOUBLE RUBELLITE ROSE', N'18K Rose Gold Bracelet', 65000000, 18, 90800000, CAST(N'2024-06-21T21:55:11.077' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:29:23.967' AS DateTime), 4, 5000000, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'VTNN0013', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2Fc0b181a6-0a1a-4e27-97c7-b24e076e0f9e?alt=media&token=328bf199-aae7-4860-9e6e-fa435a02adfe', 6500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'51d21598-0887-4f17-b4b0-f55ee1b4925d', N'RUBELLITE ROSE', N'18K Rose Gold Bracelet', 45000000, 18, 64400000, CAST(N'2024-06-21T21:51:51.370' AS DateTime), 1, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-06-22T21:29:13.837' AS DateTime), 4, 5000000, N'6deabe63-d453-49d2-aba9-4acf763170e8', N'VTNN0012', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F557b0452-798e-4654-9beb-62ec281fbe69?alt=media&token=f8f8915d-4639-4308-8e1b-30401f09aa16', 4500000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'847224d3-12c2-47eb-9f9b-f6e9a93944cb', N'OPEN HEART', N'Yellow Gold Open Heart 14K Necklace', 20000000, 40, 25001200, CAST(N'2024-06-18T12:59:31.783' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-22T21:29:05.943' AS DateTime), 8, 1000000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'DCNN0002', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F8060109f-128e-4f1f-a99c-3b2ce495a331?alt=media&token=23eb74bc-6d36-4410-9d64-488f8d820827', 1000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'b4025767-dd1b-49a6-8c5c-fbbdd5779f95', N'YELLOW  OVAL CLUSTER', N'14K Gold Oval Diamond Cluster Ring', 28000000, 8, 39160000, CAST(N'2024-06-21T01:28:32.147' AS DateTime), 1, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-06-22T21:21:44.537' AS DateTime), 5, 2200000, N'805a6669-0372-4917-a87e-c08c6e68e4ea', N'N0NN0002', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F25d68ad8-4190-46f9-9029-7646d02f99f7?alt=media&token=8ffb341f-7446-4284-bbc0-ec25b8ae96e4', 2800000, NULL, 6)
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [ImportPrice], [Size], [SellingPrice], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [ProcessPrice], [MaterialId], [Code], [ImgProduct], [Tax], [SubId], [PeriodWarranty]) VALUES (N'49773e12-a168-4361-b9c2-fd57f858d6f8', N'3 HEART 1 DIAMOND', N'Heart-Diamond White Gold Necklace', 30000000, 42, 38501200, CAST(N'2024-06-18T12:55:54.173' AS DateTime), 1, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-06-21T23:57:18.263' AS DateTime), 4, 2500000, N'6c4d9d19-d733-404d-a79e-e2b80db45416', N'DCNN0001', N'https://firebasestorage.googleapis.com/v0/b/jssimage-253a4.appspot.com/o/uploads%2F73e109c7-861f-4170-9a05-ee563776cf2e?alt=media&token=689bb8d2-e648-4dc0-8fb1-e8972e131140', 1000, N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', 6)
GO
INSERT [dbo].[ProductConditionGroup] ([Id], [ProductId], [InsDate], [PromotionId], [Quantity]) VALUES (N'c84c0c37-7cea-4961-a551-898917377802', N'34d5a90f-055e-4d96-a2b9-7a9c0ac75e73', CAST(N'2024-05-22T08:45:56.120' AS DateTime), N'e5b5e790-c100-4c7a-b980-16959f03ce4b', 1)
INSERT [dbo].[ProductConditionGroup] ([Id], [ProductId], [InsDate], [PromotionId], [Quantity]) VALUES (N'fe68a7ce-ed61-4750-bd40-d340e41b82c2', N'03b7fe69-9959-4c40-aa95-76103bd0cd06', CAST(N'2024-05-22T08:45:56.120' AS DateTime), N'ae583068-e2ee-4fdb-86b4-d669e0ca30cb', 1)
GO
INSERT [dbo].[Promotion] ([Id], [PromotionName], [Type], [Description], [ProductQuantity], [Percentage], [InsDate], [UpsDate], [StartDate], [EndDate], [ImgURL], [Deflag]) VALUES (N'e5b5e790-c100-4c7a-b980-16959f03ce4b', N'Khuyến mãi cho khách hàng mới 15% cho sản phẩm đầu tiên', N'Product', N'Khuyến mãi cho khách hàng mới 15% cho sản phẩm đầu tiên', 1, 15, CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-30T08:45:56.120' AS DateTime), CAST(N'2025-06-15T08:45:56.120' AS DateTime), NULL, NULL)
INSERT [dbo].[Promotion] ([Id], [PromotionName], [Type], [Description], [ProductQuantity], [Percentage], [InsDate], [UpsDate], [StartDate], [EndDate], [ImgURL], [Deflag]) VALUES (N'442373e5-da8f-4999-857b-904c71430339', N'Test', N'Product', N'Test', 15, 15, CAST(N'2024-06-20T08:11:17.303' AS DateTime), CAST(N'2024-06-20T08:11:17.303' AS DateTime), CAST(N'2024-06-20T08:11:00.000' AS DateTime), CAST(N'2024-06-22T08:11:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Promotion] ([Id], [PromotionName], [Type], [Description], [ProductQuantity], [Percentage], [InsDate], [UpsDate], [StartDate], [EndDate], [ImgURL], [Deflag]) VALUES (N'ae583068-e2ee-4fdb-86b4-d669e0ca30cb', N'Giảm 10% cho sản phẩm vòng bạc', N'Product', N'Giảm 10% cho sản phẩm vòng bạc', 1, 10, CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-30T08:45:56.120' AS DateTime), CAST(N'2025-06-15T08:45:56.120' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PurchasePrice] ([PurchasePriceId], [PurchasePrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (1, N'10000000  ', 3.5, N'b2c3d4e5-6789-1011-1213-141516171819', N'Ring', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[PurchasePrice] ([PurchasePriceId], [PurchasePrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (2, N'10000000  ', 6.5, N'c3d4e5f6-7890-1011-1213-141516171819', N'Bracelet', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[PurchasePrice] ([PurchasePriceId], [PurchasePrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (3, N'10000000  ', 6.5, N'a1b2c3d4-5678-9101-1121-314151617181', N'Necklace', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[PurchasePrice] ([PurchasePriceId], [PurchasePrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (4, N'10000000  ', 5.5, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Nhẫn', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
INSERT [dbo].[PurchasePrice] ([PurchasePriceId], [PurchasePrice], [Size], [CategoryId], [Description], [UpsDate], [Status]) VALUES (5, N'10000000  ', 7.5, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Nhẫn', CAST(N'2024-06-18T12:13:25.107' AS DateTime), 1)
GO
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'0f8fad5b-d9cb-469f-a165-70867728950e', N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae7', N'Manager')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae8', N'Staff')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae9', N'Customer')
GO
INSERT [dbo].[Stall] ([Id], [Name], [Number], [StaffId], [Deflag]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f7d22', N'Do Huu Thuan', 12, N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', 1)
GO
INSERT [dbo].[SubProducts] ([Id], [TitleProductName], [Description], [Status]) VALUES (N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1', N'San pham thu mua', N'san pham thu mua', 1)
INSERT [dbo].[SubProducts] ([Id], [TitleProductName], [Description], [Status]) VALUES (N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a2', N'san pham ban', N'san pham ban ', 1)
INSERT [dbo].[SubProducts] ([Id], [TitleProductName], [Description], [Status]) VALUES (N'b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a6', N'San gia cong', N'San pham gia cong khong phai cua tiem', 1)
GO
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'ad9ad780-18d6-4200-b47e-053cd15c80cc', N'512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'Đã thanh toán đơn 512d3c3d-80be-4f8e-b6c7-cb38474023e4', N'đ', 156200000, CAST(N'2024-06-24T08:22:57.063' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'173c13df-908f-4399-931b-11a23c5335c1', N'84d2e237-284f-4761-a2d1-ea506364848f', N'Giao dịch 2', N'USD', 100000, CAST(N'2024-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'173c13df-908f-4399-931b-11a23c5335c2', N'c5082309-2994-4f03-ba90-f5837684831e', N'Giao dịch 1', N'USD', 79940000, CAST(N'2024-06-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'8d21ced1-5734-4071-8cfd-16323c0f4a56', N'da6dbad0-dd2c-46e4-8409-8d3d945cdf74', N'Đã thanh toán đơn da6dbad0-dd2c-46e4-8409-8d3d945cdf74', N'đ', 38501200, CAST(N'2024-06-24T10:56:17.893' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'dbc29432-40fb-45cc-9142-1b2fc3730cde', N'15ec8001-e125-4746-ad95-7c047b37d7c2', N'Đã thanh toán đơn 15ec8001-e125-4746-ad95-7c047b37d7c2', N'đ', 48700000, CAST(N'2024-06-24T00:39:54.507' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'56a95b53-4ed3-46df-b80a-237633ae9e37', N'c5082309-2994-4f03-ba90-f5837684831e', N'Đã thanh toán đơn c5082309-2994-4f03-ba90-f5837684831e', N'đ', 79940000, CAST(N'2024-07-04T16:20:09.543' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'03ad5166-71c0-4eda-be5d-2fa32f32ef9e', N'915292b1-e804-4d7a-b663-f2f11ca0cb09', N'Đã thanh toán đơn 915292b1-e804-4d7a-b663-f2f11ca0cb09', N'đ', 39001200, CAST(N'2024-06-24T00:40:57.637' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'0615bff1-340d-465e-8f27-3821e62d5522', N'7d71ade9-f33c-414a-89c6-13069d908a56', N'Đã thanh toán đơn 7d71ade9-f33c-414a-89c6-13069d908a56', N'đ', 38501200, CAST(N'2024-06-24T10:48:26.370' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'5715e3e4-d77c-44cc-9727-9a5e67fcbd61', N'c098ddb9-d335-47e5-819d-7d5335135436', N'Đã thanh toán đơn c098ddb9-d335-47e5-819d-7d5335135436', N'đ', 62400000, CAST(N'2024-07-07T22:09:57.083' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'e4f6a5e0-5f6e-4b19-b85e-bc31709abb2b', N'8d804979-1809-4466-bb43-92f2dadc0dc2', N'Đã thanh toán đơn 8d804979-1809-4466-bb43-92f2dadc0dc2', N'đ', 111600000, CAST(N'2024-07-02T16:20:19.997' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'63f9e872-cfba-47d6-82b3-c124fe618e75', N'85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'Đã thanh toán đơn 85f32cee-cf14-499e-8ec9-ebd70ffd05a8', N'đ', 95260000, CAST(N'2024-07-07T12:27:42.837' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'70255fe5-c90a-49a0-8d8b-c1bf2860468a', N'af87c852-3eb3-4912-b35d-4271793fb615', N'Đã thanh toán đơn af87c852-3eb3-4912-b35d-4271793fb615', N'đ', 65401200, CAST(N'2024-06-24T08:57:54.987' AS DateTime))
INSERT [dbo].[Transaction] ([Id], [OrderId], [Description], [Currency], [TotalPrice], [InsDate]) VALUES (N'83712b4e-c84d-4d06-ba42-da0449d62a1e', N'3020ec58-50af-4824-8c68-00d6c9b7c22b', N'Đã thanh toán đơn 3020ec58-50af-4824-8c68-00d6c9b7c22b', N'đ', 48700000, CAST(N'2024-06-24T00:39:54.523' AS DateTime))
GO
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'dd79d744-930f-4e47-9202-081b9143ec24', CAST(N'2024-06-24T00:00:00.000' AS DateTime), CAST(N'2024-07-04T00:00:00.000' AS DateTime), 6, 1, N'83a2e9b5-d024-4769-ad5a-1d29f58fc1ce', N'0913769909', N'Active', N'', NULL)
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', CAST(N'2024-06-24T00:00:00.000' AS DateTime), CAST(N'2024-12-25T00:00:00.000' AS DateTime), 6, 1, N'd6974dea-0f4d-4b96-a86f-c7b2e56ef168', N'024578963', N'Active', N'', NULL)
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'018329c2-a87c-4247-821f-969b00177f3b', CAST(N'2024-06-12T14:50:02.813' AS DateTime), CAST(N'2024-06-12T14:50:02.813' AS DateTime), 1, 1, N'407a9873-d979-4294-8270-01e95cadf169', N'090667052 ', N'Active', N'Nhẫn bạc', NULL)
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'9cd31f7d-75b0-47f8-88fb-a180df937925', CAST(N'2024-06-12T14:50:02.983' AS DateTime), CAST(N'2024-06-12T14:50:02.983' AS DateTime), 12, 1, N'9cf3c2a5-5bf2-42c2-9022-09b769b844fa', N'090667052 ', N'Active', N'Nhẫn bạc', NULL)
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'44fea9ad-2fed-4737-a4ba-b4bc67edbc19', CAST(N'2024-07-07T00:00:00.000' AS DateTime), CAST(N'2025-01-07T00:00:00.000' AS DateTime), 6, 1, N'3cbf70d2-8c4b-4aa3-a3e2-6addff75e85d', N'0906697051', N'Active', N'', N'ZNIIL')
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'f69baa03-88fa-487b-be3f-df05d18693a1', CAST(N'2024-06-23T00:00:00.000' AS DateTime), CAST(N'2024-12-23T00:00:00.000' AS DateTime), 6, 1, N'60c1211f-635c-4f27-9c57-3e4808bacf8f', N'0913769909', N'Active', N'', NULL)
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [Status], [Note], [CodeWarranty]) VALUES (N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', CAST(N'2024-06-24T00:00:00.000' AS DateTime), CAST(N'2024-12-25T00:00:00.000' AS DateTime), 6, 1, N'9ca4eeb3-8311-4abb-ad88-a64d349731ec', N'024578963', N'Active', N'', NULL)
GO
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'e73045b5-3687-4f30-9c5e-01a4f22e7ed0', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'b1958280-788a-4bd8-95c3-eef953878098', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'd4fa3770-d171-4216-a7b6-0658b0099e30', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'2ffcee25-d9a8-4236-96c8-a27ae08f7ec8', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'f4a29c3e-5370-4974-b917-0791a412116a', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', CAST(N'2024-06-24T08:59:16.197' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'aa5b8812-24d0-408c-890f-0ae2308b2ed8', N'44fea9ad-2fed-4737-a4ba-b4bc67edbc19', N'51142e3c-729c-49b4-b6cf-135d4df06ba5', CAST(N'2024-07-07T22:12:59.160' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'ceebb524-0d92-4d68-a4a2-116aa1ac6a57', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'f3ee84d4-886e-4830-a323-8b16b8fc13ed', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'd4d73abc-d30d-4091-9804-156c7935dade', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'b1958280-788a-4bd8-95c3-eef953878098', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'c39307d8-d2a3-4ed5-9982-2239e455b869', N'dd79d744-930f-4e47-9202-081b9143ec24', N'51142e3c-729c-49b4-b6cf-135d4df06ba5', CAST(N'2024-06-24T08:43:55.133' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'5f803084-77e8-413d-ab02-27df81eba74a', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'9e0db4bd-cfe1-40ed-82f0-7a9e77dc1603', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'4c9b21e5-ed85-4543-b331-32da1f5c98fc', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'83877137-10b8-44d4-abb5-9594f22e52ce', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'1fb07c99-a64e-44d6-bd8d-419240efba2a', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'9e0db4bd-cfe1-40ed-82f0-7a9e77dc1603', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'081aab06-a66e-4ae8-baec-525baa228e22', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'51142e3c-729c-49b4-b6cf-135d4df06ba5', CAST(N'2024-06-24T08:59:16.197' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'd350d83b-4511-4929-b83d-5ea89d2229cd', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'51142e3c-729c-49b4-b6cf-135d4df06ba5', CAST(N'2024-06-24T00:45:00.043' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'14d5b31e-476e-4e47-8171-5ef9bdb8fdd5', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'51142e3c-729c-49b4-b6cf-135d4df06ba5', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'96a861b8-52cd-439f-945f-64da72f3fe85', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'e985512b-dacc-4227-b3fa-6bbdb7ed7de1', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'9e0db4bd-cfe1-40ed-82f0-7a9e77dc1603', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'357a61ea-c478-4ab4-ace1-6ebf3842ba83', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'f3ee84d4-886e-4830-a323-8b16b8fc13ed', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'9d2547cb-e385-4894-8e11-93eefefa4f2a', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'f3ee84d4-886e-4830-a323-8b16b8fc13ed', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'0a3bd41b-ec8b-4d3c-bec7-a6ef6e296d17', N'9a0d2cdf-9212-4ad0-a9fe-e2841c03ca98', N'2ffcee25-d9a8-4236-96c8-a27ae08f7ec8', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'bf4c2f68-1db9-40e0-89b0-b2aaf11133d2', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'363ae4da-d23f-4bad-ad98-d3135b39f913', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'83877137-10b8-44d4-abb5-9594f22e52ce', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'3313496a-2160-4d08-828f-d7637debbc02', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'2ffcee25-d9a8-4236-96c8-a27ae08f7ec8', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'482ada6d-16be-4161-b29f-df22c79554ab', N'1dd3675f-bdcd-44fe-a0de-5e37fbccf90f', N'83877137-10b8-44d4-abb5-9594f22e52ce', CAST(N'2024-06-24T08:59:16.200' AS DateTime))
INSERT [dbo].[WarrantyMappingCondition] ([Id], [WarrantyId], [ConditionWarrantyId], [InsDate]) VALUES (N'78ee675e-0108-4456-b355-edcd8eff63f6', N'f69baa03-88fa-487b-be3f-df05d18693a1', N'b1958280-788a-4bd8-95c3-eef953878098', CAST(N'2024-06-24T00:45:00.060' AS DateTime))
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Discount]  WITH CHECK ADD  CONSTRAINT [FK_Discount_Membership] FOREIGN KEY([MembershipId])
REFERENCES [dbo].[Membership] ([Id])
GO
ALTER TABLE [dbo].[Discount] CHECK CONSTRAINT [FK_Discount_Membership]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_Account] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_Account]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_MemberType] FOREIGN KEY([MemberTypeId])
REFERENCES [dbo].[MemberType] ([Id])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_MemberType]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Discount] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discount] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Discount]
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
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Promotion]
GO
ALTER TABLE [dbo].[ProcessPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProcessPrice_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[ProcessPrice] CHECK CONSTRAINT [FK_ProcessPrice_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Material] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Material]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_SubProducts] FOREIGN KEY([SubId])
REFERENCES [dbo].[SubProducts] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_SubProducts]
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
ALTER TABLE [dbo].[Warranty]  WITH CHECK ADD  CONSTRAINT [FK_Warranty_Order] FOREIGN KEY([OrderDetailId])
REFERENCES [dbo].[OrderDetail] ([Id])
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_Order]
GO
ALTER TABLE [dbo].[WarrantyMappingCondition]  WITH CHECK ADD  CONSTRAINT [FK_WarrantyMappingCondition_ConditionWarranty] FOREIGN KEY([ConditionWarrantyId])
REFERENCES [dbo].[ConditionWarranty] ([Id])
GO
ALTER TABLE [dbo].[WarrantyMappingCondition] CHECK CONSTRAINT [FK_WarrantyMappingCondition_ConditionWarranty]
GO
ALTER TABLE [dbo].[WarrantyMappingCondition]  WITH CHECK ADD  CONSTRAINT [FK_WarrantyMappingCondition_Warranty] FOREIGN KEY([WarrantyId])
REFERENCES [dbo].[Warranty] ([Id])
GO
ALTER TABLE [dbo].[WarrantyMappingCondition] CHECK CONSTRAINT [FK_WarrantyMappingCondition_Warranty]
GO
USE [master]
GO
ALTER DATABASE [JewelrySalesSystem] SET  READ_WRITE 
GO
