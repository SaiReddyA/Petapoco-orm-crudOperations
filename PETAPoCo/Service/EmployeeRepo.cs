using PetaPoco;
using PETAPoCo.ApplicationSR.Models;

namespace PETAPoCo.ApplicationSR.Service
{
    public class EmployeeRepo
    {
        private readonly IDatabase _db;

        public EmployeeRepo(IDatabase db)
        {
            _db = db;
        }
        public int AddEmployee(Employee emp)
        {
            return (int)_db.Insert(emp);
        }

        public void AddEmployeeUsingSP(Employee emp)
        {
            _db.Execute("EXEC InsertEmployee @0, @1, @2, @3, @4",
                emp.DepartmentId, emp.FirstName, emp.LastName, emp.Email, emp.Salary);
        }

        public void UpdateEmployee(Employee emp)
        {
            _db.Update(emp);
        }

        public void DeleteEmployee(int empId)
        {
            _db.Delete<Employee>(empId);
        }

        public Employee GetEmployee(int id)
        {
            return _db.SingleOrDefault<Employee>("WHERE EmployeeId=@0", id);
        }

        public List<Employee> GetAllEmployees()
        {
            return _db.Fetch<Employee>("SELECT * FROM Employee");
        }

        public List<dynamic> GetEmployeeWithDepartment()
        {
            return _db.Fetch<dynamic>(@"
                SELECT e.EmployeeId, e.FirstName, e.LastName, e.Email,
                       d.DepartmentId, d.Name AS DepartmentName
                FROM Employee e
                JOIN Department d ON e.DepartmentId = d.DepartmentId");
        }

        public List<EmployeeDetailsView> GetEmployeeDetailsFromView()
        {
            return _db.Fetch<EmployeeDetailsView>("SELECT * FROM vw_EmployeeDetails");
        }

        public int GetDepartmentCount()
        {
            return _db.ExecuteScalar<int>("SELECT dbo.fn_GetDepartmentCount()");
        }

        public void AddDepartmentAndEmployee(Department dept, Employee emp)
        {
            using (var tx = _db.GetTransaction())
            {
                dept.DepartmentId = (int)_db.Insert(dept);
                emp.DepartmentId = dept.DepartmentId;
                _db.Insert(emp);

                tx.Complete();
            }
        }




    }
}
