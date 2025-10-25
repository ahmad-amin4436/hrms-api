using hrmsapi.DataAccessLayer;
using hrmsapi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hrmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DAL _dal;

        public DepartmentController(DAL dal)
        {
            _dal = dal;
        }

        [HttpPost]
        public async Task<IActionResult> SaveDepartment([FromBody] Department department)
        {
            if (department == null)
                return BadRequest("Invalid department data.");

            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                return BadRequest("Department Name is required.");

            bool isSaved = _dal.AddDepartment(department);

            if (isSaved)
            {
                return Ok(new
                {
                    Message = "Department saved successfully.",
                    Data = new
                    {
                        department.DepartmentName,
                        IsActive = department.IsActive == 1 ? "Active" : "Inactive"
                    }
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    Message = "Failed to save department. Please try again.",
                    Data = new
                    {
                        department.DepartmentName,
                        IsActive = department.IsActive == 1 ? "Active" : "Inactive"
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                // Example: Normally you'd fetch from your database or DAL
                var departments = new List<object>
        {
            new { DepartmentID = 1, DepartmentName = "Finance", Status = "Active" },
            new { DepartmentID = 2, DepartmentName = "HR", Status = "Inactive" },
            new { DepartmentID = 3, DepartmentName = "IT", Status = "Active" }
        };

                return Ok(new
                {
                    Success = true,
                    Message = "Departments retrieved successfully.",
                    Data = departments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while retrieving departments.",
                    Error = ex.Message
                });
            }
        }

    }
}
