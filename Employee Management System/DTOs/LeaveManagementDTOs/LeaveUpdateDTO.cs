using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.LeaveManagementDTOs
{
    public class LeaveUpdateDTO
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public List<LeaveSheetDetailsDTO> LeaveSheetDetails { get; set; }
    }
}