using Employee_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        public DbSet<Timesheet> Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
           .HasIndex(e => e.Email)
           .IsUnique();

            modelBuilder.Entity<Department>()
           .HasIndex(d => d.DepartmentName)
           .IsUnique();



            //Adding Check Constraint for LeaveType

            modelBuilder.Entity<Leave>()
           .ToTable(l => l.HasCheckConstraint("CK_Leave_LeaveType", "LeaveType IN ('Sick', 'Casual', 'Vacation', 'Other')"));

             modelBuilder.Entity<Leave>()

            .ToTable(l => l.HasCheckConstraint("CK_Leave_Status", "Status IN ('Pending', 'Approved','Rejected')"));

            modelBuilder.Entity<Timesheet>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
