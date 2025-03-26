using Employee_Management_System.Data;
using Employee_Management_System.DTOs.DepartmentDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentResponseDTO>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _context.Departments
                    .Select(d => new DepartmentResponseDTO
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName,
                    })
                    .OrderBy(d => d.DepartmentId)
                    .AsNoTracking()
                    .ToListAsync();

                return departments;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
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

                var DepartmentExist = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentName == departmentDto.DepartmentName);
                if (DepartmentExist != null)
                {
                    throw new Exception("Department already exists");
                }

                var department = new Department
                {
                    DepartmentName = departmentDto.DepartmentName
                };

                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();

                return "Department Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
