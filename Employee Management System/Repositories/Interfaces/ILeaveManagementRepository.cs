using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Repositories.Interfaces
{
    public interface ILeaveManagementRepository
    {
       
        Task<string> RegisterLeaveAsync(Leave leave);

        Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync();

        Task<List<LeaveResponseDTO>> GetLeavesByIdAsync(int id);

        //Task<bool> UpdateLeaveStatusAsync(int employeeId, string status);

        Task<Employee?> GetEmployeeByIdAsync(int id);
        //Task<bool> IsTimesheetExistsAsync(int employeeId, DateTime date);
        //Task<Timesheet?> GetTimesheetByIdAsync(int timesheetId, int employeeId);
        //Task UpdateTimesheetDetailsAsync(Timesheet timesheet);
        //Task<bool> IsTimesheetExistsAsync(int employeeId, DateOnly startDate);

        Task<bool> UpdateLeaveStatusAsync(int leaveId, string status);
    }

}
