using Employee_Management_System.Data;
using Employee_Management_System.DTOs.ReportsAnalyticsDTOs;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repositories.Services
{
    public class ReportsAnalyticsRepository : IReportsAnalyticsRepository
    {
        private readonly DataContext _context;

        public ReportsAnalyticsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportWeeklyAsync()
        {
            try
            {
                DateOnly endDate = DateOnly.FromDateTime(DateTime.UtcNow);
                DateOnly startDate = endDate.AddDays(-7);
                var timesheets = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .Include(e => e.timesheets)
                    .Select(ts => new ReportAnalyticsResponseDTO
                    {
                        EmployeeId = ts.EmployeeId,
                        FirstName = ts.User.FirstName,
                        LastName = ts.User.LastName,
                        DepartmentName = ts.Department.DepartmentName,
                        ReportAnalyticDetails = ts.timesheets
                        .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId && ts.Date >= startDate && ts.Date <= endDate)
                        .GroupBy(ts => ts.EmployeeId)
                        .Select(g => new ReportAnalyticDetailsDTO
                        {
                            StartDate = startDate,
                            EndDate = endDate,
                            TotalHours = g.Sum(ts => ts.TotalHours)


                        }).ToList()
                    }).ToListAsync();

                return timesheets;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ReportAnalyticsResponseDTO>();
            }
        }

        public async Task<List<ReportAnalyticsResponseDTO>> GetAllEmployeesTimeSheetReportMonthlyAsync()
        {
            try
            {
                DateOnly endMonth = DateOnly.FromDateTime(DateTime.UtcNow);
                DateOnly startMonth = endMonth.AddMonths(-1);
                var timesheets = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .Include(e => e.timesheets)
                    .Select(ts => new ReportAnalyticsResponseDTO
                    {
                        EmployeeId = ts.EmployeeId,
                        FirstName = ts.User.FirstName,
                        LastName = ts.User.LastName,
                        DepartmentName = ts.Department.DepartmentName,
                        ReportAnalyticDetails = ts.timesheets
                        .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId && ts.Date >= startMonth && ts.Date <= endMonth)
                        .GroupBy(ts => ts.EmployeeId)
                        .Select(g => new ReportAnalyticDetailsDTO
                        {
                            StartDate = startMonth,
                            EndDate = endMonth,
                            TotalHours = g.Sum(ts => ts.TotalHours),


                        }).ToList()
                    }).ToListAsync();

                return timesheets;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ReportAnalyticsResponseDTO>();
            }
        }
    }

}

