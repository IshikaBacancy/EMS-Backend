using Employee_Management_System.Data;
using Employee_Management_System.DTOs.DashboardDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Repositories.Services;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace Employee_Management_System.Services.Classes
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ITimeSheetRepository _timesheetRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TimesheetService(ITimeSheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
        {
            _timesheetRepository = timesheetRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<List<TimeSheetResponseDTO>> GetAllTimeSheetAsync()
        {
            try
            {
                //var timesheets = await _context.Employees
                //    .Include(e => e.Department)
                //    .Include(e => e.User)
                //    .Include(e => e.timesheets)
                //    .Select(ts => new TimeSheetResponseDTO
                //    {
                //        EmployeeId = ts.EmployeeId,
                //        FirstName = ts.User.FirstName,
                //        LastName = ts.User.LastName,
                //        DepartmentName = ts.Department.DepartmentName,
                //        TimeSheetDetails = ts.timesheets
                //        .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId)
                //        .Select(ets => new TimeSheetDetailsDTO
                //        {
                //            TimesheetId = ets.TimesheetId,
                //            Date = ets.Date,
                //            StartTime = ets.StartTime,
                //            EndTime = ets.EndTime,
                //            TotalHours = ets.TotalHours,
                //            Description = ets.Description,
                //        }).ToList()
                //    }).ToListAsync();

                return await _timesheetRepository.GetAllTimeSheetAsync();
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
                //var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);

                //var timeSheets = await _context.Employees
                //    .Include(e => e.Department)
                //    .Include(e => e.User)
                //    .Include(e => e.timesheets)
                //    .Where(e => e.EmployeeId == employee.EmployeeId)
                //    .Select(ts => new TimeSheetResponseDTO
                //    {
                //        EmployeeId = ts.EmployeeId,
                //        FirstName = ts.User.FirstName,
                //        LastName = ts.User.LastName,
                //        DepartmentName = ts.Department.DepartmentName,
                //        TimeSheetDetails = ts.timesheets
                //                .Where(ts => ts.EmployeeId == ts.Employee.EmployeeId)
                //                .Select(ets => new TimeSheetDetailsDTO
                //                {
                //                    TimesheetId = ets.TimesheetId,
                //                    Date = ets.Date,
                //                    StartTime = ets.StartTime,
                //                    EndTime = ets.EndTime,
                //                    TotalHours = ets.TotalHours,
                //                    Description = ets.Description,
                //                })
                //                    .ToList()
                //    }
                //    ).ToListAsync();

                return await _timesheetRepository.GetTimeSheetsByIdAsync(id);

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

                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    throw new Exception("Employee not found.");
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

                await _timesheetRepository.RegisterTimeSheetAsync(timeSheet);
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
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return "Employee not found.";
                }

                var timeSheet = await _timesheetRepository.GetTimeSheetByIdAsync(timeSheetDto.TimesheetId, employee.EmployeeId);
                if (timeSheet == null)
                {
                    return "Invalid Timesheet ID.";
                }


                timeSheet.Date = timeSheetDto.Date;
                timeSheet.StartTime = timeSheetDto.StartTime;
                timeSheet.EndTime = timeSheetDto.EndTime;
                timeSheet.TotalHours = Convert.ToDecimal((timeSheetDto.EndTime - timeSheetDto.StartTime).TotalHours);
                timeSheet.Description = timeSheetDto.Description;

                await _timesheetRepository.UpdateTimeSheetAsync(timeSheet);

                return "TimeSheet Updated Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<byte[]> ExportTimesheetsToExcelAsync()
        {
            var timesheetData = await _timesheetRepository.GetAllTimeSheetAsync();

            if (timesheetData == null || timesheetData.Count == 0)
            {
                throw new Exception("No timesheet data available for export.");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Timesheets");

            // Headers
            worksheet.Cells[1, 1].Value = "Employee ID";
            worksheet.Cells[1, 2].Value = "First Name";
            worksheet.Cells[1, 3].Value = "Last Name";
            worksheet.Cells[1, 4].Value = "Department";
            worksheet.Cells[1, 5].Value = "Date";
            worksheet.Cells[1, 6].Value = "Start Time";
            worksheet.Cells[1, 7].Value = "End Time";
            worksheet.Cells[1, 8].Value = "Total Hours";
            worksheet.Cells[1, 9].Value = "Description";

            int row = 2;
            foreach (var employee in timesheetData)
            {
                foreach (var ts in employee.TimeSheetDetails)
                {
                    worksheet.Cells[row, 1].Value = employee.EmployeeId;
                    worksheet.Cells[row, 2].Value = employee.FirstName;
                    worksheet.Cells[row, 3].Value = employee.LastName;
                    worksheet.Cells[row, 4].Value = employee.DepartmentName;
                    worksheet.Cells[row, 5].Value = ts.Date.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 6].Value = ts.StartTime.ToString();
                    worksheet.Cells[row, 7].Value = ts.EndTime.ToString();
                    worksheet.Cells[row, 8].Value = ts.TotalHours;
                    worksheet.Cells[row, 9].Value = ts.Description;

                    row++;
                }
            }

            using var stream = new MemoryStream();
            package.SaveAs(stream);
            return stream.ToArray();

        }

       



    }   
}
