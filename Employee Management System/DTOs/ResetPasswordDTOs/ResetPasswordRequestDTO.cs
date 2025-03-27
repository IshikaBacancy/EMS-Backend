using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.ResetPasswordDTOs
{
    public class ResetPasswordRequestDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
