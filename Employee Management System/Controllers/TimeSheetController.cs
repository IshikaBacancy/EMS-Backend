using System.Security.Claims;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Services.Classes;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeSheetController : Controller
    {
        private readonly ITimesheetService _timesheetService;

        public TimeSheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTimeSheets")]
        public async Task<IActionResult> GetAllTimeSheetsAsync()
        {
            var timeSheets = await _timesheetService.GetAllTimeSheetAsync();
            if (timeSheets == null)
            {
                return NotFound("No TimeSheets Found");
            }
            return Ok(timeSheets);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("GetTimeSheets")]
        public async Task<IActionResult> GetTimeSheetsByIdAsync()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var timeSheets = await _timesheetService.GetTimeSheetsByIdAsync(userId);

            if (timeSheets == null)
            {
                return NotFound("No TimeSheet Found");
            }
            return Ok(timeSheets);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("RegisterTimesheet")]
        public async Task<IActionResult> RegisterTimeSheetAsync([FromBody] TimeSheetRegistrationDTO timeSheetDto)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _timesheetService.RegisterTimeSheetAsync(timeSheetDto, userId);

            if (response != "TimeSheet registered successfully")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("UpdateTimesheet")]
        public async Task<IActionResult> UpdateTimeSheetAsync([FromBody] TimeSheetUpdateDTO timeSheetDto)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _timesheetService.UpdateTimeSheetAsync(timeSheetDto, userId);

            if (response != "TimeSheet Updated Successfully")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
