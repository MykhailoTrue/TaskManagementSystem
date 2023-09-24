USE master;
GO

-- create database with logical name and file name
IF NOT EXISTS 
(
	SELECT name
	FROM sys.databases
	WHERE name = N'TaskManagementSystemDb'
)
CREATE DATABASE TaskManagementSystemDb
	CONTAINMENT = NONE
 ON PRIMARY
(	
	NAME = N'TaskManagementSystemDb',
	FILENAME = N'C:\my-data\programs-data\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskManagementSystemDb.mdf', 
	SIZE = 8192KB,
	MAXSIZE = UNLIMITED,
	FILEGROWTH = 65536KB
)
LOG ON
(	
	NAME = 'TaskManagementSystemDb_log',
	FILENAME = N'C:\my-data\programs-data\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskManagementSystemDb_log.mdf',
	SIZE = 50MB,
	MAXSIZE = 200MB,
	FILEGROWTH = 5MB
);
GO

ALTER AUTHORIZATION ON DATABASE::TaskManagementSystemDb TO sa;
