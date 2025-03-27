using Employee_Management_System.Data;
using Employee_Management_System.DTOs.DepartmentDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repositories.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
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

        public async Task<string> RegisterDepartmentAsync(Department department)
        {
            try
            { 
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();

                return "Department Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<Department> GetDepartmentByNameAsync(string name)
        {
            return await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentName == name);
        }
    }
}
