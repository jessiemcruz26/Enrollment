INSERT INTO [Student] (StudentNumber, FirstName, LastName, Birthday, Address, Email, Mobile, Program, Level)
VALUES ('20221', 'Jessie', 'Cruz', '1995-01-22', 'Toronto', 'jesscruz@gmail.com', '6422720922', 'Electronics', '1')

INSERT INTO [Student] (StudentNumber, FirstName, LastName, Birthday, Address, Email, Mobile, Program, Level)
VALUES ('20222', 'Raymond', 'Balingit', '1990-07-21', 'Tokyo', 'raymond1221@gmail.com', '6552720922', 'Civil', '2')

INSERT INTO [Student] (StudentNumber, FirstName, LastName, Birthday, Address, Email, Mobile, Program, Level)
VALUES ('20223', 'Jonathan', 'Menor', '1985-09-12', 'Mumbai', 'jonathan@yahoo.com', '7862720912', 'Mechanical', '3')

INSERT INTO [Student] (StudentNumber, FirstName, LastName, Birthday, Address, Email, Mobile, Program, Level)
VALUES ('20224', 'Barak', 'Obama', '1989-09-13', 'Los Angeles', 'barak@yahoo.com', '5752720912', 'Electronics', '4')

INSERT INTO [Student] (StudentNumber, FirstName, LastName, Birthday, Address, Email, Mobile, Program, Level)
VALUES ('20225', 'Jamila', 'Smith', '1980-03-13', 'Jakarta', 'jamila@yahoo.com', '5542720913', 'Civil', '4')
--------

INSERT INTO [Instructor] (InstructorNumber, FirstName, LastName, Email, Mobile)
VALUES ('A001', 'Abu', 'Sayaf', 'abu@yahoo.com', '1122720913')

INSERT INTO [Instructor] (InstructorNumber, FirstName, LastName, Email, Mobile)
VALUES ('A002', 'Martha', 'Stewart', 'martha@yahoo.com', '7652720910')

INSERT INTO [Instructor] (InstructorNumber, FirstName, LastName, Email, Mobile)
VALUES ('A003', 'Kendall', 'Jenner', 'kendall@gmail.com', '9563720910')

INSERT INTO [Instructor] (InstructorNumber, FirstName, LastName, Email, Mobile)
VALUES ('A004', 'Robert', 'Downey', 'robert@yahoo.com', '3455679781')

INSERT INTO [Instructor] (InstructorNumber, FirstName, LastName, Email, Mobile)
VALUES ('A005', 'John', 'Legend', 'john21@yahoo.com', '2345679789')

------

INSERT INTO [Course] (CourseName, CourseDescription)
VALUES ('Mathematics', 'Numbers and formulas')

INSERT INTO [Course] (CourseName, CourseDescription)
VALUES ('Programming', 'Instructions to perform')

INSERT INTO [Course] (CourseName, CourseDescription)
VALUES ('Theology', 'The study of God')

INSERT INTO [Course] (CourseName, CourseDescription)
VALUES ('Science', 'Physical and natural world')

INSERT INTO [Course] (CourseName, CourseDescription)
VALUES ('Economics', 'Transfer of wealth')

--------

DECLARE @CourseMathID INT = (
		SELECT CourseID
		FROM Course
		WHERE CourseName = 'Mathematics'
		)
		
DECLARE @CourseProID INT = (
		SELECT CourseID
		FROM Course
		WHERE CourseName = 'Programming'
		)

DECLARE @CourseTheoID INT = (
		SELECT CourseID
		FROM Course
		WHERE CourseName = 'Theology'
		)
		
DECLARE @CourseSciID INT = (
		SELECT CourseID
		FROM Course
		WHERE CourseName = 'Science'
		)
		
DECLARE @CourseEcoID INT = (
		SELECT CourseID
		FROM Course
		WHERE CourseName = 'Economics'
		)

DECLARE @InstructorAbuID INT = (
		SELECT InstructorID
		FROM Instructor
		WHERE InstructorNumber = 'A001'
		)		

DECLARE @InstructorMarID INT = (
		SELECT InstructorID
		FROM Instructor
		WHERE InstructorNumber = 'A002'
		)	

DECLARE @InstructorKenID INT = (
		SELECT InstructorID
		FROM Instructor
		WHERE InstructorNumber = 'A003'
		)	

DECLARE @InstructorRobID INT = (
		SELECT InstructorID
		FROM Instructor
		WHERE InstructorNumber = 'A004'
		)	
		
DECLARE @InstructorJonID INT = (
		SELECT InstructorID
		FROM Instructor
		WHERE InstructorNumber = 'A005'
		)	
		
INSERT INTO [Class] (CourseID, InstructorID, ClassCode, ClassTime, ClassDate, RoomNumber)
VALUES (@CourseMathID, @InstructorAbuID, 'MA112', '9:00am-10:30am','MWF', 'W404')

INSERT INTO [Class] (CourseID, InstructorID, ClassCode, ClassTime, ClassDate, RoomNumber)
VALUES (@CourseProID, @InstructorMarID, 'PR412', '10:30am-12:00nn', 'MWF', 'S210')

INSERT INTO [Class] (CourseID, InstructorID, ClassCode, ClassTime, ClassDate, RoomNumber)
VALUES (@CourseTheoID, @InstructorKenID, 'TH112', '1:30pm-3:00pm', 'TThS', 'N113')

INSERT INTO [Class] (CourseID, InstructorID, ClassCode, ClassTime, ClassDate, RoomNumber)
VALUES (@CourseSciID, @InstructorRobID, 'SC446', '3:00pm-4:30pm', 'TThS', 'SW106')

INSERT INTO [Class] (CourseID, InstructorID, ClassCode, ClassTime, ClassDate, RoomNumber)
VALUES (@CourseEcoID, @InstructorJonID, 'EC651', '9:00am-10:30am', 'TThS', 'W303')