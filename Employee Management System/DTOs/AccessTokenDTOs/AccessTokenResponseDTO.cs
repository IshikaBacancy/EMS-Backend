using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.AccessTokenDTOs
{
    public class AccessTokenResponseDTO
    {
        [Required(ErrorMessage = "Access Token is required.")]
        public string AccessToken { get; set; }
    }
}
