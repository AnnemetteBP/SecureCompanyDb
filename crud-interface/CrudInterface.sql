USE [SecureDB]
GO

DROP PROCEDURE IF EXISTS usp_CreateDepartment
GO

CREATE PROCEDURE usp_CreateDepartment
	@DName nvarchar(50), 
	@MgrSSN int
AS
	DECLARE @NewDNumber int;
	SET @NewDNumber = (
		SELECT COALESCE(MAX(Department.DNumber) + 1, 1) FROM Department
	);
	IF EXISTS (SELECT 1 FROM Department WHERE Department.DName = @DName)
        throw 51000, 'department name already exist', 1
	ELSE
		DECLARE @MgrSSNExists int;
		SET @MgrSSNExists = (
			SELECT COUNT(Department.MgrSSN) FROM Department WHERE Department.MgrSSN = @MgrSSN
		);
		IF EXISTS (SELECT 1 FROM Department WHERE Department.MgrSSN = @MgrSSN)
			throw 51000, 'department name already exist', 1
		ELSE
		BEGIN
			INSERT INTO Department (DName, DNumber, MgrSSN, MgrStartDate) VALUES(@DName, @NewDNumber, @MgrSSN, GETDATE());
			SELECT @NewDNumber;
		END
	RETURN;
GO

DROP PROCEDURE IF EXISTS usp_UpdateDepartmentName
GO

CREATE PROCEDURE usp_UpdateDepartmentName
	@DNumber int,
	@DName nvarchar(50)
AS
	IF EXISTS (SELECT 1 FROM Department WHERE Department.DName = @DName)
        throw 51000, 'department name already exist', 1
	ELSE
	BEGIN
		UPDATE Department SET DName = @DName WHERE DNumber = @DNumber;
	END
GO

DROP PROCEDURE IF EXISTS usp_UpdateDepartmentManager
GO

CREATE PROCEDURE usp_UpdateDepartmentManager
	@DNumber int,
	@MgrSSN numeric(9, 0)
AS
	IF EXISTS (SELECT 1 FROM Department WHERE Department.MgrSSN = @MgrSSN)
        throw 51000, 'MgrSSN is already MgrSSN', 1
	ELSE
	BEGIN
		UPDATE Employee SET SuperSSN = @MgrSSN WHERE Dno = @DNumber AND SSN <> @MgrSSN;
		UPDATE Department SET MgrSSN = @MgrSSN WHERE DNumber = @DNumber;
	END
GO

DROP PROCEDURE IF EXISTS usp_DeleteDepartment
GO

CREATE PROCEDURE usp_DeleteDepartment
	@DNumber int
AS
	DELETE FROM Dept_Locations WHERE DNUmber = @DNumber;
	UPDATE Employee SET Dno = NULL WHERE Dno = @DNumber;

	DECLARE @Pno int;
	DECLARE p_cursor CURSOR  
    FOR SELECT PNumber FROM Project WHERE DNum = @DNumber
	OPEN p_cursor  
	FETCH NEXT FROM p_cursor INTO @Pno;  
	WHILE @@FETCH_STATUS = 0  
	BEGIN   
		DELETE FROM Works_on WHERE Pno = @Pno;
	FETCH NEXT FROM p_cursor INTO @Pno;  
	END    
	CLOSE p_cursor;  
	DEALLOCATE p_cursor;
	
	DELETE FROM Project WHERE DNum = @DNumber;
	DELETE FROM Department WHERE DNumber = @DNumber;
GO

DROP PROCEDURE IF EXISTS usp_GetDepartment
GO

CREATE PROCEDURE usp_GetDepartment
	@DNumber int
AS
	SELECT *, (SELECT COUNT(*) FROM Employee WHERE Dno = @DNumber) AS TotalNumberOfEmployees FROM Department WHERE DNumber = @DNumber;
GO

DROP PROCEDURE IF EXISTS usp_GetAllDepartments
GO

CREATE PROCEDURE usp_GetAllDepartments
AS
	SELECT DNumber, DName, MgrSSN, MgrStartDate, (SELECT COUNT(*) FROM Employee WHERE Dno = DNumber) AS TotalNumberOfEmployees FROM Department;
GO