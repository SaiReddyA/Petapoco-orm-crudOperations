
Create database PetaPOCO
USE PetaPOCO
GO

CREATE TABLE Department (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(100)
);

GO
CREATE TABLE Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentId INT NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100),
    Salary DECIMAL(18,2),

    FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);
GO
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.EmployeeId,
    e.FirstName + ' ' + e.LastName AS FullName,
    e.Email,
    e.Salary,
    d.Name AS DepartmentName,
    d.Location
FROM Employee e
JOIN Department d ON e.DepartmentId = d.DepartmentId;

Go
CREATE FUNCTION fn_GetDepartmentCount()
RETURNS INT
AS
BEGIN
    DECLARE @cnt INT;
    SELECT @cnt = COUNT(*) FROM Department;
    RETURN @cnt;
END
GO
CREATE PROCEDURE InsertEmployee
(
    @DepartmentId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Salary DECIMAL(18,2)
)
AS
BEGIN
    INSERT INTO Employee (DepartmentId, FirstName, LastName, Email, Salary)
    VALUES (@DepartmentId, @FirstName, @LastName, @Email, @Salary);
END;
