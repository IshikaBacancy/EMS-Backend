using Employee_Management_System.Data;
using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System.Models;
using System.Security.Cryptography;
using System.Text;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Repositories.Services;




namespace Employee_Management_System.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            try
            {
                //var users = await _context.Users
                //    .Select(a => new UserResponseDTO
                //    {
                //        UserId = a.UserId,
                //        FirstName = a.FirstName,
                //        LastName = a.LastName,
                //        Email = a.Email,
                //        Phone = a.Phone,
                //        RoleName = a.Role.RoleName,
                //        IsActive = a.IsActive,
                //    })
                //    .AsNoTracking()
                //    .ToListAsync();

                return await _userRepository.GetAllUsersAsync();
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

                if (await _userRepository.IsEmailExistsAsync(UserAdminDto.Email))
                {
                    return "This Email Address already exists. Please enter a valid Email Address.";
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


                await _userRepository.AddUserAdminAsync(UserAdmin);
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

                if (await _userRepository.IsEmailExistingAsync(UserEmployeeDto.Email))
                {
                    return "This Email Address already exists. Please enter a valid Email Address.";
                }

                if (!await _userRepository.IsValidDepartmentAsync(UserEmployeeDto.DepartmentId))
                {
                    return "Please enter a valid Department ID.";
                }


                var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(UserEmployeeDto.Password));

                var UserEmployee = new User
                {
                    
                    FirstName = UserEmployeeDto.FirstName,
                    LastName = UserEmployeeDto.LastName,
                    Email = UserEmployeeDto.Email,
                    Phone = UserEmployeeDto.Phone,
                    Password = Convert.ToBase64String(hashBytes),
                    RoleId = 2,
                };

                await _userRepository.AddUserAsync(UserEmployee);
                

                int userId = await _userRepository.GetUserEmployeeIdByEmailAsync(UserEmployeeDto.Email);
                Console.WriteLine($"Fetched User ID: {userId}");
                if (userId == 0)
                {
                    return "Error in Employee Registration";
                }

                var employee = new Employee
                {
                    UserId = userId,
                    DepartmentId = UserEmployeeDto.DepartmentId,
                   
                };

                await _userRepository.AddUserEmployeeAsync(employee);

                return "User Employee Registered Successully..";
            }
            catch (Exception ex)
            {
                return ex.Message;
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

                var user = await _userRepository.GetUserByIDAsync(userDto.UserId);

                if (user == null)
                {
                    throw new Exception("User not found..");
                }

                if (user.Email != userDto.Email && await _userRepository.IsEmailExistAsync(userDto.Email))
                {
                    throw new Exception("Email Mismatch. The email address is already in use..");
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.Phone = userDto.Phone;
                user.UpdatedAt = DateTime.UtcNow;

                await _userRepository.UpdateUserAsync(user);
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
               var user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new Exception("User Employee Not Found. Please enter the valid Employee Details:");
                }

                if (!user.IsActive)
                {
                    await _userRepository.ActivateUserEmployeeAsync(user);
                    return "User Employee Activated Successfully.";
                }
                else
                {
                    return ("User Employee is already in Activated mode");
                }

               

               
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
                var user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new Exception("User Employee Not Found. Please enter the valid Employee Details:");
                }

                if (user.IsActive)
                {
                    await _userRepository.DeactivateUserEmployeeAsync(user);
                    return "User Employee Deactivated Successfully.";
                }
                else
                {
                    return ($"User Employee is already in Deactivated mode");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
