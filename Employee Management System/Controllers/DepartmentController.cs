using Employee_Management_System.DTOs.DepartmentDTOs;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            if (departments == null)
            {
                return NotFound("No department Found!");
            }

            return Ok(departments);
        }

        [HttpPost("RegisterDepartment")]
        public async Task<IActionResult> RegisterDepartmentAsync(DepartmentRegistrationDTO departmentDto)
        {
            var response = await _departmentService.RegisterDepartmentAsync(departmentDto);

            var DepartmentExists = "Department Added Successfully";
            if (response != "DepartmentExists")
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
