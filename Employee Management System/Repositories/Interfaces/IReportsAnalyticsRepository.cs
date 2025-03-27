using Employee_Management_System.DTOs.ReportsAnalyticsDTOs;

namespace Employee_Management_System.Repositories.Interfaces
{
    public interface IReportsAnalyticsRepository
    {
        Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportWeeklyAsync();

        Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportMonthlyAsync();
    }
}
