CREATE TABLE Marks (
    GroupID int,
	FirstName varchar(255),
    LastName varchar(255),
	Lab1 float,
	Lab2 float,
	Lab3 float,
);

INSERT INTO Marks(GroupID, FirstName, LastName, Lab1, Lab2, Lab3)
VALUES (0, 'Oles', 'Melnyk', 5, 5, 4)
INSERT INTO Marks(GroupID, FirstName, LastName, Lab1, Lab2, Lab3)
VALUES (0, 'Barry', 'Allen', 5, 5, 5)
INSERT INTO Marks(GroupID, FirstName, LastName, Lab1, Lab2, Lab3)
VALUES (0, 'Dwayne',  'Johnson', 4, 4, 5)
/*INSERT INTO Marks(GroupID, FirstName, LastName, Lab1, Lab2, Lab3)
VALUES (0, 'Bob',  'Johnson', -4, 4, -5)*/
GO;

--DROP PROCEDURE SelectAvg;

CREATE PROCEDURE SelectAvg @FirstName varchar(255), @Lastname varchar(255)
AS
DECLARE @ReturnValue numeric(20,10)
SELECT @ReturnValue = (Lab1 + Lab2 + Lab3) / 3 FROM Marks Where @FirstName = FirstName and @Lastname = LastName;
RETURN @ReturnValue

GO;

/*DECLARE @dwayne numeric(20,10);
EXEC @dwayne = SelectAvg @FirstName = 'Dwayne', @Lastname = 'Johnson'
PRINT @dwayne*/

--SELECT COUNT(FirstName) as count FROM Marks WHERE Lab1 < 0 or Lab2 < 0 or Lab3 < 0;

EXEC tSQLt.NewTestClass 'testMarksTable';
GO

CREATE PROCEDURE testMarksTable.[test that check marks]
AS
BEGIN

	DECLARE @actual int;
	SET @actual = (SELECT COUNT(FirstName) as count FROM Marks WHERE Lab1 < 0 or Lab2 < 0 or Lab3 < 0);

    EXEC tSQLt.AssertNotEquals 0, @actual;

END;
GO

--DROP PROCEDURE testMarksTable.[test check if table have data]

CREATE PROCEDURE testMarksTable.[test check if table have data]
AS
BEGIN

	DECLARE @actual int;
	SET @actual = (SELECT COUNT(FirstName) as count FROM Marks WHERE GroupId IS NOT NULL
	AND FirstName IS NOT NULL
	AND LastName IS NOT NULL
	AND Lab1 IS NOT NULL
	AND Lab2 IS NOT NULL
	AND Lab3 IS NOT NULL)

    EXEC tSQLt.AssertNotEquals 0, @actual;;
END;
GO

CREATE PROCEDURE testMarksTable.[test check if avg calculate right]
AS
BEGIN

	DECLARE @actual numeric(20,10);
	SET @actual = 4.0000000000;
	DECLARE @expected numeric(20,10);

	EXEC @expected = SelectAvg @FirstName = 'Dwayne', @Lastname = 'Johnson';

    EXEC tSQLt.AssertEquals @expected, @actual;;
END;
GO



EXEC tSQLt.RunAll