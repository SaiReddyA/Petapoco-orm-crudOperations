using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PETAPoCo.ApplicationSR.Models;
using PETAPoCo.ApplicationSR.Service;

namespace PETAPoCo.ApplicationSR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepo _repo;

        public EmployeeController(EmployeeRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            var id = _repo.AddEmployee(emp);
            return Ok(new { Message = "Employee Added", EmployeeId = id });
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee emp)
        {
            _repo.UpdateEmployee(emp);
            return Ok(new { Message = "Employee Updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _repo.DeleteEmployee(id);
            return Ok(new { Message = "Employee Deleted" });
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var emp = _repo.GetEmployee(id);
            if (emp == null)
                return NotFound();

            return Ok(emp);
        }

        [HttpGet("all")]
        public IActionResult GetAllEmployees()
        {
            var list = _repo.GetAllEmployees();
            return Ok(list);
        }

        [HttpGet("with-department")]
        public IActionResult GetEmployeeWithDepartment()
        {
            var list = _repo.GetEmployeeWithDepartment();
            return Ok(list);
        }

        [HttpGet("from-view")]
        public IActionResult GetEmployeeDetailsFromView()
        {
            var list = _repo.GetEmployeeDetailsFromView();
            return Ok(list);
        }

        [HttpGet("department-count")]
        public IActionResult GetDepartmentCount()
        {
            var count = _repo.GetDepartmentCount();
            return Ok(count);
        }

        [HttpPost("add-with-department")]
        public IActionResult AddDepartmentAndEmployee(DepartmentEmployeeRequest request)
        {
            _repo.AddDepartmentAndEmployee(request.Department, request.Employee);
            return Ok(new { Message = "Department + Employee saved using Transaction" });
        }
    }
}
