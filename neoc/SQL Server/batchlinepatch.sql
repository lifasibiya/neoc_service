USE MASTER
GO


IF EXISTS (SELECT * FROM sys.databases 
			WHERE NAME = 'neoc')
	DROP DATABASE neoc
GO


CREATE DATABASE neoc
ON PRIMARY
(
	NAME = 'neoc_data',
	FILENAME = 'C:\SQL Server\neoc.mdf',
	SIZE = 5MB,
	FILEGROWTH = 5%
)
LOG ON
(
	NAME = 'neoc_log',
	FILENAME = 'C:\SQL Server\neoc.ldf',
	SIZE = 5MB,
	FILEGROWTH = 5%
)



USE neoc
GO



IF (OBJECT_ID('[dbo].[inv]') IS NOT NULL)
	DROP TABLE [dbo].[inv]
GO



IF (OBJECT_ID('[dbo].[prod]') IS NOT NULL)
	DROP TABLE [dbo].[prod]
GO



IF (OBJECT_ID('[dbo].[cust]') IS NOT NULL)
	DROP TABLE [dbo].[cust]
GO


CREATE TABLE [dbo].[cust]
(
	[id] int not null primary key identity(1,1),
	[cust_name] nvarchar(100) not null,
	[cust_address] nvarchar(max) not null,
	[cust_tel] nvarchar(20) not null,
)

CREATE TABLE [dbo].[prod]
(
	[id] int not null primary key identity(1,1),
	[desc] nvarchar(max) not null,
	[price] money not null
)

CREATE TABLE [dbo].[inv]
(
	[id] int not null primary key identity(1,1),
	[date] date not null,
	[prod] int not null references [dbo].[prod]([id]),
	[line_total] money not null
)




















