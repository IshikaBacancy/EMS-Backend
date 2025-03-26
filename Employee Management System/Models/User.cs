using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Employee_Management_System.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role Id is required.")]
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }



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
        public string Phone {  get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
        [MaxLength(256, ErrorMessage = "Password cannot exceed 256 characters.")]
        public string Password { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Navigation Property
        public Role Role { get; set; }
    }
}
