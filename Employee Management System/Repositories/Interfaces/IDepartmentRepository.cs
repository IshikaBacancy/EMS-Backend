using Employee_Management_System.DTOs.DepartmentDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentResponseDTO>> GetAllDepartmentsAsync();
        Task<string> RegisterDepartmentAsync(Department department);

        Task<Department> GetDepartmentByNameAsync(string name);
    }
}
