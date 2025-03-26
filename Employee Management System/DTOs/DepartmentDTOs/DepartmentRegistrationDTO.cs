using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.DepartmentDTOs
{
    public class DepartmentRegistrationDTO
    {
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }
    }

}
