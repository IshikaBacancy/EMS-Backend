using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //One-Many relationship
        public List<Employee> Employees { get; set; }
    }
}
