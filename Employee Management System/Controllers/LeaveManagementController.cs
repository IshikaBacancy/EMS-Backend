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

        [Authorize(Roles = "Admin")]
        [HttpPut("update-leave-status/{leaveId}")]
        public async Task<IActionResult> UpdateLeaveStatus(int leaveId, [FromBody] string status)
        {
            try
            {
                bool updated = await _leaveManagementService.UpdateLeaveStatusAsync(leaveId, status);

                if (!updated)
                    return NotFound(new { Message = "Leave request not found or could not be updated." });

                return Ok(new { Message = "Leave status updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating leave status.", Error = ex.Message });
            }
        }
    }
}

