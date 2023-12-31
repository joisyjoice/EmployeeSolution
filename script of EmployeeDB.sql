USE [master]
GO
/****** Object:  Database [Employee]    Script Date: 6/22/2023 8:46:21 AM ******/
CREATE DATABASE [Employee]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Employee', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Employee.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Employee_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Employee_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Employee].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Employee] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Employee] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Employee] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Employee] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Employee] SET ARITHABORT OFF 
GO
ALTER DATABASE [Employee] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Employee] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Employee] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Employee] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Employee] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Employee] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Employee] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Employee] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Employee] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Employee] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Employee] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Employee] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Employee] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Employee] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Employee] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Employee] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Employee] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Employee] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Employee] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Employee] SET  MULTI_USER 
GO
ALTER DATABASE [Employee] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Employee] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Employee] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Employee] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [Employee]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeByID]    Script Date: 6/22/2023 8:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeeByID]

@EmployeeID int=NULL
AS
BEGIN
 select * from EmployeeInfo where EmployeeIDPK=@EmployeeID


END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeesList]    Script Date: 6/22/2023 8:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeesList]


AS
BEGIN
 select * from EmployeeInfo


END

GO
/****** Object:  StoredProcedure [dbo].[SaveEmployee]    Script Date: 6/22/2023 8:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveEmployee]

@EmployeeIDPK  int = NULL,
@FirstName   varchar(50) = NULL,
@LastName varchar(50)= NULL,
@DepartMent varchar(50)=NULL,
@Msg varchar(100)= NULL OUTPUT	
AS
BEGIN

Declare @NewEmployeeIDPK  int
IF @EmployeeIDPK is null or @EmployeeIDPK =0
BEGIN


---------insert department--------------
SET @NewEmployeeIDPK  =  (select isnull(MAX(EmployeeIDPK),0)+1 from EmployeeInfo)

Insert into EmployeeInfo(EmployeeIDPK,FirstName,LastName,DepartMent) Values (@NewEmployeeIDPK,@FirstName,@LastName,@DepartMent)
	
SET @Msg=@NewEmployeeIDPK 

   END

   ELSE
   BEGIN
   print 'Update Department'

  Update EmployeeInfo set FirstName=@FirstName,LastName=@LastName,DepartMent=@DepartMent where EmployeeIDPK=@EmployeeIDPK 
   SET @Msg=@FirstName

END

END

GO
/****** Object:  Table [dbo].[EmployeeInfo]    Script Date: 6/22/2023 8:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeInfo](
	[EmployeeIDPK] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Department] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeIDPK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeInfo] ON 

INSERT [dbo].[EmployeeInfo] ([EmployeeIDPK], [FirstName], [LastName], [Department], [Username], [Password]) VALUES (3, N'Ann', N'Lit', N'IT', N'Ann', N'test')
SET IDENTITY_INSERT [dbo].[EmployeeInfo] OFF
USE [master]
GO
ALTER DATABASE [Employee] SET  READ_WRITE 
GO
