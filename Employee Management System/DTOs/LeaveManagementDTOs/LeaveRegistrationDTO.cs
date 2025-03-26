using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.DTOs.LeaveManagementDTOs
{
    public class LeaveRegistrationDTO
    {
        //[Required(ErrorMessage = "Employee ID is required")]
        //[ForeignKey("EmployeeId")]
        //public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Leave Type is required.")]
        public string LeaveType { get; set; } 

        public string? Reason { get; set; } 
    }
}
