using Employee_Management_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.EmployeeDTOs
{
    public class EmployeeRegisterDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentId { get; set; }
        public string? TechStack { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Navigation Property

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }

}
