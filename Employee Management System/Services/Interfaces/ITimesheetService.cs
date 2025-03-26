using Employee_Management_System.DTOs.TimesheetDTOs;

namespace Employee_Management_System.Services.Interfaces
{
    public interface ITimesheetService
    {
        Task<List<TimeSheetResponseDTO>> GetAllTimeSheetAsync();

        Task<List<TimeSheetResponseDTO>> GetTimeSheetsByIdAsync(int id);

        Task<string> RegisterTimeSheetAsync(TimeSheetRegistrationDTO timeSheetDto, int id);
        Task<string> UpdateTimeSheetAsync(TimeSheetUpdateDTO timeSheetDto, int id);
    }
}
