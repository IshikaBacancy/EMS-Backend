using Employee_Management_System.DTOs.EmployeeDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IEmployeeService 
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);
        Task<string> UpdateEmployeeAsync(EmployeeUpdateDTO employeeDto, int id);
    }
}
