# 🚀 PetaPoco CRUD Example — .NET 9 Web API

This project demonstrates a **complete CRUD application** using
**PetaPoco.Compiled (v6.0.683)** with **.NET 9 Web API**, following best practices:

* Repository Pattern
* Dependency Injection
* SQL Server Provider
* Stored Procedures
* Views & Scalar Functions
* Controller Endpoints
* Swagger Enabled

---

## 📌 Features

✔ Employee & Department tables
✔ Full CRUD operations
✔ Join queries
✔ Stored Procedure example
✔ SQL View example
✔ SQL Function example
✔ Transactions (Department + Employee insert)
✔ Swagger UI support

---

# 📦 Tech Stack

| Technology | Version          |
| ---------- | ---------------- |
| .NET       | 9                |
| PetaPoco   | Compiled 6.0.683 |
| SQL Server | 2019+            |
| Swagger    | Included         |

---

# 📁 Project Structure

```
/src
 ├── Controllers
 │    ├── EmployeeController.cs
 │    └── DepartmentController.cs
 ├── Repositories
 │    ├── EmployeeRepo.cs
 │    └── DepartmentRepo.cs
 ├── Models
 │    ├── Employee.cs
 │    └── Department.cs
 ├── Program.cs
 └── README.md
```

---

# ⚙️ 1. Install PetaPoco Package

```sh
dotnet add package PetaPoco.Compiled --version 6.0.683
```

---

# 🛢️ 2. SQL Server Tables

### Department Table

```sql
CREATE TABLE Department (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Location NVARCHAR(100)
);
```

### Employee Table

```sql
CREATE TABLE Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentId INT FOREIGN KEY REFERENCES Department(DepartmentId),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100),
    Salary DECIMAL(18,2)
);
```

---

# 📦 3. Configure PetaPoco in Program.cs

```csharp
builder.Services.AddScoped<IDatabase>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var cs = config.GetConnectionString("DefaultConnection");

    return new Database(cs, "Microsoft.Data.SqlClient");
});
```

### Enable Swagger

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

---

# 📘 4. Models

### Department.cs

```csharp
public class Department
{
    public int DepartmentId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}
```

### Employee.cs

```csharp
public class Employee
{
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public decimal Salary { get; set; }
}
```

---

# 🧰 5. Repository Layer

### EmployeeRepo.cs

```csharp
public class EmployeeRepo
{
    private readonly IDatabase _db;

    public EmployeeRepo(IDatabase db)
    {
        _db = db;
    }

    public int AddEmployee(Employee emp) => (int)_db.Insert(emp);

    public void UpdateEmployee(Employee emp) => _db.Update(emp);

    public void DeleteEmployee(int empId) => _db.Delete<Employee>(empId);

    public Employee? GetEmployee(int id) =>
        _db.SingleOrDefault<Employee>("WHERE EmployeeId=@0", id);

    public List<Employee> GetAllEmployees() =>
        _db.Fetch<Employee>("SELECT * FROM Employee");
}
```

---

### DepartmentRepo.cs

```csharp
public class DepartmentRepo
{
    private readonly IDatabase _db;

    public DepartmentRepo(IDatabase db)
    {
        _db = db;
    }

    public int AddDepartment(Department dept) => (int)_db.Insert(dept);

    public void UpdateDepartment(Department dept) => _db.Update(dept);

    public void DeleteDepartment(int id) => _db.Delete<Department>(id);

    public Department? GetDepartment(int id) =>
        _db.SingleOrDefault<Department>("WHERE DepartmentId=@0", id);

    public List<Department> GetAllDepartments() =>
        _db.Fetch<Department>("SELECT * FROM Department");
}
```

---

# 📡 6. Controllers

### EmployeeController.cs

```csharp
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeRepo _repo;

    public EmployeeController(EmployeeRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAllEmployees());

    [HttpGet("{id}")]
    public IActionResult Get(int id) => Ok(_repo.GetEmployee(id));

    [HttpPost]
    public IActionResult Add(Employee emp) => Ok(_repo.AddEmployee(emp));

    [HttpPut]
    public IActionResult Update(Employee emp)
    {
        _repo.UpdateEmployee(emp);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.DeleteEmployee(id);
        return Ok();
    }
}
```

---

### DepartmentController.cs

```csharp
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentRepo _repo;

    public DepartmentController(DepartmentRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAllDepartments());

    [HttpPost]
    public IActionResult Add(Department dept) => Ok(_repo.AddDepartment(dept));
}
```

---

# 🧪 7. Test Using Swagger

Run the project:

```
dotnet run
```

Open:

👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)
👉 or [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

# 🛠 8. Stored Procedure Example

### SQL

```sql
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
```

### Call From Repo

```csharp
_db.Execute("EXEC InsertEmployee @0, @1, @2, @3, @4",
    emp.DepartmentId, emp.FirstName, emp.LastName, emp.Email, emp.Salary);
```

---

# 🎯 Conclusion

This project shows:

✔ Clean layered architecture
✔ Proper DI with PetaPoco.Compiled
✔ Safe CRUD operations
✔ SQL + Views + SP + Functions
✔ Beginner-friendly & interview-ready implementation

---

Just tell me!
