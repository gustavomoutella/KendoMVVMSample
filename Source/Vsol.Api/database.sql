USE [master]
GO
/****** Object:  Database [VSol]    Script Date: 22/12/2017 01:33:59 ******/
CREATE DATABASE [VSol]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VSol', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\VSol.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VSol_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\VSol_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VSol] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VSol].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VSol] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VSol] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VSol] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VSol] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VSol] SET ARITHABORT OFF 
GO
ALTER DATABASE [VSol] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VSol] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VSol] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VSol] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VSol] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VSol] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VSol] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VSol] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VSol] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VSol] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VSol] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VSol] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VSol] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VSol] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [VSol] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VSol] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [VSol] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VSol] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VSol] SET  MULTI_USER 
GO
ALTER DATABASE [VSol] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VSol] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VSol] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VSol] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VSol] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VSol] SET QUERY_STORE = ON
GO
ALTER DATABASE [VSol] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [VSol]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [VSol]
GO
/****** Object:  Schema [AppConfiguration]    Script Date: 22/12/2017 01:33:59 ******/
CREATE SCHEMA [AppConfiguration]
GO
/****** Object:  Schema [AppNavigation]    Script Date: 22/12/2017 01:33:59 ******/
CREATE SCHEMA [AppNavigation]
GO
/****** Object:  Schema [AppSecurity]    Script Date: 22/12/2017 01:33:59 ******/
CREATE SCHEMA [AppSecurity]
GO
/****** Object:  Schema [Integration]    Script Date: 22/12/2017 01:33:59 ******/
CREATE SCHEMA [Integration]
GO
/****** Object:  Schema [JForceSap]    Script Date: 22/12/2017 01:33:59 ******/
CREATE SCHEMA [JForceSap]
GO
/****** Object:  View [dbo].[V_PBI_Produtos]    Script Date: 22/12/2017 01:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[V_PBI_Produtos]
as
select gp.Descricao as Grupo, p.Descricao as Produto,
		isnull(sum(od.ValorVenda),0.0) as ValorTotal		
from [dbo].produto p inner join [dbo].OrdemDetalhe od
						on p.Id = od.IdProduto
						inner join [dbo].GrupoProduto gp
						on p.IdGrupoProduto = gp.Id

group by gp.Descricao, p.Descricao

GO
/****** Object:  Table [AppSecurity].[Authorization]    Script Date: 22/12/2017 01:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AppSecurity].[Authorization](
	[IdAuthorization] [uniqueidentifier] NOT NULL,
	[Authorized] [bit] NOT NULL,
	[IdFeature] [uniqueidentifier] NOT NULL,
	[IdRole] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Authorization] PRIMARY KEY CLUSTERED 
(
	[IdAuthorization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AppSecurity].[Feature]    Script Date: 22/12/2017 01:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AppSecurity].[Feature](
	[IdFeature] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](3000) NULL,
	[FeatureKey] [nvarchar](200) NULL,
	[FeatureName] [nvarchar](200) NOT NULL,
	[IdFeatureParent] [uniqueidentifier] NULL,
	[RecursiveName] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED 
(
	[IdFeature] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AppSecurity].[Role]    Script Date: 22/12/2017 01:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AppSecurity].[Role](
	[IdRole] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](3000) NULL,
	[Enabled] [bit] NOT NULL,
	[RoleName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AppSecurity].[User]    Script Date: 22/12/2017 01:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AppSecurity].[User](
	[IdUser] [uniqueidentifier] NOT NULL,
	[Blocked] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[InvalidLogonAmount] [int] NOT NULL,
	[LastActionDate] [datetime2](7) NULL,
	[LastName] [nvarchar](200) NULL,
	[LogonDate] [datetime2](7) NULL,
	[Password] [nvarchar](2000) NULL,
	[SecurityKey] [nvarchar](400) NULL,
	[Username] [nvarchar](100) NOT NULL,
	[IdPessoa] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AppSecurity].[UserInRole]    Script Date: 22/12/2017 01:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AppSecurity].[UserInRole](
	[IdRole] [uniqueidentifier] NOT NULL,
	[IdUser] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserInRole] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC,
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/12/2017 01:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](250) NOT NULL,
	[UnitPrice] [decimal](12, 2) NULL,
	[UnitsInStock] [decimal](12, 3) NULL,
	[Discontinued] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [AppSecurity].[User] ([IdUser], [Blocked], [CreationDate], [Email], [EmailConfirmed], [Enabled], [FirstName], [InvalidLogonAmount], [LastActionDate], [LastName], [LogonDate], [Password], [SecurityKey], [Username], [IdPessoa]) VALUES (N'4ed0f619-5790-4297-aa5f-f7e7fa20a478', 0, CAST(N'2017-11-20T17:04:13.2100000' AS DateTime2), N'gustavo.moutella@gmail.com', 1, 1, N'Administrator', 0, CAST(N'2017-12-19T10:28:13.2766667' AS DateTime2), NULL, CAST(N'2017-12-19T10:28:13.2766667' AS DateTime2), N'i2ZRo5ROBZjLIlIP/qIBmQrMU7gBmBmyQc1J+h5Qj4U=', NULL, N'admin', NULL)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [UnitPrice], [UnitsInStock], [Discontinued]) VALUES (N'c75eafee-fb77-42f5-f9ef-08d548e93ddd', N'Gustavo', CAST(111.00 AS Decimal(12, 2)), CAST(222.000 AS Decimal(12, 3)), 0)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [UnitPrice], [UnitsInStock], [Discontinued]) VALUES (N'328e65b9-dc5c-4dd6-b8a4-08d548ea9ebc', N'Novo Teste1', CAST(23.00 AS Decimal(12, 2)), CAST(32.000 AS Decimal(12, 3)), 0)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [UnitPrice], [UnitsInStock], [Discontinued]) VALUES (N'57256ed8-1ae7-44d6-7498-08d548ec823a', N'Gustavo2', CAST(1.00 AS Decimal(12, 2)), CAST(2.000 AS Decimal(12, 3)), 0)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [UnitPrice], [UnitsInStock], [Discontinued]) VALUES (N'285e5ab3-44fb-4c82-b0ab-545b3397c797', N'Product 1', CAST(10.00 AS Decimal(12, 2)), CAST(1.000 AS Decimal(12, 3)), 0)
GO
INSERT [dbo].[Product] ([Id], [ProductName], [UnitPrice], [UnitsInStock], [Discontinued]) VALUES (N'd7b05102-fac2-40e3-b25f-dc60823003a8', N'Product 2', CAST(20.00 AS Decimal(12, 2)), CAST(11.000 AS Decimal(12, 3)), 0)
GO
/****** Object:  Index [IX_Authorization_IdFeature]    Script Date: 22/12/2017 01:34:00 ******/
CREATE NONCLUSTERED INDEX [IX_Authorization_IdFeature] ON [AppSecurity].[Authorization]
(
	[IdFeature] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Authorization_IdRole]    Script Date: 22/12/2017 01:34:00 ******/
