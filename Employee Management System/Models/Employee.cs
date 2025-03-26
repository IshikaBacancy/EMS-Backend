using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }


        [ForeignKey("User")]
        [Required(ErrorMessage = "User Id is required.")]
        public int UserId { get; set; }

        [ForeignKey("DepartmentId")]
        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }
        public string? TechStack { get; set; }
        

        //Navigation Property
         public virtual User User { get; set; }
        public virtual Department Department { get; set; }

        //One-Many relatiosnhip with Employees
        public List<Timesheet> timesheets { get; set; }

        public List<Leave> Leaves { get; set; }

    }
}
