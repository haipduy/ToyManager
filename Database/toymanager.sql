USE [master]
GO
/****** Object:  Database [ToyManagement]    Script Date: 20/10/2018 16:16:46 ******/
CREATE DATABASE [ToyManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToyManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ToyManagement.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ToyManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ToyManagement_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ToyManagement] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToyManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToyManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToyManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToyManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToyManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToyManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToyManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToyManagement] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ToyManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToyManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToyManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToyManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToyManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToyManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToyManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToyManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToyManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToyManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToyManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToyManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToyManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToyManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToyManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToyManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToyManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ToyManagement] SET  MULTI_USER 
GO
ALTER DATABASE [ToyManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToyManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToyManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToyManagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ToyManagement]
GO
/****** Object:  Table [dbo].[TblAccount]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblAccount](
	[UserID] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [int] NULL,
	[RoleID] [int] NULL,
	[StatusAcc] [bit] NULL,
 CONSTRAINT [PK_TblAccount] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblCategory]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCategory](
	[CategoryID] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblItems]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblItems](
	[ItemID] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[Material] [nvarchar](50) NULL,
	[Weight] [nvarchar](50) NULL,
	[QuanlityID] [nvarchar](50) NULL,
	[SupplierID] [nvarchar](50) NULL,
	[CategoryID] [nvarchar](50) NULL,
	[Quantity] [nchar](10) NULL,
	[Description] [nchar](10) NULL,
	[StatusItemID] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblItems] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblOrder]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblOrder](
	[OrderID] [nvarchar](50) NOT NULL,
	[UserID] [nvarchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[BuyDate] [date] NULL,
	[StatusOrder] [bit] NULL,
 CONSTRAINT [PK_TblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblQuanlity]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblQuanlity](
	[QualityID] [nvarchar](50) NOT NULL,
	[QualityName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblQuanlity] PRIMARY KEY CLUSTERED 
(
	[QualityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblRole]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblRole](
	[RoleID] [int] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblStatus]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblStatus](
	[StatusItemID] [nvarchar](50) NOT NULL,
	[StatusItemName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblStatus] PRIMARY KEY CLUSTERED 
(
	[StatusItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblSupplier]    Script Date: 20/10/2018 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSupplier](
	[SupplierID] [nvarchar](50) NOT NULL,
	[SupplierName] [nvarchar](50) NULL,
	[AddressItem] [nvarchar](50) NULL,
	[Phone] [int] NULL,
 CONSTRAINT [PK_TblSupplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [ToyManagement] SET  READ_WRITE 
GO
