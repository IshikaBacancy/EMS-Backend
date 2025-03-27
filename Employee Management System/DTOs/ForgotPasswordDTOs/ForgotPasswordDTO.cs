using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.ForgotPasswordDTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
