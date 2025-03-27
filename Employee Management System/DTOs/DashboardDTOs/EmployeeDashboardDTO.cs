using Employee_Management_System.DTOs.TimesheetDTOs;

namespace Employee_Management_System.DTOs.DashboardDTOs
{
    public class EmployeeDashboardDTO
    {
        public int EmployeeId { get; set; }
        public double TotalLoggedHours { get; set; }
        
        public List<TimeSheetResponseDTO> LatestTimesheets { get; set; }
    }
}
