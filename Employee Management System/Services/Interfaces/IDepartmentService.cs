
using Employee_Management_System.DTOs.DepartmentDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDTO>> GetAllDepartmentsAsync();
        Task<string> RegisterDepartmentAsync(DepartmentRegistrationDTO departmentDto);
    }
}
