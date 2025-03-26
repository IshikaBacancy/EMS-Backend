using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.DTOs.UserDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface ILeaveManagementService
    {
        Task<string> RegisterLeaveAsync(LeaveRegistrationDTO leaveRegistrationDTO, int id);

        Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync();

        Task<List<LeaveResponseDTO>> GetLeavesByIdAsync(int id);

        //Task<string> UpdateLeaveAsync(LeaveUpdateDTO updateLeaveDTO, int id);
    }
}
