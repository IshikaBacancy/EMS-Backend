using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.TimesheetDTOs
{
    public class TimeSheetResponseDTO
    {
        [Key]
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

        public List<TimeSheetDetailsDTO> TimeSheetDetails { get; set; }

    }
}
