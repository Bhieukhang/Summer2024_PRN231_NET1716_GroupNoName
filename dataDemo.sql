USE [master]
GO
/****** Object:  Database [JewelrySalesSystem]    Script Date: 5/23/2024 11:53:13 PM ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[DOB] [date] NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[ImgURL] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Deflag] [bit] NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[PricePressure] [float] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionWarranty]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionWarranty](
	[Id] [uniqueidentifier] NOT NULL,
	[Condition] [nvarchar](max) NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_ConditionWarranty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ManagerId] [uniqueidentifier] NOT NULL,
	[PercentDiscount] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[ConditionDiscount] [float] NULL,
	[Status] [nvarchar](50) NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [uniqueidentifier] NOT NULL,
	[MaterialName] [nvarchar](50) NULL,
	[Weight] [float] NULL,
	[InsDate] [datetime] NULL,
	[ProductMaterialId] [uniqueidentifier] NOT NULL,
	[Deflag] [bit] NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[Order]    Script Date: 5/23/2024 11:53:13 PM ******/
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
	[DiscountId] [uniqueidentifier] NOT NULL,
	[TotalPrice] [float] NULL,
	[MaterialProcessPrice] [float] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [float] NULL,
	[Quantity] [int] NULL,
	[Discount] [decimal](18, 0) NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[CaptitalPrice] [float] NULL,
	[Size] [float] NULL,
	[Price] [float] NULL,
	[InsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[UpsDate] [datetime] NULL,
	[Quantity] [int] NULL,
	[Accessory] [float] NULL,
	[ProductMaterialId] [uniqueidentifier] NULL,
	[Code] [char](10) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductConditionGroup]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductConditionGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
	[PromotionId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductConditionGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMaterial]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMaterial](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[MaterialId] [uniqueidentifier] NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_ProductMaterial] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[Id] [uniqueidentifier] NOT NULL,
	[MembershipId] [uniqueidentifier] NOT NULL,
	[PromotionId] [uniqueidentifier] NOT NULL,
	[InsDate] [datetime] NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [uniqueidentifier] NOT NULL,
	[PromotionName] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[ProductQuantity] [int] NULL,
	[Percentage] [int] NULL,
	[InsDate] [datetime] NULL,
	[UpsDate] [datetime] NULL,
	[Deflag] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/23/2024 11:53:13 PM ******/
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
/****** Object:  Table [dbo].[Stall]    Script Date: 5/23/2024 11:53:13 PM ******/
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
/****** Object:  Table [dbo].[Transaction]    Script Date: 5/23/2024 11:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[Currency] [nvarchar](50) NULL,
	[TotalPrice] [float] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warranty]    Script Date: 5/23/2024 11:53:13 PM ******/
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
	[OrderDetailId] [uniqueidentifier] NULL,
	[Phone] [nvarchar](50) NULL,
	[ConditionWarrantyId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Warranty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'567f996e-7639-4bee-9c6e-2fb09cafbef9', N'Tran Ngan', N'090667052 ', CAST(N'2000-04-11' AS Date), N'123', N'ahihi', NULL, N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-50c65a63e629', N'Staff', N'0333888257', CAST(N'1995-05-05' AS Date), N'Staff', N'Staff', NULL, N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'b0e5f7f6-8c0a-4ac3-ad54-50c65a63e6f2', N'Admin User', N'0123456789', CAST(N'1990-01-01' AS Date), N'adminpassword', N'Admin Address', NULL, N'Active', 0, N'0f8fad5b-d9cb-469f-a165-70867728950e', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f6d88', N'Regular User', N'0987654321', CAST(N'1995-05-05' AS Date), N'userpassword', N'User Address', NULL, N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-22T08:45:56.120' AS DateTime), CAST(N'2024-05-22T08:45:56.120' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', N'Staff', N'0333888257', CAST(N'1995-05-05' AS Date), N'Staff', N'Staff', NULL, N'Active', 0, N'7c9e6679-7425-40de-944b-e07fc1f90ae7', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Account] ([Id], [FullName], [Phone], [DOB], [Password], [Address], [ImgURL], [Status], [Deflag], [RoleId], [InsDate], [UpsDate]) VALUES (N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', N'Tran Do', N'0906697053', CAST(N'2000-05-03' AS Date), N'123', N'HCM', NULL, N'Active', 1, N'7c9e6679-7425-40de-944b-e07fc1f90ae9', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-05-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'b2c3d4e5-6789-1011-1213-141516171819', N'Ring', N'Jewelry', N'Ring Category', 0.2)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'c3d4e5f6-7890-1011-1213-141516171819', N'Bracelet', N'Jewelry', N'Bracelet Category', 0.15)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'a1b2c3d4-5678-9101-1121-314151617181', N'Necklace', N'Jewelry', N'Necklace Category', 0.1)
INSERT [dbo].[Category] ([Id], [Name], [Type], [Description], [PricePressure]) VALUES (N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', N'Nhẫn', N'Nhẫn', NULL, 1)
GO
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag]) VALUES (N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', N'Giảm 15% dành cho trang sức thuộc cửa hàng trong trường hợp mua lại', NULL, NULL)
INSERT [dbo].[ConditionWarranty] ([Id], [Condition], [InsDate], [Deflag]) VALUES (N'b1958280-788a-4bd8-95c3-eef953878098', N'Sản phẩm còn nguyên vẹn, không sức mẻ', NULL, NULL)
GO
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'11e12473-f84e-41e8-8db2-67f0e8b92809', N'Tran Ngan', 1, 0, 0, N'567f996e-7639-4bee-9c6e-2fb09cafbef9', 0, 1)
INSERT [dbo].[Membership] ([Id], [Name], [Level], [Point], [RedeemPoint], [UserId], [UsedMoney], [Deflag]) VALUES (N'854a375a-2b82-45b4-9d69-dd789baa6d96', N'Tran Do', 1, 0, 0, N'f8d700a3-1cd8-4a98-89c1-eb27de0ae5a6', 0, 0)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [CaptitalPrice], [Size], [Price], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [Accessory], [ProductMaterialId], [Code]) VALUES (N'd4e5f6a7-8901-1121-3141-516171819202', N'Gold Necklace', N'Gold Necklace Description', 100, 16, 150, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 0, N'a1b2c3d4-5678-9101-1121-314151617181', CAST(N'2024-05-22T08:45:56.120' AS DateTime), 50, 50, NULL, N'PN001     ')
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [CaptitalPrice], [Size], [Price], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [Accessory], [ProductMaterialId], [Code]) VALUES (N'e5f6a7b8-9012-2131-4151-617181920213', N'Silver Ring', N'Silver Ring Description', 50, 10, 80, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 0, N'b2c3d4e5-6789-1011-1213-141516171819', CAST(N'2024-05-22T08:45:56.120' AS DateTime), 100, 30, NULL, N'PR002     ')
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [CaptitalPrice], [Size], [Price], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [Accessory], [ProductMaterialId], [Code]) VALUES (N'f6a7b8c9-0123-3141-5161-718192021314', N'Diamond Bracelet', N'Diamond Bracelet Description', 200, 20, 300, CAST(N'2024-05-22T08:45:56.120' AS DateTime), 0, N'c3d4e5f6-7890-1011-1213-141516171819', CAST(N'2024-05-22T08:45:56.120' AS DateTime), 30, 70, NULL, N'PB003     ')
INSERT [dbo].[Product] ([Id], [ProductName], [Description], [CaptitalPrice], [Size], [Price], [InsDate], [Deflag], [CategoryId], [UpsDate], [Quantity], [Accessory], [ProductMaterialId], [Code]) VALUES (N'9a80df53-9851-42ee-8f7a-95fcdc4a1b80', N'Nhẫn bạc', NULL, NULL, NULL, NULL, NULL, NULL, N'b31fb460-a2e2-4fdc-9413-4ad6fcbd2feb', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'0f8fad5b-d9cb-469f-a165-70867728950e', N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae7', N'Manager')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae8', N'Staff')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (N'7c9e6679-7425-40de-944b-e07fc1f90ae9', N'Customer')
GO
INSERT [dbo].[Stall] ([Id], [Name], [Number], [StaffId], [Deflag]) VALUES (N'4c55bcb1-33a4-4c7c-ab0d-b4672a2f7d22', N'Do Huu Thuan', 12, N'4c55bcb1-33a4-4c7c-a20d-b4672a2f7d89', NULL)
GO
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [ConditionWarrantyId], [Status]) VALUES (N'dab79c31-cbae-4cdc-9b6a-59d54596522b', CAST(N'2024-05-18T23:55:39.677' AS DateTime), CAST(N'2024-05-18T23:55:39.793' AS DateTime), 0, 1, NULL, NULL, N'b1958280-788a-4bd8-95c3-eef953878098', N'Active')
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [ConditionWarrantyId], [Status]) VALUES (N'80523094-de2b-4af6-b63e-9d7726b1be21', CAST(N'2024-05-18T23:55:39.677' AS DateTime), CAST(N'2024-05-18T23:55:39.677' AS DateTime), 3, 1, NULL, NULL, N'b1958280-788a-4bd8-95c3-eef953878098', N'Active')
INSERT [dbo].[Warranty] ([Id], [DateOfPurchase], [ExpirationDate], [Period], [Deflag], [OrderDetailId], [Phone], [ConditionWarrantyId], [Status]) VALUES (N'74ba2a95-d642-462c-9296-afc4c4235658', CAST(N'2024-05-18T23:55:39.677' AS DateTime), CAST(N'2024-05-18T23:55:39.677' AS DateTime), 5, 1, NULL, NULL, N'0d2fa7b5-cb6d-4828-91fd-6cac2fa598b3', N'Active')
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_User_Role]
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
