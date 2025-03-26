using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.UserDTOs
{
    public class UserRegistrationAdminDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone{ get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        public string Password { get; set; }
    }
}
