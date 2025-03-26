using Employee_Management_System.Data;
using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System.Models;
using System.Security.Cryptography;
using System.Text;




namespace Employee_Management_System.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
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

        public async Task<string> RegistrationUserAdminAsync(UserRegistrationAdminDTO UserAdminDto)
        {
            try
            {
                if (UserAdminDto == null)
                {
                    throw new Exception("Please enter the valid User Admin Details.");


                }

                if (await _context.Users.AnyAsync(u => u.Email == UserAdminDto.Email))
                {
                    throw new Exception("This Email Address already exists. Please enter a valid Email Address.");
                }

                var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(UserAdminDto.Password));
                var UserAdmin = new User
                {
                    FirstName = UserAdminDto.FirstName,
                    LastName = UserAdminDto.LastName,
                    Email = UserAdminDto.Email,
                    Phone = UserAdminDto.Phone,
                    Password = Convert.ToBase64String(hashBytes),
                    RoleId = 1,
                };


                await _context.Users.AddAsync(UserAdmin);
                await _context.SaveChangesAsync();

                return "User Admin Registered Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> RegistrationUserEmployeeAsync(UserEmployeeRegistrationDTO UserEmployeeDto)
        {
            try
            {
                if (UserEmployeeDto == null)
                {
                    throw new Exception("Please enter the valid User Employees Details.");
                }

                if (await _context.Users.AnyAsync(u => u.Email == UserEmployeeDto.Email))
                {
                    throw new Exception("This Email Address already exists. Please enter a valid Email Address.");
                }

                if (await _context.Departments.AllAsync(d => d.DepartmentId != UserEmployeeDto.DepartmentId))
                {
                    throw new Exception("Please enter a valid DepartmentID:");
                }

                var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(UserEmployeeDto.Password));

                var userEmployee = new User
                {
                    FirstName = UserEmployeeDto.FirstName,
                    LastName = UserEmployeeDto.LastName,
                    Email = UserEmployeeDto.Email,
                    Phone = UserEmployeeDto.Phone,
                    Password = Convert.ToBase64String(hashBytes),
                    RoleId = 2,
                };

                await _context.Users.AddAsync(userEmployee);
                await _context.SaveChangesAsync();

                var userEmployeeId = await _context.Users
                    .Where(ue => ue.Email == UserEmployeeDto.Email)
                    .Select(ue => ue.UserId).SingleOrDefaultAsync();

                if (userEmployeeId == 0)
                {
                    throw new Exception("Error in Employee Registration");
                }

                var employee = new Employee
                {
                    UserId = userEmployeeId,
                    DepartmentId = UserEmployeeDto.DepartmentId,
                };

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                return $"User Employee Registered Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message ;
            }
        }

        public async Task<string> UpdateUserAsync(UserUpdateDTO userDto)
        {
            try
            {
                if (userDto == null)
                {
                    throw new Exception("Please enter the valid User Object");
                }

                var user = await _context.Users.FindAsync(userDto.UserId);

                if (user == null)
                {
                    throw new Exception("User not found..");
                }

                if (user.Email != userDto.Email && await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                {
                    throw new Exception("Email Mismatch. The email address is already in use..");
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.Phone = userDto.Phone;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return "User Details Updated Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }

        public async Task<string> ActivateUserEmployeeAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.UserId == id && u.RoleId != 1)
                    .SingleOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User Employee Not Found. Please enter the valid Employee Details:");
                }

                if (!user.IsActive)
                {
                    user.IsActive = true;
                }
                else
                {
                    return("User Employee is already in Activated mode");
                }

                await _context.SaveChangesAsync();

                return $"User Employee Activated Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }

        public async Task<string> DeactivateUserEmployeeAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.UserId == id && u.RoleId != 1)
                    .SingleOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User Employee Not Found. Please enter the valid Employee Details:");
                }

                if (user.IsActive)
                {
                    user.IsActive = false;
                }
                else
                {
                    return($"User Employee is already in Deactivated mode");
                }

                await _context.SaveChangesAsync();

                return $"User Employee Deactivated Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
