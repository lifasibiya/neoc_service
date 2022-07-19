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
	[cust] int not null references [dbo].[cust]([id]),
	[prod] int not null references [dbo].[prod]([id]),
	[lineTotal] money not null,
	[quantity] int not null
)



USE neoc
GO

IF (OBJECT_ID('[dbo].[fn_getLineTotal]') IS NOT NULL)
	DROP FUNCTION [dbo].[fn_getLineTotal]
GO

CREATE FUNCTION [dbo].[fn_getLineTotal](@prod_id int, @quantity int)
	RETURNS MONEY
AS
BEGIN
	RETURN (SELECT [price] * @quantity AS total
			FROM [dbo].[prod]
			WHERE [id] = @prod_id)
END
GO


IF (OBJECT_ID('[dbo].[sp_getAllInvoices]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_getAllInvoices]
GO

CREATE PROC [dbo].[sp_getAllInvoices]
	@id int
AS
SELECT i.[id],i.[date],i.[lineTotal],i.[quantity],
	c.[cust_name] as [name],c.[cust_address] as [address],c.[cust_tel] as [tel],
	p.[desc],p.[price]
	FROM [dbo].[inv] i
	JOIN [dbo].[cust] c
	ON i.cust = c.id
	JOIN [dbo].[prod] p
	ON i.prod = p.id
GO

IF (OBJECT_ID('[dbo].[sp_getInvoice]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_getInvoice]
GO

CREATE PROC [dbo].[sp_getInvoice]
	@id int
AS
SELECT i.[id],i.[date],i.[lineTotal],i.[quantity],
	c.[cust_name] as [name],c.[cust_address] as [address],c.[cust_tel] as [tel],
	p.[desc],p.[price]
	FROM [dbo].[inv] i
	JOIN [dbo].[cust] c
	ON i.cust = c.id
	JOIN [dbo].[prod] p
	ON i.prod = p.id
	WHERE i.[id] = @id
GO


IF (OBJECT_ID('[dbo].[sp_addInvoice]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_addInvoice]
GO

CREATE PROC [dbo].[sp_addInvoice]
	@date date,
	@customer int,
	@product int,
	@quantity int
AS
INSERT INTO [dbo].[inv]([date],[cust],[prod],[quantity],[lineTotal])
VALUES (@date, @customer, @product, @quantity, [dbo].[fn_getLineTotal](@product,@quantity))

SELECT 1
GO


IF (OBJECT_ID('[dbo].[sp_addProduct]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_addProduct]
GO

CREATE PROC [dbo].[sp_addProduct]
	@desc nvarchar(max),
	@price money
AS
INSERT INTO [dbo].[prod]([desc],[price])
VALUES (@desc, @price)

SELECT SCOPE_IDENTITY()
GO



IF (OBJECT_ID('[dbo].[sp_addCustomer]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_addCustomer]
GO

CREATE PROC [dbo].[sp_addCustomer]
	@name nvarchar(100),
	@address nvarchar(max),
	@tel nvarchar(20)
AS
INSERT INTO [dbo].[cust]([cust_name],[cust_address],[cust_tel])
VALUES (@name, @address, @tel)

SELECT SCOPE_IDENTITY()
GO

























