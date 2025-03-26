using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.EmployeeDTOs
{
    public class EmployeeResponseDTO
    {
        [Key]
        public int EmployeeId { get; set; }

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
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [MaxLength(40, ErrorMessage = "Role Name cannot exceed 40 characters.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateOnly? DateOfBirth { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? TechStack { get; set; }
    }
}
