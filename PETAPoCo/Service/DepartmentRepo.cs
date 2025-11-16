using PetaPoco;
using PETAPoCo.ApplicationSR.Models;

namespace PETAPoCo.ApplicationSR.Service
{
    public class DepartmentRepo
    {
        private readonly IDatabase _db;

        public DepartmentRepo(IDatabase db)
        {
            _db = db;
        }

        public int AddDepartment(Department dept)
        {
            return (int)_db.Insert(dept);
        }

        public void UpdateDepartment(Department dept)
        {
            _db.Update(dept);
        }

        public void DeleteDepartment(int id)
        {
            _db.Delete<Department>(id);
        }

        public Department GetDepartment(int id)
        {
            return _db.SingleOrDefault<Department>("WHERE DepartmentId=@0", id);
        }

        public List<Department> GetAllDepartments()
        {
            return _db.Fetch<Department>("SELECT * FROM Department");
        }
    }
}
