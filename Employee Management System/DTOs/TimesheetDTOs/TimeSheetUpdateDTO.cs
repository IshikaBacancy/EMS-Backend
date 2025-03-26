using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.TimesheetDTOs
{
    public class TimeSheetUpdateDTO
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [Column(TypeName = "DATE")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public TimeOnly EndTime { get; set; }

        public string? Description { get; set; }
    }
}
