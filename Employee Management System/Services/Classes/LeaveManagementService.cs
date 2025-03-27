using Employee_Management_System.Data;
using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.DTOs.TimesheetDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Employee_Management_System.Repositories.Services;
using Employee_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Services.Classes
{
    public class LeaveManagementService : ILeaveManagementService
    {
        private readonly ILeaveManagementRepository _leaveManagementRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveManagementService(ILeaveManagementRepository leaveManagementRepository, IEmployeeRepository employeeRepository)
        {
            _leaveManagementRepository = leaveManagementRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<string> RegisterLeaveAsync(LeaveRegistrationDTO leaveRegistrationDTO, int id)
        {
            try
            {
                if (leaveRegistrationDTO == null)
                {
                    throw new Exception("Please enter a valid Leave Application Object!..");
                }

                

                if (leaveRegistrationDTO.StartDate > leaveRegistrationDTO.EndDate)
                {
                    throw new Exception("Start Date should be before than End Date!..");
                }

               
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    throw new Exception("Employee Not Found!");
                }

               
                var leave = new Leave
                {
                    EmployeeId = employee.EmployeeId,
                    StartDate = leaveRegistrationDTO.StartDate,
                    EndDate = leaveRegistrationDTO.EndDate,
                    LeaveType = leaveRegistrationDTO.LeaveType,
                    Reason = leaveRegistrationDTO.Reason,
                };
                return await _leaveManagementRepository.RegisterLeaveAsync(leave);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "An unexpected error occurred. Please try again later";

            }
        }

        public async Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync()
        {
            try
            {
               

                return await _leaveManagementRepository.GetLeaveAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;


            }



        }

        public async Task<List<LeaveResponseDTO>> GetLeavesByIdAsync(int id)
        {
            try
            {
                
                return await _leaveManagementRepository.GetLeavesByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }

        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, string status)
        {
            var validStatuses = new List<string> { "Pending", "Approved", "Rejected" };

            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid leave status. Allowed values: Pending, Approved, Rejected.");
            }

            return await _leaveManagementRepository.UpdateLeaveStatusAsync(leaveId, status);
        }
    }

}
