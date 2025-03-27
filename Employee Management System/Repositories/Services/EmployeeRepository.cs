using Employee_Management_System.Data;
using Employee_Management_System.DTOs.EmployeeDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Employee_Management_System.Repositories.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .ThenInclude(u => u.Role)
                    .Select(e => new EmployeeResponseDTO
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        Email = e.User.Email,
                        Phone = e.User.Phone,
                        RoleName = e.User.Role.RoleName,
                        DepartmentName = e.Department.DepartmentName,
                        DateOfBirth = (DateOnly)e.DateOfBirth,
                        Address = e.Address,
                        TechStack = e.TechStack,
                    })
                    .AsNoTracking()
                    .ToListAsync();
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
                var employees = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .ThenInclude(u => u.Role)
                    .Where(e => e.UserId == id)
                    .Select(e => new EmployeeResponseDTO
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        Email = e.User.Email,
                        Phone = e.User.Phone,
                        RoleName = e.User.Role.RoleName,
                        DepartmentName = e.Department.DepartmentName,
                        DateOfBirth = (DateOnly)e.DateOfBirth,
                        Address = e.Address,
                        TechStack = e.TechStack,
                    })
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<string> UpdateEmployeeAsync(EmployeeUpdateDTO employeeDto, int id)
        {
            try
            {
               
                var employee = await _context.Employees
                    .Include(e => e.User)
                    .SingleOrDefaultAsync(e => e.UserId == id);

                if (employee == null)
                {
                    throw new Exception("Please enter the valid details. Employee Not Found");
                }


                employee.DateOfBirth = employeeDto.DateOfBirth;
                employee.Address = employeeDto.Address;
                employee.TechStack = employeeDto.TechStack;
                employee.User.Phone = employeeDto.Phone;

                await _context.SaveChangesAsync();
                return "Employee Details Updated Successfully";

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Error occurred while updating the employee details";
            }
        }

    }
}
