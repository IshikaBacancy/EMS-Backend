using Employee_Management_System.Data;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repositories.Services
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly DataContext _context;

        public TimeSheetRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<TimeSheetResponseDTO>> GetAllTimeSheetAsync()
        {
            try
            {
                var timesheets = await _context.Employees
                   .Include(e => e.Department)
                   .Include(e => e.User)
                   .Include(e => e.timesheets)
                   .Select(ts => new TimeSheetResponseDTO
                   {
                       EmployeeId = ts.EmployeeId,
                       FirstName = ts.User.FirstName,
                       LastName = ts.User.LastName,
                       DepartmentName = ts.Department.DepartmentName,
                       TimeSheetDetails = ts.timesheets
                       .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId)
                       .Select(ets => new TimeSheetDetailsDTO
                       {
                           TimesheetId = ets.TimesheetId,
                           Date = ets.Date,
                           StartTime = ets.StartTime,
                           EndTime = ets.EndTime,
                           TotalHours = ets.TotalHours,
                           Description = ets.Description,
                       }).ToList()
                   }).ToListAsync();

                return timesheets;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<List<TimeSheetResponseDTO>> GetTimeSheetsByIdAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);

                var timeSheets = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .Include(e => e.timesheets)
                    .Where(e => e.EmployeeId == employee.EmployeeId)
                    .Select(ts => new TimeSheetResponseDTO
                    {
                        EmployeeId = ts.EmployeeId,
                        FirstName = ts.User.FirstName,
                        LastName = ts.User.LastName,
                        DepartmentName = ts.Department.DepartmentName,
                        TimeSheetDetails = ts.timesheets
                                .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId)
                                .Select(ets => new TimeSheetDetailsDTO
                                {
                                    TimesheetId = ets.TimesheetId,
                                    Date = ets.Date,
                                    StartTime = ets.StartTime,
                                    EndTime = ets.EndTime,
                                    TotalHours = ets.TotalHours,
                                    Description = ets.Description,
                                })
                                    .ToList()
                    }
                    ).ToListAsync();

                return timeSheets;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<string> RegisterTimeSheetAsync(Timesheet timeSheets)
        {
            
            await _context.Timesheets.AddAsync(timeSheets);
            await _context.SaveChangesAsync();

            return "Timesheet Registered Successfully";

        }

        public async Task<Timesheet> GetTimeSheetByIdAsync(int timesheetId, int employeeId)
        {
            return await _context.Timesheets.SingleOrDefaultAsync(ts => ts.TimesheetId == timesheetId && ts.EmployeeId == employeeId);
        }
        public async Task UpdateTimeSheetAsync(Timesheet timeSheet)
        {
            _context.Timesheets.Update(timeSheet);
            await _context.SaveChangesAsync();
        }

    }
}
