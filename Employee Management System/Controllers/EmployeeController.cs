using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.DTOs.EmployeeDTOs;
using Employee_Management_System.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeServices;

        public EmployeeController(IEmployeeService employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeServices.GetAllEmployeesAsync();

            if (employees == null)
            {
                return BadRequest("no Employees Found!");
            }
            return Ok(employees);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployeeByIdAsync()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var employees = await _employeeServices.GetEmployeeByIdAsync(userId);

            if (employees == null)
            {
                return BadRequest("No Employees Found");
            }
            return Ok(employees);
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync(EmployeeUpdateDTO employeeDTO)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var employees = await _employeeServices.UpdateEmployeeAsync(employeeDTO, userId);

            if (employees == null)
            {
                return BadRequest("NO EMPLOYEE FOUND");
            }
            return Ok(employees);
        }






    }
}
