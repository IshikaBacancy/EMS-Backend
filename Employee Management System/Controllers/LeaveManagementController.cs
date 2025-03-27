using Employee_Management_System.DTOs.TimesheetDTOs;
using System.Security.Claims;
using Employee_Management_System.Services.Classes;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.DTOs.LeaveManagementDTOs;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaveManagementController : Controller
    {
        private readonly ILeaveManagementService _leaveManagementService;

        public LeaveManagementController(ILeaveManagementService leaveManagementService)
        {
            _leaveManagementService = leaveManagementService;
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("Registerleave")]
        public async Task<IActionResult> RegisterLeaveAsync([FromBody] LeaveRegistrationDTO leaveRegistrationDTO)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _leaveManagementService.RegisterLeaveAsync(leaveRegistrationDTO, userId);

            if (response != "Leave Application Registered Successfully")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("GetLeaveAllEmployees")]
        public async Task<IActionResult> GetLeaveAllEmployeesAsync()
        {
            {
                var leaves = await _leaveManagementService.GetLeaveAllEmployeesAsync();
                if (leaves == null)
                {
                    return NotFound("No Leaves Found");
                }
                return Ok(leaves);
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("GetLeave")]
        public async Task<IActionResult> GetLeavesByIdAsync()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var leaves = await _leaveManagementService.GetLeavesByIdAsync(userId);

            if (leaves == null)
            {
                return NotFound("No Leaves Found");
            }
            return Ok(leaves);
        }

        //    [HttpPut("UpdateLeaveStatus")]
        //    public async Task<IActionResult> UpdateLeaveStatus([FromBody] LeaveUpdateDTO leaveUpdateDTO)
        //    {
        //        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        var employee = await _leaveManagementService.GetEmployeeByIdAsync(userId);

        //        if (employee == null)
        //            return BadRequest(new { Message = result });

        //        var response = new
        //        {
        //            Message = result,
        //            EmployeeDetails = new
        //            {
        //                employee.EmployeeId,
        //                employee.FirstName,
        //                employee.LastName,
        //                employee.DepartmentName,
        //                Leaves = employee.Leaves.Select(leave => new
        //                {
        //                    leave.StartDate,
        //                    leave.EndDate,
        //                    leave.LeaveType,
        //                    leave.Status,
        //                    leave.AppliedAt
        //                }).ToList()
        //            }
        //        };

        //        return Ok(response);






        //    }
    }
}

