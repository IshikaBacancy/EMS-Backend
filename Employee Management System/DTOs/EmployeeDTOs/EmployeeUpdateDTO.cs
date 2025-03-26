using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.EmployeeDTOs
{
    public class EmployeeUpdateDTO
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? TechStack { get; set; }
    }
}
