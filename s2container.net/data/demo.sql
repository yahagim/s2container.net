IF exists(select * from master..sysdatabases where [name]='s2dotnetdemo')
DROP DATABASE [s2dotnetdemo]
GO

CREATE DATABASE [s2dotnetdemo]

GO

use [s2dotnetdemo]
GO

CREATE TABLE [dbo].[DEPT] (
	[DEPTNO] numeric (2, 0) NOT NULL ,
	[DNAME] varchar (14) COLLATE Japanese_CI_AS NULL ,
	[LOC] varchar (13) COLLATE Japanese_CI_AS NULL ,
	[VERSIONNO] numeric (8, 0) NULL ,
	[ACTIVE] numeric (1, 0) NULL ,
	CONSTRAINT [PK_DEPT] PRIMARY KEY CLUSTERED ([DEPTNO]))
GO


CREATE TABLE [dbo].[DEPT2] (
	[DEPTNO] numeric (2, 0) NOT NULL ,
	[DNAME] varchar (14) COLLATE Japanese_CI_AS NULL ,
	[ACTIVE] numeric (1, 0) NULL ,
	CONSTRAINT [PK_DEPT2] PRIMARY KEY CLUSTERED ([DEPTNO]))
GO

CREATE TABLE [dbo].[DUAL] (
	[DUMMY] varchar (1) COLLATE Japanese_CI_AS NULL )
GO

CREATE TABLE [dbo].[EMP] (
	[EMPNO] numeric (4, 0) NOT NULL ,
	[ENAME] varchar (10) COLLATE Japanese_CI_AS NULL ,
	[JOB] varchar (9) COLLATE Japanese_CI_AS NULL ,
	[MGR] numeric (4, 0) NULL ,
	[HIREDATE] datetime NULL ,
	[SAL] decimal (7, 2) NULL ,
	[COMM] numeric (7, 2) NULL ,
	[DEPTNO] numeric (2, 0) NULL ,
	[TSTAMP] datetime NULL ,
	CONSTRAINT [PK_EMP] PRIMARY KEY CLUSTERED ([EMPNO]))
GO

CREATE TABLE [dbo].[EMP2] (
	[EMPNO] numeric (4, 0) NOT NULL ,
	[ENAME] varchar (10) COLLATE Japanese_CI_AS NULL ,
	[DEPTNUM] numeric (2, 0) NULL ,
	CONSTRAINT [PK_EMP2] PRIMARY KEY CLUSTERED ([EMPNO]))
GO

CREATE TABLE [dbo].[IDENTITYTABLE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[NAME] varchar (20) COLLATE Japanese_CI_AS NULL ,
	CONSTRAINT [PK_IDENTITYTABLE] PRIMARY KEY CLUSTERED ([ID]))
GO

CREATE TABLE [dbo].[SEQTABLE] (
	[ID] int NOT NULL ,
	[NAME] varchar (20) COLLATE Japanese_CI_AS NULL ,
	CONSTRAINT [PK_SEQTABLE] PRIMARY KEY CLUSTERED ([ID]))
GO

CREATE TABLE [dbo].[BASICTYPE] (
	[ID] numeric (18, 0) NOT NULL,
	[BOOLTYPE] bit NULL,
	[BYTETYPE] tinyint NULL,
	[SBYTETYPE] numeric (3, 0) NULL,
	[INT16TYPE] smallint NULL,
	[INT32TYPE] int NULL,
	[INT64TYPE] bigint NULL,
	[SINGLETYPE] float NULL,
	[DOUBLETYPE] DOUBLE PRECISION NULL,
	[DECIMALTYPE] decimal (28, 0) NULL,
	[STRINGTYPE] varchar (1024) NULL,
	[DATETIMETYPE] datetime NULL,
	[BINARYTYPE] binary (16) NULL,
	CONSTRAINT pk_basictype PRIMARY KEY NONCLUSTERED ([id])
) 
GO

INSERT INTO [dbo].[EMP] VALUES(7369,'SMITH','CLERK',7902,CONVERT(datetime,'1980-12-17'),800,NULL,20,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7499,'ALLEN','SALESMAN',7698,CONVERT(datetime,'1981-02-20'),1600,300,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7521,'WARD','SALESMAN',7698,CONVERT(datetime,'1981-02-22'),1250,500,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7566,'JONES','MANAGER',7839,CONVERT(datetime,'1981-04-02'),2975,NULL,20,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7654,'MARTIN','SALESMAN',7698,CONVERT(datetime,'1981-09-28'),1250,1400,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7698,'BLAKE','MANAGER',7839,CONVERT(datetime,'1981-05-01'),2850,NULL,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7782,'CLARK','MANAGER',7839,CONVERT(datetime,'1981-06-09'),2450,NULL,10,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7788,'SCOTT','ANALYST',7566,CONVERT(datetime,'1982-12-09'),3000.0,NULL,20,CONVERT(datetime,'2005-01-18 13:09:32.213'))
INSERT INTO [dbo].[EMP] VALUES(7839,'KING','PRESIDENT',NULL,CONVERT(datetime,'1981-11-17'),5000,NULL,10,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7844,'TURNER','SALESMAN',7698,CONVERT(datetime,'1981-09-08'),1500,0,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7876,'ADAMS','CLERK',7788,CONVERT(datetime,'1983-01-12'),1100,NULL,20,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7900,'JAMES','CLERK',7698,CONVERT(datetime,'1981-12-03'),950,NULL,30,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7902,'FORD','ANALYST',7566,CONVERT(datetime,'1981-12-03'),3000,NULL,20,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[EMP] VALUES(7934,'MILLER','CLERK',7782,CONVERT(datetime,'1982-01-23'),1300,NULL,10,CONVERT(datetime,'2000-01-01 00:00:00.0'))
INSERT INTO [dbo].[DEPT] VALUES(10,'ACCOUNTING','NEW YORK',0,1)
INSERT INTO [dbo].[DEPT] VALUES(20,'RESEARCH','DALLAS',0,1)
INSERT INTO [dbo].[DEPT] VALUES(30,'SALES','CHICAGO',0,1)
INSERT INTO [dbo].[DEPT] VALUES(40,'OPERATIONS','BOSTON',0,1)
INSERT INTO [dbo].[EMP2] VALUES(7369,'SMITH',20)
INSERT INTO [dbo].[EMP2] VALUES(7499,'ALLEN',30)
INSERT INTO [dbo].[DEPT2] VALUES(20,'RESEARCH',1)
INSERT INTO [dbo].[DEPT2] VALUES(30,'SALES',0)
INSERT INTO [dbo].[DUAL] VALUES('X')
INSERT INTO [dbo].[BASICTYPE] VALUES (
	1,
	1,
	255,
	-128,
	32767,
	2147483647,
	9223372036854775807,
	9.876543,
	9.87654321098765,
	9999999999999999999999999999,
	'�|\���`',
	CONVERT(datetime, '1980-12-17 12:34:56.123'),
	CONVERT(binary, 0x7D)
);

INSERT INTO [dbo].[BASICTYPE] (
	id
) VALUES (
	2
);
