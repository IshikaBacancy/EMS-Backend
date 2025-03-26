using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    [Table("Leave")]
    public class Leave
    {
        [Key]
        public int LeaveId { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Column(TypeName = "DATE")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Column(TypeName = "DATE")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Leave type is required")]
        [StringLength(50)]
        public string LeaveType { get; set; }
        public string? Reason { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; 
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key Relationship
 
        public virtual Employee Employee { get; set; }

    }
}
