using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Employee_Management_System.Models
{
    [Table("Timesheet")]
    public class Timesheet
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "Total hours is required")]
        [Column(TypeName = "DECIMAL(5,2)")]
        public decimal TotalHours { get; set; }
        public string? Description { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

    }
}
