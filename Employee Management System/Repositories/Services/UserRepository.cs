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

        public async Task AddUserEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
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
    }
}
