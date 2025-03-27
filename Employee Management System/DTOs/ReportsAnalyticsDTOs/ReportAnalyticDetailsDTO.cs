using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.DTOs.ReportsAnalyticsDTOs
{
    public class ReportAnalyticDetailsDTO
    {
        [Required(ErrorMessage = "Start time is required.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Total hours are required.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal TotalHours { get; set; }

        


    }
}
