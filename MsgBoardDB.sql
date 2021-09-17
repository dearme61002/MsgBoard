USE [master]
GO
/****** Object:  Database [MsgBoard]    Script Date: 2021/9/17 下午 02:48:28 ******/
IF exists (select * from sysdatabases where name='MsgBoard')
		drop database MsgBoard
GO

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE MsgBoard
  ON PRIMARY (NAME = N''MsgBoard'', FILENAME = N''' + @device_directory + N'MsgBoard.mdf'')
  LOG ON (NAME = N''MsgBoard_log'',  FILENAME = N''' + @device_directory + N'MsgBoard.ldf'')')
GO

ALTER DATABASE [MsgBoard] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MsgBoard].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MsgBoard] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MsgBoard] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MsgBoard] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MsgBoard] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MsgBoard] SET ARITHABORT OFF 
GO
ALTER DATABASE [MsgBoard] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MsgBoard] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MsgBoard] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MsgBoard] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MsgBoard] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MsgBoard] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MsgBoard] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MsgBoard] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MsgBoard] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MsgBoard] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MsgBoard] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MsgBoard] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MsgBoard] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MsgBoard] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MsgBoard] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MsgBoard] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MsgBoard] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MsgBoard] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MsgBoard] SET  MULTI_USER 
GO
ALTER DATABASE [MsgBoard] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MsgBoard] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MsgBoard] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MsgBoard] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MsgBoard] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MsgBoard] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MsgBoard', N'ON'
GO
ALTER DATABASE [MsgBoard] SET QUERY_STORE = OFF
GO
USE [MsgBoard]
GO
/****** Object:  User [NT AUTHORITY\NETWORK SERVICE]    Script Date: 2021/9/17 下午 02:48:28 ******/
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
/****** Object:  Table [dbo].[Posting]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[ismaincontent] [bit] NOT NULL,
 CONSTRAINT [PK_Posting] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounting]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Account] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Level] [varchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Bucket] [date] NULL,
	[BirthDay] [date] NOT NULL,
 CONSTRAINT [PK_Accounting] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDisplayPost]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDisplayPost] AS
SELECT [dbo].[Posting].[PostID], 
	   [dbo].[Posting].[Title],
	   [dbo].[Accounting].[Name],
	   [dbo].[Posting].[CreateDate],
	   [dbo].[Accounting].[Level],
	   [dbo].[Posting].[ismaincontent]
FROM [dbo].[Posting] 
INNER JOIN [dbo].[Accounting]
ON [dbo].[Posting].[UserID] = [dbo].[Accounting].[UserID]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MsgID] [uniqueidentifier] NOT NULL,
	[PostID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MsgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDisplayMsg]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDisplayMsg] AS
SELECT [dbo].[Message].[PostID],
	   [dbo].[Message].[Body], 
       [dbo].[Accounting].[Name],
	   [dbo].[Message].[CreateDate]	   
FROM [dbo].[Message] 
INNER JOIN [dbo].[Accounting]
ON [dbo].[Message].[UserID] = [dbo].[Accounting].[UserID]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Body] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Info]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RegisteredPeople] [int] NULL,
	[PeopleOnline] [int] NULL,
	[CreateDate] [date] NOT NULL,
 CONSTRAINT [PK_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Swear]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Swear](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Swear] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 2021/9/17 下午 02:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[LogoutDate] [datetime] NULL,
	[IP] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounting] ADD  CONSTRAINT [DF_Accounting_UserID]  DEFAULT (newid()) FOR [UserID]
GO
ALTER TABLE [dbo].[Accounting] ADD  CONSTRAINT [DF_Accounting_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Info] ADD  CONSTRAINT [DF_Info_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_MsgID]  DEFAULT (newid()) FOR [MsgID]
GO
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Posting] ADD  CONSTRAINT [DF_Posting_PostID]  DEFAULT (newid()) FOR [PostID]
GO
ALTER TABLE [dbo].[Posting] ADD  CONSTRAINT [DF_Posting_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Posting] ADD  CONSTRAINT [DF_Posting_ismaincontent]  DEFAULT ((0)) FOR [ismaincontent]
GO
ALTER TABLE [dbo].[UserLogin] ADD  CONSTRAINT [DF_UserLogin_IP]  DEFAULT ('無法取得') FOR [IP]
GO
USE [master]
GO
ALTER DATABASE [MsgBoard] SET  READ_WRITE 
GO

USE [MsgBoard]
GO

INSERT INTO [dbo].[Accounting]
           ([UserID]
           ,[Name]
           ,[Account]
           ,[Password]
           ,[Level]
           ,[Email]
           ,[Bucket]
           ,[BirthDay])
     VALUES
           ('9E481FA6-27AD-41FE-A87E-2A47226E9324'
           ,'系統管理者'
           ,'Admin'
           ,'9a4yLSHeobDruJtcBUNJhA=='
           ,'Admin'
           ,'admin@ubay.com'
           ,NULL
           ,'2021-08-31 00:00:00')
GO
