Create Database Enrollment;
GO

USE Enrollment;

--1
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Student')
 BEGIN
	CREATE TABLE Student
	(
		 StudentID 				INT	IDENTITY(1,1)
		,StudentNumber			VARCHAR(50)   UNIQUE NOT NULL
		,FirstName				VARCHAR(50)   NOT NULL	
		,LastName				VARCHAR(50)   NOT NULL		
		,Birthday				[datetime] 	  NULL
		,Address				VARCHAR(50)   NULL	
		,Email					VARCHAR(50)   NULL	
		,Mobile 				VARCHAR(50)   NULL	
		,Program 				VARCHAR(50)   NULL	
		,Level					VARCHAR(50)   NULL	
	)
 END
GO

IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Student'))
 BEGIN
	ALTER TABLE dbo.Student
	ADD CONSTRAINT StudentID_PK
	PRIMARY KEY CLUSTERED (StudentID)
 END
GO

--2
--Instructor
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Instructor' and xtype='U')
 BEGIN
	CREATE TABLE Instructor
	(
		 InstructorID 			INT			 IDENTITY(1,1)
		,InstructorNumber		VARCHAR(50)   UNIQUE NOT NULL
		,FirstName		 		VARCHAR(50)  NOT NULL	
		,LastName				VARCHAR(50)  NOT NULL	 			
		,Email					VARCHAR(50)  NULL
		,Mobile					VARCHAR(50)  NULL	
	)
 END
GO

IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Instructor'))
 BEGIN
	ALTER TABLE dbo.Instructor
	ADD CONSTRAINT InstructorID_PK
	PRIMARY KEY CLUSTERED (InstructorID)
 END
GO

--3 Course
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Course' and xtype='U')
 BEGIN
	CREATE TABLE Course
	(
		 CourseID 			INT			  IDENTITY(1,1)
		,CourseName			VARCHAR(50)   NULL
		,CourseDescription  VARCHAR(50)   NULL 				
	)
 END
GO

IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Course'))
 BEGIN
	ALTER TABLE dbo.Course
	ADD CONSTRAINT CourseID_PK
	PRIMARY KEY CLUSTERED (CourseID)
 END
GO

--4 Class
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Class' and xtype='U')
 BEGIN
	CREATE TABLE [Class]
	(
		 ClassID 			INT		IDENTITY(1,1) PRIMARY KEY
		,CourseID 			INT 	FOREIGN KEY REFERENCES [Course] (CourseID)
		,InstructorID 		INT 	FOREIGN KEY REFERENCES [Instructor] (InstructorID)	
		,ClassTime			VARCHAR(50)  NULL	 		
		,ClassDate			VARCHAR(50)  NULL
		,RoomNumber			VARCHAR(50)  NULL			
	)
 END
GO


--5 StudentClass
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StudentClass' and xtype='U')
 BEGIN
	CREATE TABLE [StudentClass]
	(
		 StudentClassID 	INT		IDENTITY(1,1) PRIMARY KEY
		,StudentID		 	INT 	NOT NULL FOREIGN KEY REFERENCES [Student] (StudentID)	
		,ClassID			INT 	NOT NULL FOREIGN KEY REFERENCES [Class] (ClassID)		
			
	)
 END
GO

