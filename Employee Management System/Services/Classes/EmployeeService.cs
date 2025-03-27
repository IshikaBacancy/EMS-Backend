using Employee_Management_System.Data;
using Employee_Management_System.DTOs.EmployeeDTOs;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            try
            {
               
                var employees = await _employeeRepository.GetAllEmployeesAsync();   
                
                if(employees == null || !employees.Any())
                {
                    throw new Exception("Please enter valid employees details!");
                }
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<EmployeeResponseDTO>();
            }
        }

        public async Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    throw new Exception("Please enter valid employee details!");
                }
                return employee;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null; ;
            }
        }

        public async Task<string> UpdateEmployeeAsync(EmployeeUpdateDTO employeeDto, int id)
        {
            try
            {
                if (employeeDto == null)
                {
                    return null;
                }

                var updateemployee = await _employeeRepository.UpdateEmployeeAsync(employeeDto, id);
                
                if (updateemployee == null)
                {
                    throw new Exception("Please enter the valid update employee details!");
                }
                return updateemployee;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }

    
}
