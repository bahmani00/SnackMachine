USE [master]
GO
/****** Object:  Database [SnackMachineDb]    Script Date: 4/7/2020 9:58:24 PM ******/
CREATE DATABASE [SnackMachineDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SnackMachineDb', FILENAME = N'D:\Development\Database\SQL Server\DATA\SnackMachineDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SnackMachineDb_log', FILENAME = N'D:\Development\Database\SQL Server\Logs\SnackMachineDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SnackMachineDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SnackMachineDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SnackMachineDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SnackMachineDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SnackMachineDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SnackMachineDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SnackMachineDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [SnackMachineDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SnackMachineDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SnackMachineDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SnackMachineDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SnackMachineDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SnackMachineDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SnackMachineDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SnackMachineDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SnackMachineDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SnackMachineDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SnackMachineDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SnackMachineDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SnackMachineDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SnackMachineDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SnackMachineDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SnackMachineDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SnackMachineDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SnackMachineDb] SET RECOVERY FULL 
GO
ALTER DATABASE [SnackMachineDb] SET  MULTI_USER 
GO
ALTER DATABASE [SnackMachineDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SnackMachineDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SnackMachineDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SnackMachineDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SnackMachineDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SnackMachineDb', N'ON'
GO
ALTER DATABASE [SnackMachineDb] SET QUERY_STORE = OFF
GO
USE [SnackMachineDb]
GO
/****** Object:  Table [dbo].[Atm]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Atm](
	[AtmId] [bigint] NOT NULL,
	[MoneyCharged] [decimal](19, 5) NOT NULL,
	[OneCentCount] [int] NOT NULL,
	[TenCentCount] [int] NOT NULL,
	[QuarterCount] [int] NOT NULL,
	[OneDollarCount] [int] NOT NULL,
	[FiveDollarCount] [int] NOT NULL,
	[TwentyDollarCount] [int] NOT NULL,
	[Amount]  AS ((((([OneCentCount]*(0.01)+[TenCentCount]*(0.1))+[QuarterCount]*(0.25))+[OneDollarCount])+[FiveDollarCount]*(5))+[TwentyDollarCount]*(20)),
 CONSTRAINT [PK_AtmId] PRIMARY KEY CLUSTERED 
(
	[AtmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HeadOffice]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HeadOffice](
	[HeadOfficeId] [bigint] NOT NULL,
	[Balance] [decimal](19, 5) NOT NULL,
	[OneCentCount] [int] NOT NULL,
	[TenCentCount] [int] NOT NULL,
	[QuarterCount] [int] NOT NULL,
	[OneDollarCount] [int] NOT NULL,
	[FiveDollarCount] [int] NOT NULL,
	[TwentyDollarCount] [int] NOT NULL,
	[Amount]  AS ((((([OneCentCount]*(0.01)+[TenCentCount]*(0.1))+[QuarterCount]*(0.25))+[OneDollarCount])+[FiveDollarCount]*(5))+[TwentyDollarCount]*(20)),
 CONSTRAINT [PK_HeadOfficeId] PRIMARY KEY CLUSTERED 
(
	[HeadOfficeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ids]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ids](
	[Id] [nvarchar](255) NOT NULL,
	[NextHigh] [bigint] NULL,
 CONSTRAINT [PK_Ids] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slot](
	[SlotId] [bigint] NOT NULL,
	[Position] [int] NOT NULL,
	[SnackMachineId] [bigint] NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](19, 5) NOT NULL,
	[SnackId] [bigint] NULL,
 CONSTRAINT [PK_SlotId] PRIMARY KEY CLUSTERED 
(
	[SlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Snack]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snack](
	[SnackId] [bigint] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_SnackId] PRIMARY KEY CLUSTERED 
(
	[SnackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SnackMachine]    Script Date: 4/7/2020 9:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SnackMachine](
	[SnackMachineId] [bigint] NOT NULL,
	[OneCentCount] [int] NOT NULL,
	[TenCentCount] [int] NOT NULL,
	[QuarterCount] [int] NOT NULL,
	[OneDollarCount] [int] NOT NULL,
	[FiveDollarCount] [int] NOT NULL,
	[TwentyDollarCount] [int] NOT NULL,
	[Amount]  AS ((((([OneCentCount]*(0.01)+[TenCentCount]*(0.1))+[QuarterCount]*(0.25))+[OneDollarCount])+[FiveDollarCount]*(5))+[TwentyDollarCount]*(20)),
 CONSTRAINT [PK_SnackMachineId] PRIMARY KEY CLUSTERED 
(
	[SnackMachineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--- Begin Insert -------------------------------------------------------------------
INSERT [dbo].[Ids] ([Id], [NextHigh]) VALUES (N'SlotId', 3)
INSERT [dbo].[Ids] ([Id], [NextHigh]) VALUES (N'SnackId', 3)
INSERT [dbo].[Ids] ([Id], [NextHigh]) VALUES (N'SnackMachineId', 1)
INSERT [dbo].[Ids] ([Id], [NextHigh]) VALUES (N'AtmId', 1)
INSERT [dbo].[Ids] ([Id], [NextHigh]) VALUES (N'HeadOfficeId', 1)

INSERT [dbo].[Snack] ([SnackId], [Name]) VALUES (1, N'Chocolate')
INSERT [dbo].[Snack] ([SnackId], [Name]) VALUES (2, N'Soda')
INSERT [dbo].[Snack] ([SnackId], [Name]) VALUES (3, N'Gum')

INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (1, 1, 1, 16, CAST(3.00000 AS Decimal(19, 5)), 1)
INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (2, 2, 1, 15, CAST(2.00000 AS Decimal(19, 5)), 2)
INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (3, 3, 1, 10, CAST(1.00000 AS Decimal(19, 5)), 3)

INSERT [dbo].[SnackMachine] ([SnackMachineId], [OneCentCount], [TenCentCount], [QuarterCount], [OneDollarCount], [FiveDollarCount], [TwentyDollarCount]) VALUES (1, 4, 2, 2, 13, 1, 2)

INSERT INTO [dbo].[Atm] ([AtmId], [MoneyCharged], [OneCentCount], [TenCentCount], [QuarterCount], [OneDollarCount], [FiveDollarCount], [TwentyDollarCount]) VALUES (1, 0, 100, 100, 100, 100, 100, 100)

INSERT INTO [dbo].[HeadOffice] ([HeadOfficeId], [MoneyCharged], [OneCentCount], [TenCentCount], [QuarterCount], [OneDollarCount], [FiveDollarCount], [TwentyDollarCount]) VALUES (1, 110, 20, 20, 20, 20, 20, 20)
GO
--- End Insert -------------------------------------------------------------------

ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_SnackId_SlotId] FOREIGN KEY([SnackId])
REFERENCES [dbo].[Snack] ([SnackId])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_SnackId_SlotId]
GO
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_SnackMachineId_SlotId] FOREIGN KEY([SnackMachineId])
REFERENCES [dbo].[SnackMachine] ([SnackMachineId])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_SnackMachineId_SlotId]
GO

--- Upgrade 1-------------------------------------------------------------------
ALTER TABLE [dbo].[Snack]
	ADD [ImageWidth] [int] DEFAULT 70;

UPDATE [dbo].[Snack] SET ImageWidth = 120 WHERE [SnackId] = 1
UPDATE [dbo].[Snack] SET ImageWidth = 70 WHERE [SnackId] = 2
UPDATE [dbo].[Snack] SET ImageWidth = 70 WHERE [SnackId] = 3
ALTER TABLE [dbo].[Snack] 
	ALTER COLUMN ImageWidth int NOT NULL;

UPDATE [dbo].[Ids] SET [NextHigh] = 2 WHERE [Id] = N'SnackMachineId'
INSERT [dbo].[SnackMachine] ([SnackMachineId], [OneCentCount], [TenCentCount], [QuarterCount], [OneDollarCount], [FiveDollarCount], [TwentyDollarCount]) VALUES (2, 40, 20, 20, 130, 10, 20)

UPDATE [dbo].[Snack] SET [NextHigh] = 6 WHERE [Id] = N'SnackId'
INSERT [dbo].[Snack] ([SnackId], [Name], ImageWidth) VALUES (4, N'Water', 70)
INSERT [dbo].[Snack] ([SnackId], [Name], ImageWidth) VALUES (5, N'Pepsi', 70)
INSERT [dbo].[Snack] ([SnackId], [Name], ImageWidth) VALUES (6, N'Soda', 70)

UPDATE [dbo].[Snack] SET [NextHigh] = 6 WHERE [Id] = N'SlotId'
INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (4, 1, 2, 10, CAST(1.00000 AS Decimal(19, 5)), 4)
INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (5, 2, 2, 10, CAST(1.00000 AS Decimal(19, 5)), 5)
INSERT [dbo].[Slot] ([SlotId], [Position], [SnackMachineId], [Quantity], [Price], [SnackId]) VALUES (6, 3, 2, 10, CAST(1.00000 AS Decimal(19, 5)), 6)

--- Upgrade -------------------------------------------------------------------


USE [master]
GO
ALTER DATABASE [SnackMachineDb] SET  READ_WRITE 
GO
