    using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<string> RegisterLeaveAsync(LeaveRegistrationDTO leaveRegistrationDTO, int id);
        Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync();
        Task<List<LeaveResponseDTO>> GetLeavesByIdAsync(int id);

        Task<bool> UpdateLeaveStatusAsync(int leaveId, string status);




    }
}
