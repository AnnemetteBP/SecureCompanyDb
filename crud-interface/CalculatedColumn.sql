USE SecureDB
GO

ALTER TABLE Department
ADD EmpCount int;
GO

UPDATE Department SET EmpCount = 
	(SELECT COUNT(*) FROM Employee WHERE Dno = DNumber) WHERE DNumber IN 
		(SELECT DNumber FROM Department);

GO

ALTER PROCEDURE usp_CreateDepartment
	@DName nvarchar(50), 
	@MgrSSN int
AS
	DECLARE @NewDNumber int;
	SET @NewDNumber = (
		SELECT MAX(Department.DNumber) FROM Department
	) + 1;
	DECLARE @IfDNameExists int;
	SET @IfDNameExists = (
		SELECT COUNT(Department.DName) FROM Department WHERE Department.DName = @DName
	);
	IF 
	@IfDNameExists=0
	BEGIN
		DECLARE @MgrSSNExists int;
		SET @MgrSSNExists = (
			SELECT COUNT(Department.MgrSSN) FROM Department WHERE Department.MgrSSN = @MgrSSN
		);
		IF @MgrSSNExists=0
		BEGIN
			INSERT INTO Department (DName, DNumber, MgrSSN, MgrStartDate, EmpCount) VALUES(@DName, @NewDNumber, @MgrSSN, GETDATE(), 1);
			SELECT @NewDNumber;
		END
		ELSE
		BEGIN
			PRINT 'The Department MgrSSN already exist.'; 
		END
	END
	ELSE
	BEGIN
		PRINT 'The Department DName already exist.'; 
	END
	RETURN;
GO

ALTER PROCEDURE usp_UpdateDepartmentName
	@DNumber int,
	@DName nvarchar(50)
AS
	DECLARE @DNameExists int;
	SET @DNameExists = (
		SELECT COUNT(*) FROM Department WHERE Department.DName = @DName
	);
	IF
	@DNameExists=0
	BEGIN
		UPDATE Department SET DName = @DName WHERE DNumber = @DNumber;
	END
	ELSE
	BEGIN
		PRINT 'DName already exists.';
	END
GO

ALTER PROCEDURE usp_UpdateDepartmentManager
	@DNumber int,
	@MgrSSN numeric(9, 0)
AS
	DECLARE @MgrSSNExists int;
	SET @MgrSSNExists = (
		SELECT COUNT(*) FROM Department WHERE MgrSSN = @MgrSSN
	);
	IF
	@MgrSSNExists=0
	BEGIN
		DECLARE @OldSuperSSN numeric(9, 0);
		SET @OldSuperSSN = (
			SELECT SuperSSN FROM Employee WHERE Dno = @DNumber
		);
		UPDATE Employee SET SuperSSN = @MgrSSN WHERE SuperSSN = @OldSuperSSN;
		UPDATE Employee SET Dno = @DNumber WHERE SSN = @MgrSSN;
		UPDATE Department SET MgrSSN = @MgrSSN WHERE DNumber = @DNumber;
	END
	ELSE
	BEGIN
		PRINT 'MgrSSN is already MgrSSN';
	END
GO

ALTER PROCEDURE usp_DeleteDepartment
	@DNumber int
AS
	DELETE FROM Dept_Locations WHERE DNUmber = @DNumber;
	DELETE FROM Works_on WHERE Pno = ALL(
		SELECT PNumber FROM Project WHERE DNum = @DNumber
	);
	DELETE FROM Project WHERE DNum = @DNumber;
	UPDATE Employee SET Dno = NULL WHERE Dno = @DNumber;
	DELETE FROM Department WHERE DNumber = @DNumber;
GO

ALTER PROCEDURE usp_GetDepartment
	@DNumber int
AS
	SELECT *, (SELECT COUNT(*) FROM Employee WHERE Dno = @DNumber) AS TotalNumberOfEmployees FROM Department WHERE DNumber = @DNumber;
GO

ALTER PROCEDURE usp_GetAllDepartments
AS
	SELECT DNumber, DName, MgrSSN, MgrStartDate, (SELECT COUNT(*) FROM Employee WHERE Dno = DNumber) AS TotalNumberOfEmployees FROM Department;
GO