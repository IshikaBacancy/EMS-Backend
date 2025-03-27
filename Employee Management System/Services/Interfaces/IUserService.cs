using Employee_Management_System.DTOs.UserDTOs;



namespace Employee_Management_System.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<string> RegistrationUserAdminAsync(UserRegistrationAdminDTO UserAdminDto);
        Task<string> RegistrationUserEmployeeAsync(UserEmployeeRegistrationDTO UserEmployeeDto);
        Task<string> UpdateUserAsync(UserUpdateDTO userDto);
        Task<string> ActivateUserEmployeeAsync(int id);
        Task<string> DeactivateUserEmployeeAsync(int id);
    }
}
