using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.UserAuthenticationDTOs
{
    public class UserResetPasswordDTO
    {
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "NewPassword is required.")]
        [MinLength(5, ErrorMessage = "NewPassword must be at least 6 characters.")]
        [MaxLength(20, ErrorMessage = "NewPassword cannot exceed 20 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [MinLength(5, ErrorMessage = "ConfirmPassword must be at least 6 characters.")]
        [MaxLength(20, ErrorMessage = "ConfirmPassword cannot exceed 20 characters.")]
        public string ConfirmPassword { get; set; }
    }
}

