using Employee_Management_System.Data;
using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Repositories.Services;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class LeaveManagementService : ILeaveManagementService
    {
        private readonly ILeaveManagementRepository _leaveManagementRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveManagementService(ILeaveManagementRepository leaveManagementRepository, IEmployeeRepository employeeRepository)
        {
            _leaveManagementRepository = leaveManagementRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<string> RegisterLeaveAsync(LeaveRegistrationDTO leaveRegistrationDTO, int id)
        {
            try
            {
                if (leaveRegistrationDTO == null)
                {
                    throw new Exception("Please enter a valid Leave Application Object!..");
                }

                

                if (leaveRegistrationDTO.StartDate > leaveRegistrationDTO.EndDate)
                {
                    throw new Exception("Start Date should be before than End Date!..");
                }

               
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    throw new Exception("Employee Not Found!");
                }

               
                var leave = new Leave
                {
                    EmployeeId = employee.EmployeeId,
                    StartDate = leaveRegistrationDTO.StartDate,
                    EndDate = leaveRegistrationDTO.EndDate,
                    LeaveType = leaveRegistrationDTO.LeaveType,
                    Reason = leaveRegistrationDTO.Reason,
                };
                return await _leaveManagementRepository.RegisterLeaveAsync(leave);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "An unexpected error occurred. Please try again later";

            }
        }

        public async Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync()
        {
            try
            {
               

                return await _leaveManagementRepository.GetLeaveAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;


            }



        }

        public async Task<List<LeaveResponseDTO>> GetLeavesByIdAsync(int id)
        {
            try
            {
                //var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);

                //var leavesApplication = await _context.Employees
                //    .Include(e => e.Department)
                //    .Include(e => e.User)
                //    .Include(e => e.timesheets)
                //    .Where(e => e.EmployeeId == employee.EmployeeId)
                //    .Select(lr => new LeaveResponseDTO
                //    {
                //        EmployeeId = lr.EmployeeId,
                //        FirstName = lr.User.FirstName,
                //        LastName = lr.User.LastName,
                //        DepartmentName = lr.Department.DepartmentName,
                //        LeaveSheetDetails = lr.Leaves.Select(leave => new LeaveSheetDetailsDTO
                //        {
                //            StartDate = leave.StartDate,
                //            EndDate = leave.EndDate,
                //            LeaveType = leave.LeaveType,
                //            Reason = leave.Reason,
                //            Status = leave.Status,
                //            AppliedAt = leave.AppliedAt,
                //        })
                //        .ToList()
                //    }
                //    ).ToListAsync();
                return await _leaveManagementRepository.GetLeavesByIdAsync(id);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }

        }

        //public async Task<string> UpdateLeaveAsync(LeaveUpdateDTO updateLeaveDTO, int id)
        //{
        //    try
        //    {
        //        if (updateLeaveDTO == null || updateLeaveDTO.LeaveSheetDetails == null || !updateLeaveDTO.LeaveSheetDetails.Any())
        //        {
        //            throw new Exception("Please enter the valid update Timesheet Details");
        //        }

        //        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        //        if (employee == null)
        //        {
        //            throw new Exception("Employee not found.");
        //        }

        //        if (updateLeaveDTO.StartDate > updateLeaveDTO.EndDate)
        //        {
        //            throw new Exception("Start Time should be first than end time");
        //        }

        //        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);
        //        var ValidatingTimesheet = await _context.Timesheets.AnyAsync(ts => ts.EmployeeId == employee.EmployeeId && ts.Date == timeSheetDto.Date);

        //        if (ValidatingTimesheet)
        //        {
        //            throw new Exception("Timesheet for that particular employee already exist for that date");
        //        }

        //        var timeSheet = await _context.Timesheets.SingleOrDefaultAsync(ts => ts.TimesheetId == timeSheetDto.TimesheetId && ts.EmployeeId == employee.EmployeeId);

        //        if (timeSheet == null)
        //        {
        //            throw new Exception("Enter valid TimeSheetId to Update!..");
        //        }

        //        timeSheet.Date = updateLeaveDTO.StartDate;
        //        timeSheet.StartTime = updateLeaveDTO.StartTime;
        //        timeSheet.EndTime = updateLeaveDTO.EndTime;
        //        timeSheet.TotalHours = Convert.ToDecimal((updateLeaveDTO.EndTime - updateLeaveDTO.StartTime).TotalHours);
        //        timeSheet.Description = updateLeaveDTO.Description;

        //        await _context.SaveChangesAsync();

        //        return "TimeSheet Updated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public async Task<(string, Employee)> UpdateLeaveStatusAsync(LeaveUpdateDTO leaveUpdateDTO, LeaveUpdateDTO.Status, int id)
        //{
        //    // Fetch employee details using EmployeeId
        //    var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        //    if (employee == null)
        //    {
        //        return ("Employee not found!", null);
        //    }

        //    // Update Leave Status for all leave records
        //    var isUpdated = await _leaveManagementRepository.UpdateLeaveStatusAsync(employee.EmployeeId, leaveUpdateDTO.Status, id);

        //    return isUpdated ? ("Leave status updated successfully.", employee) : ("Failed to update leave status.", null);

        //}











    }
}
