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

        

       

    }

}
