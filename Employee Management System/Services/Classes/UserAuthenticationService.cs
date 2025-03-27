using Employee_Management_System.Data;
using Employee_Management_System.DTOs.AccessTokenDTOs;
using Employee_Management_System.DTOs.UserAuthenticationDTOs;
using Employee_Management_System.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System.DTOs.ResetPasswordDTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;


namespace Employee_Management_System.Services.Classes
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly DataContext _context;
        private readonly JwtServices _jwtServices;
        private readonly IConfiguration _configuration;


        public UserAuthenticationService(DataContext context, JwtServices jwtServices, IConfiguration configuration)
        {
            _context = context;
            _jwtServices = jwtServices;
            _configuration = configuration;
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

                var tokens = await _jwtServices.GenerateAccessToken(user);

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

        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto)
        {
            try
            {
                if (resetPasswordDto == null)
                {
                    throw new Exception("ENTER VALID RESET PASSWORD OBJECT");
                }

                // ✅ Validate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("IshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyani");

                try
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //ClockSkew = TimeSpan.Zero // No time delay for token expiration
                    };

                    var principal = tokenHandler.ValidateToken(resetPasswordDto.Token, tokenValidationParameters, out SecurityToken validatedToken);
                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim == null)
                    {
                        throw new Exception("INVALID RESET PASSWORD TOKEN");
                    }

                    int userId = int.Parse(userIdClaim.Value);

                    // ✅ Fetch User by UserId from Token
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                    if (user == null)
                    {
                        throw new Exception("USER NOT FOUND");
                    }

                    // ✅ Validate New Password & Confirm Password
                    if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                    {
                        throw new Exception("CONFIRM PASSWORD DOES NOT MATCH WITH NEW PASSWORD");
                    }

                    // ✅ Hash New Password
                    using var sha256 = SHA256.Create();
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(resetPasswordDto.NewPassword));
                    string hashedNewPassword = Convert.ToBase64String(hashBytes);

                    // ✅ Prevent setting the same password
                    if (user.Password == hashedNewPassword)
                    {
                        throw new Exception("NEW PASSWORD IS SAME AS CURRENT PASSWORD");
                    }

                    // ✅ Update Password
                    user.Password = hashedNewPassword;
                    await _context.SaveChangesAsync();

                    return "PASSWORD RESET SUCCESSFULLY";
                }
                catch (SecurityTokenException)
                {
                    throw new Exception("INVALID OR EXPIRED RESET PASSWORD TOKEN");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }





        }


        public async Task<string> ResetPasswordRequestAsync(string userEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    throw new Exception("Enter Email to send request for reset password");
                }

                // ✅ Fetch User from Database
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                {
                    throw new Exception("User email not found");
                }

                // ✅ Generate JWT Token for Reset Password
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("IshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyani");
                var expiryTime = DateTime.UtcNow.AddMinutes(15);

                var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expiryTime).ToUnixTimeSeconds().ToString())
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiryTime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                string resetToken = tokenHandler.WriteToken(token);

                // ✅ Send Email with Reset Token
                EmailService emailServices = new EmailService(_configuration);
                var emailSubject = "RESET PASSWORD CREDENTIALS EMS";
                var emailBody = $@"
                Dear {user.FirstName} {user.LastName},<br><br>

               Welcome again to <b>EMS</b>! Here is your reset password credentials.<br><br>

               <b>Email:</b> {user.Email}<br>
              <b>Reset Token:</b> {resetToken}<br>
              <b>Token Validity:</b> 15 Minutes<br><br>

              Please keep these credentials safe and do not share them with anyone.<br><br>

              You can reset your password using the following link:<br>
              <a href='https://localhost:7242/swagger/index.html'>Reset Password</a><br><br>

              If you face any issues, feel free to contact our support team.<br><br>

            Best regards,<br>
            <b>The EMS Team</b><br>
        ";
                EmailService emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(user.Email, resetToken);

                return $"USER RESET PASSWORD REQUEST SENT SUCCESSFULLY. EMAIL SENT TO {user.Email}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ResetPasswordRequestAsync: {ex.Message}");
                return "An error occurred while processing the password reset request.";
            }
            


            

        }
            
    }




}


