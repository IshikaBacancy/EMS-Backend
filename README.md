# Employee Management System

## Introduction

The Employee Management System (EMS) is a web-based application developed using .NET Core following a multi-tiered architecture. The system is designed to manage employee details, timesheets, leave applications, and reports efficiently. It provides role-based access for employees and admins, ensuring secure authentication and data handling.

## Technical Requirements 
1. Develop the API in .NET Core following a multi-tiered architecture. 
2. Use the Entity Framework Code-First approach (optional but recommended). 
3. Provide a compilable and functioning project. 
4. Ensure proper security and authentication for employees and admins. 
5. If any functionality is not implemented, clearly document the exclusions.

## Endpoints

### Department

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/Department/GetAllDepartments` | Retrieve all departments. |
| POST | `/Department/RegisterDepartment` | Register a new department. |

### Employee

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/Employee/GetAllEmployees` | Retrieve all employees. |
| GET | `/Employee/GetEmployee` | Retrieve a specific employee. |
| PUT | `/Employee/UpdateEmployee` | Update an employee's information. |

### Leave Management

| Method | Endpoint | Description |
|--------|---------|-------------|
| POST | `/LeaveManagement/Registerleave` | Register leave for an employee. |
| GET | `/LeaveManagement/GetLeaveAllEmployees` | Get leave details for all employees. |
| GET | `/LeaveManagement/GetLeave` | Get leave details for a specific employee. |
| PUT | `/LeaveManagement/update-leave-status/{leaveId}` | Update the status of a leave request. |

### Reports Analytics

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/ReportsAnalytics/GetAllEmployeesTimeSheetReportWeekly` | Get weekly timesheet reports for all employees. |
| GET | `/ReportsAnalytics/GetAllEmployeesTimeSheetReportMonthly` | Get monthly timesheet reports for all employees. |

### TimeSheet

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/TimeSheet/GetAllTimeSheets` | Retrieve all timesheets. |
| GET | `/TimeSheet/GetTimeSheets` | Retrieve specific timesheets. |
| POST | `/TimeSheet/RegisterTimesheet` | Register a new timesheet. |
| PUT | `/TimeSheet/UpdateTimesheet` | Update an existing timesheet. |
| GET | `/TimeSheet/ExportTimesheets` | Export timesheet data. |

### User Management

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/User/GetAllUsers` | Retrieve all users. |
| POST | `/User/RegistrationUserAdmin` | Register a new admin user. |
| POST | `/User/RegistrationUserEmployee` | Register a new employee user. |
| PUT | `/User/UpdateUser` | Update user details. |
| PUT | `/User/ActivateUserEmployee` | Activate an employee account. |
| PUT | `/User/DeactivateUserEmployee` | Deactivate an employee account. |

### User Authentication

| Method | Endpoint | Description |
|--------|---------|-------------|
| POST | `/UserAuthentication/LogInUser` | Log in a user. |
| PUT | `/UserAuthentication/ResetPassword` | Reset user password. |
| POST | `/UserAuthentication/ResetPassword` | Reset password (alternative method). |
| POST | `/UserAuthentication/RequestResetPassword` | Request password reset link. |

## Setup Instructions 
1. Clone the repository:  
   sh
   git clone https://github.com/IshikaBacancy/EMS-Backend.git
2. Navigate to the project directory:  
   sh
   cd EMS--Backend-REST-API
   
3. Restore dependencies:  
   sh
   dotnet restore
   
4. Configure the database in appsettings.json.
5. Run database migrations:  
   sh
   dotnet ef database update
   
6. Start the API:  
   sh
   dotnet run
   
7. Access Swagger UI at:  
   
    (https://localhost:7256/swagger/index.html)

## Conclusion

This API serves as the backbone of an Employee Management System (EMS), providing essential features for managing employees, departments, leave requests, timesheets, and user authentication. With a well-structured and secure design, it helps streamline operations and improves efficiency.  







   




