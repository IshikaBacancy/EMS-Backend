using Employee_Management_System.DTOs.AccessTokenDTOs;
using Employee_Management_System.DTOs.ResetPasswordDTOs;
using Employee_Management_System.DTOs.UserAuthenticationDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<AccessTokenResponseDTO?> LogInUserAsync(UserLoginDTO userDto);
        Task<string> ChangePasswordAsync(UserResetPasswordDTO resetPasswordDto, int id);

       Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto);

        Task<string> ResetPasswordRequestAsync(string userEmail);



    }
}
