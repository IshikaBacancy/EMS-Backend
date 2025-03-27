using Employee_Management_System.DTOs.EmployeeDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);

        Task<string> UpdateEmployeeAsync(EmployeeUpdateDTO employeeDto, int id);


    }
}
