﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Employee_Management_System.DTOs.UserAuthenticationDTOs;
using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Services.Classes;



namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin, Employee")]
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

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


    }
}
