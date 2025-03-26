using Employee_Management_System.Data;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class TimesheetService : ITimesheetService
    {
        private readonly DataContext _context;

        public TimesheetService(DataContext context)
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

        public async Task<string> RegisterTimeSheetAsync(TimeSheetRegistrationDTO timeSheetDto, int id)
        {
            try
            {
                if (timeSheetDto == null)
                {
                    throw new Exception("Please enter the valid Timesheet Details!..");
                }

                if (timeSheetDto.StartTime > timeSheetDto.EndTime)
                {
                    throw new Exception("Start Time should be first than end time");
                }

                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);
                var ValidatingTimesheet = await _context.Timesheets.AnyAsync(ts => ts.EmployeeId == employee.EmployeeId && ts.Date == timeSheetDto.Date);

                if (ValidatingTimesheet)
                {
                    throw new Exception("Timesheet for that particular employee already exist for that date");
                }

                var timeSheet = new Timesheet
                {
                    EmployeeId = employee.EmployeeId,
                    Date = timeSheetDto.Date,
                    StartTime = timeSheetDto.StartTime,
                    EndTime = timeSheetDto.EndTime,
                    TotalHours = Convert.ToDecimal((timeSheetDto.EndTime - timeSheetDto.StartTime).TotalHours),
                    Description = timeSheetDto.Description,
                };

                await _context.Timesheets.AddAsync(timeSheet);
                await _context.SaveChangesAsync();

                return "TimeSheet registered successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> UpdateTimeSheetAsync(TimeSheetUpdateDTO timeSheetDto, int id)
        {
            try
            {
                if (timeSheetDto == null)
                {
                    throw new Exception("Please enter the valid update Timesheet Details");
                }

                if (timeSheetDto.StartTime > timeSheetDto.EndTime)
                {
                    throw new Exception("Start Time should be first than end time");
                }

                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);
                var ValidatingTimesheet = await _context.Timesheets.AnyAsync(ts => ts.EmployeeId == employee.EmployeeId && ts.Date == timeSheetDto.Date);

                if (ValidatingTimesheet)
                {
                    throw new Exception("Timesheet for that particular employee already exist for that date");
                }

                var timeSheet = await _context.Timesheets.SingleOrDefaultAsync(ts => ts.TimesheetId == timeSheetDto.TimesheetId && ts.EmployeeId == employee.EmployeeId);

                if (timeSheet == null)
                {
                    throw new Exception("Enter valid TimeSheetId to Update!..");
                }

                timeSheet.Date = timeSheetDto.Date;
                timeSheet.StartTime = timeSheetDto.StartTime;
                timeSheet.EndTime = timeSheetDto.EndTime;
                timeSheet.TotalHours = Convert.ToDecimal((timeSheetDto.EndTime - timeSheetDto.StartTime).TotalHours);
                timeSheet.Description = timeSheetDto.Description;

                await _context.SaveChangesAsync();

                return "TimeSheet Updated Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
