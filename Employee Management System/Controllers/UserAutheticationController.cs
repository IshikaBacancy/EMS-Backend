using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Employee_Management_System.DTOs.UserAuthenticationDTOs;
using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Services.Classes;
using Employee_Management_System.DTOs.ResetPasswordDTOs;



namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin, Employee")]
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [Authorize(Roles = "Admin, Employee")]
        [AllowAnonymous]
            [HttpPost("LogInUser")]
            public async Task<IActionResult> LogInUserAsync([FromBody] UserLoginDTO userDto)

            {
                
                var response = await _userAuthenticationService.LogInUserAsync(userDto);

                if (response == null)
                {
                    return BadRequest("Error in creating Token/ Invalid Email Address / Invalid Password");
                }

                return Ok(response);
            }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] UserResetPasswordDTO resetPasswordDTO)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _userAuthenticationService.ChangePasswordAsync(resetPasswordDTO, userId);

            if (response != "Password Reseted Successfully")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RequestResetPassword")]
        public async Task<IActionResult> RequestResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Invalid email address.");
            }

            string result = await _userAuthenticationService.ResetPasswordRequestAsync(request.Email);

            if (result.StartsWith("USER RESET PASSWORD REQUEST SENT SUCCESSFULLY"))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            string result = await _userAuthenticationService.ResetPasswordAsync(resetPasswordDto);

            if (result == "PASSWORD RESET SUCCESSFULLY")
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }


    }
}
