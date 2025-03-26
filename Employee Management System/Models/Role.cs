using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class Role
    {
        [Key]
        [Required(ErrorMessage = "Role Id is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [MaxLength(40, ErrorMessage = "Role Name cannot exceed 40 characters.")]
        public string RoleName { get; set; }

        //Navigation Property
        public List<User> Users { get; set; } = new List<User>();
    }
}
