using Employee_Management_System.Data;
using Employee_Management_System.DTOs.AccessTokenDTOs;
using Employee_Management_System.DTOs.UserAuthenticationDTOs;
using Employee_Management_System.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Employee_Management_System.Services.Classes
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly DataContext _context;
        private readonly JwtServices _jwtServices;

        public UserAuthenticationService(DataContext context, JwtServices jwtServices)
        {
            _context = context;
            _jwtServices = jwtServices;
        }

        public async Task<AccessTokenResponseDTO?> LogInUserAsync(UserLoginDTO userDto)
        {
            try
            {
                if (userDto == null)
                {
                    Console.WriteLine("Please Enter the valid User object!");
                    return null;
                }

                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);

                if (user == null)
                {
                    Console.WriteLine("Email Mismatch. Please enter a valid email address!");
                    return null;
                }

                var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
                Console.WriteLine("\n\n\n\n\n" + Convert.ToBase64String(hashBytes));
                if (user.Password != Convert.ToBase64String(hashBytes))
                {
                    Console.WriteLine("Please check the Password. Invalid Password!");
                    return null;
                }

                var tokens =  await _jwtServices.GenerateAccessToken(user);

                return new AccessTokenResponseDTO
                {
                    AccessToken = tokens
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<string> ChangePasswordAsync(UserResetPasswordDTO resetPasswordDto, int id)
        {
            try
            {
                if (resetPasswordDto == null)
                {
                    throw new Exception("Enter a Valid Reset Password object");

                }
                if (resetPasswordDto.Password == resetPasswordDto.NewPassword)
                {
                    throw new Exception("New Password can't be same as old Password.Enter a new unique Password");
                }
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);

                var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(resetPasswordDto.Password));

                //var hashedNewPassword = Convert.ToBase64String(hashBytes);
                if (user.Password != Convert.ToBase64String(hashBytes))
                {
                    throw new Exception("Invalid current Password.Enter valid current Password");
                }

                if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                {
                    throw new Exception("New Password do not match Confirm Password");
                }

                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(resetPasswordDto.NewPassword));
                user.Password = Convert.ToBase64String(hashBytes);
                await _context.SaveChangesAsync();

                return "Password Reseted Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }

    }
}
