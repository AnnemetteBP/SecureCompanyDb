--Exercise A
--This should fail MgrSSN EXISTS
EXEC usp_CreateDepartment @DName = N'Evil Corp', @MgrSSN = 123456789;
--This should succeed
EXEC usp_CreateDepartment @DName = N'Evil Corp', @MgrSSN = 666884444;
--This should fail DName EXISTS
EXEC usp_CreateDepartment @DName = N'Evil Corp', @MgrSSN = 888665555;

--Exercise B
--This should succeed
EXEC usp_UpdateDepartmentName @DNumber = 6, @DName = N'E Corp';
--This should fail DName EXISTS
EXEC usp_UpdateDepartmentName @DNumber = 5, @DName = N'E Corp';

--Exercise C
--This should fail MgrSSN already MgrSSN
EXEC usp_UpdateDepartmentManager @DNumber = 6, @MgrSSN = 987654321;
--This should succeed
EXEC usp_UpdateDepartmentManager @DNumber = 6, @MgrSSN = 987987987;

--Exercise D
--This should succeed
EXEC usp_DeleteDepartment @DNumber  = 6;

--Exercise E
--This should succeed with TotalNumbersOfEmployees = 4
EXEC usp_GetDepartment @DNumber = 5;

--Exercise F
--This should succeed
EXEC usp_GetAllDepartments;