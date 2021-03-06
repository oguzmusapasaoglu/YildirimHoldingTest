/****** Object:  Database [YildirimHoldingTest]    Script Date: 29.09.2020 11:40:02 ******/
CREATE DATABASE [YildirimHoldingTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YildirimHoldingTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\YildirimHoldingTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'YildirimHoldingTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\YildirimHoldingTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [YildirimHoldingTest] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YildirimHoldingTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YildirimHoldingTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YildirimHoldingTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YildirimHoldingTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YildirimHoldingTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YildirimHoldingTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YildirimHoldingTest] SET  MULTI_USER 
GO
ALTER DATABASE [YildirimHoldingTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YildirimHoldingTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YildirimHoldingTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YildirimHoldingTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [YildirimHoldingTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [YildirimHoldingTest] SET QUERY_STORE = OFF
GO
/****** Object:  Schema [Customer]    Script Date: 29.09.2020 11:40:02 ******/
CREATE SCHEMA [Customer]
GO
/****** Object:  Schema [Invoice]    Script Date: 29.09.2020 11:40:02 ******/
CREATE SCHEMA [Invoice]
GO
/****** Object:  Schema [Users]    Script Date: 29.09.2020 11:40:02 ******/
CREATE SCHEMA [Users]
GO
/****** Object:  Table [Customer].[CustomerInfo]    Script Date: 29.09.2020 11:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customer].[CustomerInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerNo] [nvarchar](150) NOT NULL,
	[CustomerName] [nvarchar](250) NULL,
	[CutomerCity] [nvarchar](150) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[CreatedUser] [int] NULL,
	[ActivationStatus] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Invoice].[InvoiceHeader]    Script Date: 29.09.2020 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Invoice].[InvoiceHeader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nvarchar](150) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[GrandTotal] [decimal](18, 2) NULL,
	[Currency] [varchar](5) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[CreatedUser] [int] NULL,
	[ActivationStatus] [int] NULL,
 CONSTRAINT [PK_InvoiceHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Users].[UsersInfo]    Script Date: 29.09.2020 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[UsersInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserNo] [nvarchar](150) NOT NULL,
	[UserName] [nvarchar](150) NULL,
	[FullName] [nvarchar](250) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[CreatedUser] [int] NULL,
	[ActivationStatus] [int] NULL,
 CONSTRAINT [PK_UsersInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Customer].[CustomerInfo] ON 

INSERT [Customer].[CustomerInfo] ([Id], [CustomerNo], [CustomerName], [CutomerCity], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (1, N'C0000001', N'Customer 001', N'İstanbul', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [Customer].[CustomerInfo] ([Id], [CustomerNo], [CustomerName], [CutomerCity], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (2, N'C0000002', N'Customer 002', N'İstanbul', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [Customer].[CustomerInfo] ([Id], [CustomerNo], [CustomerName], [CutomerCity], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (3, N'C0000003', N'Customer 003', N'Ankara', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 2, 1)
INSERT [Customer].[CustomerInfo] ([Id], [CustomerNo], [CustomerName], [CutomerCity], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (4, N'C0000004', N'Customer 004', N'İzmir', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 2, 1)
SET IDENTITY_INSERT [Customer].[CustomerInfo] OFF
SET IDENTITY_INSERT [Invoice].[InvoiceHeader] ON 

INSERT [Invoice].[InvoiceHeader] ([Id], [InvoiceNo], [CustomerId], [GrandTotal], [Currency], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (1, N'INV0001', 1, CAST(100.00 AS Decimal(18, 2)), N'TL', CAST(N'2020-09-20T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [Invoice].[InvoiceHeader] ([Id], [InvoiceNo], [CustomerId], [GrandTotal], [Currency], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (2, N'INV0002', 2, CAST(2055.55 AS Decimal(18, 2)), N'USD', CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [Invoice].[InvoiceHeader] ([Id], [InvoiceNo], [CustomerId], [GrandTotal], [Currency], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (3, N'INV0003', 3, CAST(205.46 AS Decimal(18, 2)), N'GBP', CAST(N'2020-10-01T00:00:00.0000000' AS DateTime2), 2, 1)
INSERT [Invoice].[InvoiceHeader] ([Id], [InvoiceNo], [CustomerId], [GrandTotal], [Currency], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (4, N'INV0004', 4, CAST(105.00 AS Decimal(18, 2)), N'EURO', CAST(N'2020-11-01T00:00:00.0000000' AS DateTime2), 2, 1)
SET IDENTITY_INSERT [Invoice].[InvoiceHeader] OFF
SET IDENTITY_INSERT [Users].[UsersInfo] ON 

INSERT [Users].[UsersInfo] ([Id], [UserNo], [UserName], [FullName], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (1, N'USR001', N'User1', N'User One', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [Users].[UsersInfo] ([Id], [UserNo], [UserName], [FullName], [CreatedDate], [CreatedUser], [ActivationStatus]) VALUES (2, N'USR002', N'User2', N'User Two', CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), 1, 1)
SET IDENTITY_INSERT [Users].[UsersInfo] OFF
ALTER TABLE [Invoice].[InvoiceHeader] ADD  CONSTRAINT [DF_InvoiceHeader_ActivationStatus]  DEFAULT ((1)) FOR [ActivationStatus]
GO
ALTER TABLE [Customer].[CustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInfo_UsersInfo] FOREIGN KEY([CreatedUser])
REFERENCES [Users].[UsersInfo] ([Id])
GO
ALTER TABLE [Customer].[CustomerInfo] CHECK CONSTRAINT [FK_CustomerInfo_UsersInfo]
GO
ALTER TABLE [Invoice].[InvoiceHeader]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceHeader_CustomerInfo] FOREIGN KEY([CreatedUser])
REFERENCES [Users].[UsersInfo] ([Id])
GO
ALTER TABLE [Invoice].[InvoiceHeader] CHECK CONSTRAINT [FK_InvoiceHeader_CustomerInfo]
GO
ALTER DATABASE [YildirimHoldingTest] SET  READ_WRITE 
GO
