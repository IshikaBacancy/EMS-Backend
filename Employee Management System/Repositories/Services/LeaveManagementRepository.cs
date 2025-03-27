using Employee_Management_System.Data;
using Employee_Management_System.DTOs.LeaveManagementDTOs;
using Employee_Management_System.Models;
using Employee_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repositories.Services
{
    public class LeaveManagementRepository : ILeaveManagementRepository
    {
        private readonly DataContext _context;

        public LeaveManagementRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> RegisterLeaveAsync(Leave leave)
        {
            try
            {
                await _context.Leaves.AddAsync(leave);
                await _context.SaveChangesAsync();
                return "Leave Registered Successfully..";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error registering leave";
            }
        }
        public async Task<List<LeaveResponseDTO>> GetLeaveAllEmployeesAsync()
        {
            try
            {
                var leaves = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .Include(e => e.Leaves)
                    .Select(lr => new LeaveResponseDTO
                    {
                        EmployeeId = lr.EmployeeId,
                        FirstName = lr.User.FirstName,
                        LastName = lr.User.LastName,
                        DepartmentName = lr.Department.DepartmentName,
                        LeaveSheetDetails = lr.Leaves
                        .Select(leave => new LeaveSheetDetailsDTO
                        {
                            StartDate = leave.StartDate,
                            EndDate = leave.EndDate,
                            LeaveType = leave.LeaveType,
                            Reason = leave.Reason,
                            Status = leave.Status,
                            AppliedAt = leave.AppliedAt,

                        }).ToList()
                    }).ToListAsync();

                return leaves;
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
                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.UserId == id);

                var leavesApplication = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .Include(e => e.timesheets)
                    .Where(e => e.EmployeeId == employee.EmployeeId)
                    .Select(lr => new LeaveResponseDTO
                    {
                        EmployeeId = lr.EmployeeId,
                        FirstName = lr.User.FirstName,
                        LastName = lr.User.LastName,
                        DepartmentName = lr.Department.DepartmentName,
                        LeaveSheetDetails = lr.Leaves.Select(leave => new LeaveSheetDetailsDTO
                        {
                            StartDate = leave.StartDate,
                            EndDate = leave.EndDate,
                            LeaveType = leave.LeaveType,
                            Reason = leave.Reason,
                            Status = leave.Status,
                            AppliedAt = leave.AppliedAt,
                        })
                        .ToList()
                    }
                    ).ToListAsync();
                return leavesApplication;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Leaves)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, string status)
        {
            try
            {
                var leave = await _context.Leaves.FindAsync(leaveId);
                if (leave == null)
                    return false;

                leave.Status = status;
                _context.Leaves.Update(leave);
                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }




    }
}


