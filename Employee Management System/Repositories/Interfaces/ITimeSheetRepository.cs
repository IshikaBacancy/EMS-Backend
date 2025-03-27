using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;

namespace Employee_Management_System.Repositories.Interfaces
{
    public interface ITimeSheetRepository
    {
        Task<List<TimeSheetResponseDTO>> GetAllTimeSheetAsync();

        Task<List<TimeSheetResponseDTO>> GetTimeSheetsByIdAsync(int id);

        Task<string> RegisterTimeSheetAsync(Timesheet timeSheets);

        // for updates
        Task<Timesheet> GetTimeSheetByIdAsync(int timesheetId, int employeeId);

        Task UpdateTimeSheetAsync(Timesheet timeSheets);

       
    }
}

