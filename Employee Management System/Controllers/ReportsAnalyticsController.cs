using Employee_Management_System.Services.Classes;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ReportsAnalyticsController : Controller
    {
        private readonly IReportsAnalyticsService _ReportsAnalyticsService;

        public ReportsAnalyticsController(IReportsAnalyticsService ReportsAnalyticsService)
        {
            _ReportsAnalyticsService = ReportsAnalyticsService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllEmployeesTimeSheetReportWeekly")]
        public async Task<IActionResult> GetAllEmployeesTimeSheetReportWeeklyAsync()
        {
            var timeSheets = await _ReportsAnalyticsService.GetAllEmployeesTimeSheetReportWeeklyAsync();
            if (timeSheets == null)
            {
                return NotFound("No TimeSheets Found");
            }
            return Ok(timeSheets);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllEmployeesTimeSheetReportMonthly")]
        public async Task<IActionResult> GetAllEmployeesTimeSheetReportMonthlyAsync()
        {
            var timeSheets = await _ReportsAnalyticsService.GetAllEmployeesTimeSheetReportMonthlyAsync();
            if (timeSheets == null)
            {
                return NotFound("No TimeSheets Found");
            }
            return Ok(timeSheets);
        }

    }
}
