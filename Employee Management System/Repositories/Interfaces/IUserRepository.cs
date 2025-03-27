using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Repositories.Interfaces
{

    public interface IUserRepository
    {
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        //Task<string> RegistrationUserAdminAsync(UserRegistrationAdminDTO UserAdminDto);
        //Task<string> RegistrationUserEmployeeAsync(UserEmployeeRegistrationDTO UserEmployeeDto);
        //Task<string> UpdateUserAsync(UserUpdateDTO userDto);

        //Registration of AdminUser
        Task<bool> IsEmailExistsAsync(string email);
        Task AddUserAdminAsync(User user);

       

       
        Task<int> GetUserEmployeeIdByEmailAsync(string email);

        Task<string> ActivateUserEmployeeAsync(User user);

        Task<string> DeactivateUserEmployeeAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        //Task<string> DeactivateUserEmployeeAsync(int id);

        //updates
        Task<User> GetUserByIDAsync(int id);

        Task<bool> IsEmailExistAsync(string email);

        Task UpdateUserAsync(User user);

        //Register User Employee Register
        Task<bool> IsEmailExistingAsync(string email);

        Task<bool> IsValidDepartmentAsync(int departmentId);

        Task<int> GetUserEmployeeIDByEmailAsync(string email);

        Task AddUserAsync(User user);
        Task AddUserEmployeeAsync(Employee employee);






    }
}
