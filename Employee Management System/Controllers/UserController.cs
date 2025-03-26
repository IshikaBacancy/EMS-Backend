using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.DTOs.UserDTOs;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Employee_Management_System.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userServices.GetAllUsersAsync();
            if (users == null)
            {
                return BadRequest("No Users Found");
            }
            return Ok(users);
        }

        [HttpPost("RegistrationUserAdmin")]
        public async Task<IActionResult> RegistrationUserAdminAsync(UserRegistrationAdminDTO UserAdminDto)
        {
            var response = await _userServices.RegistrationUserAdminAsync(UserAdminDto);
            if (response == null) {

                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("RegistrationUserEmployee")]

        public async Task<IActionResult> RegistrationUserEmployeeAsync(UserEmployeeRegistrationDTO UserEmployeeDto)


        {
            var response = await _userServices.RegistrationUserEmployeeAsync(UserEmployeeDto);
            if (response == null)
            {

                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateDTO userDto)
        {
            var response = await _userServices.UpdateUserAsync(userDto);

            var user = "User Details Updated Successfully";

            if (response != $"user")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("ActivateUserEmployee")]
        public async Task<IActionResult> ActivateUserEmployeeAsync(int id)
        {
            var response = await _userServices.ActivateUserEmployeeAsync(id);

            var ActivationStatus = "User Employee Activated Successfully";

            if (response != "ActivationStatus")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeactivateUserEmployee")]
        public async Task<IActionResult> DeactivateUserEmployeeAsync(int id)
        {
            var response = await _userServices.DeactivateUserEmployeeAsync(id);

            var DeactivationStatus = "User Employee Deactivated Successfully";

            if (response != "DeactivationStatus")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }

}
