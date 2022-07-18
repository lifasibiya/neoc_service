use MASTER
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

