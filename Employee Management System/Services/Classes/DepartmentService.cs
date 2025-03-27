using Employee_Management_System.Data;
using Employee_Management_System.DTOs.DepartmentDTOs;
using Employee_Management_System.DTOs.EmployeeDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        public async Task<List<DepartmentResponseDTO>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetAllDepartmentsAsync();

                if(departments == null || !departments.Any()) 
                {
                    throw new Exception("Please enter valid departments details!");
                }

                return departments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in fetching departments:{ex.Message}");
                return new List<DepartmentResponseDTO>();
            }
        }
        public async Task<string> RegisterDepartmentAsync(DepartmentRegistrationDTO departmentDto)
        {
            try
            {
                if (departmentDto == null)
                {
                    throw new Exception("Please enter the valid Department Details");
                }

                var DepartmentExist = await _departmentRepository.GetDepartmentByNameAsync(departmentDto.DepartmentName);
                if (DepartmentExist != null)
                {
                    throw new Exception("Department already exists");
                }

                var department = new Department
                {
                    DepartmentName = departmentDto.DepartmentName
                };

                return await _departmentRepository.RegisterDepartmentAsync(department);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error occurred during registering the department";
            }

            
        }
    }

}
