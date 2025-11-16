using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PETAPoCo.ApplicationSR.Models;
using PETAPoCo.ApplicationSR.Service;

namespace PETAPoCo.ApplicationSR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepo _repo;

        public DepartmentController(DepartmentRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult AddDepartment(Department dept)
        {
            var id = _repo.AddDepartment(dept);
            return Ok(new { Message = "Department Added", DepartmentId = id });
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department dept)
        {
            _repo.UpdateDepartment(dept);
            return Ok(new { Message = "Department Updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            _repo.DeleteDepartment(id);
            return Ok(new { Message = "Department Deleted" });
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var dept = _repo.GetDepartment(id);
            if (dept == null)
                return NotFound();

            return Ok(dept);
        }

        [HttpGet("all")]
        public IActionResult GetAllDepartments()
        {
            var list = _repo.GetAllDepartments();
            return Ok(list);
        }
    }
}
