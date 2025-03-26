using Employee_Management_System.DTOs.ReportsAnalyticsDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IReportsAnalyticsService
    {
        Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportWeeklyAsync();

        Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportMonthlyAsync();

    }
}
