using Employee_Management_System.Data;
using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repositories.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users
                    .Select(a => new UserResponseDTO
                    {
                        UserId = a.UserId,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Phone = a.Phone,
                        RoleName = a.Role.RoleName,
                        IsActive = a.IsActive,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<UserResponseDTO>();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.UserId == id && u.RoleId != 1)
                .SingleOrDefaultAsync();
        }

        //Registation of AdminUser
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        
        public async Task<int?> GetDepartmentIdByUserIdAsync(int userId)
        {
            return await _context.Employees
                .Where(e => e.UserId == userId)
                .Select(e => e.DepartmentId)
                .FirstOrDefaultAsync();
        }

        public async Task AddUserAdminAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetUserEmployeeIdByEmailAsync(string email)
        {
            return await _context.Users
                .Where(ue => ue.Email == email)
                .Select(ue => ue.UserId)
                .SingleOrDefaultAsync();
        }

       
        public async Task<string> ActivateUserEmployeeAsync(User user)
        {
            try
            {
                user.IsActive = true;
                await _context.SaveChangesAsync();
                return "User employee IsActive";

               
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }

        public async Task<string> DeactivateUserEmployeeAsync(User user)
        {
            try
            {
                user.IsActive = false;
                await _context.SaveChangesAsync();
                return "User employee IsDeactive";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }

        //For update
        public async Task<User> GetUserByIDAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // For Register User Employee
        public async Task<bool> IsEmailExistingAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsValidDepartmentAsync(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<int> GetUserEmployeeIDByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user?.UserId ?? 0;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}