CREATE NONCLUSTERED INDEX [IX_Authorization_IdRole] ON [AppSecurity].[Authorization]
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feature_IdFeatureParent]    Script Date: 22/12/2017 01:34:00 ******/
CREATE NONCLUSTERED INDEX [IX_Feature_IdFeatureParent] ON [AppSecurity].[Feature]
(
	[IdFeatureParent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserInRole_IdUser]    Script Date: 22/12/2017 01:34:00 ******/
CREATE NONCLUSTERED INDEX [IX_UserInRole_IdUser] ON [AppSecurity].[UserInRole]
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [AppSecurity].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Feature_IdFeature] FOREIGN KEY([IdFeature])
REFERENCES [AppSecurity].[Feature] ([IdFeature])
GO
ALTER TABLE [AppSecurity].[Authorization] CHECK CONSTRAINT [FK_Authorization_Feature_IdFeature]
GO
ALTER TABLE [AppSecurity].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Role_IdRole] FOREIGN KEY([IdRole])
REFERENCES [AppSecurity].[Role] ([IdRole])
GO
ALTER TABLE [AppSecurity].[Authorization] CHECK CONSTRAINT [FK_Authorization_Role_IdRole]
GO
ALTER TABLE [AppSecurity].[Feature]  WITH CHECK ADD  CONSTRAINT [FK_Feature_Feature_IdFeatureParent] FOREIGN KEY([IdFeatureParent])
REFERENCES [AppSecurity].[Feature] ([IdFeature])
GO
ALTER TABLE [AppSecurity].[Feature] CHECK CONSTRAINT [FK_Feature_Feature_IdFeatureParent]
GO
ALTER TABLE [AppSecurity].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_Role_IdRole] FOREIGN KEY([IdRole])
REFERENCES [AppSecurity].[Role] ([IdRole])
GO
ALTER TABLE [AppSecurity].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_Role_IdRole]
GO
ALTER TABLE [AppSecurity].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_User_IdUser] FOREIGN KEY([IdUser])
REFERENCES [AppSecurity].[User] ([IdUser])
GO
ALTER TABLE [AppSecurity].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_User_IdUser]
GO
USE [master]
GO
ALTER DATABASE [VSol] SET  READ_WRITE 
GO
