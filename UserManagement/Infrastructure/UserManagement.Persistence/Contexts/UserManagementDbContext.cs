using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.Contexts
{
    public class UserManagementDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }

        public UserManagementDbContext (DbContextOptions options) : base(options) {
            
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<JobHistory>().HasKey(jobHistory => jobHistory.Id);
            modelBuilder.Entity<JobHistory>().HasOne(context=> context.Employee).
                WithMany(context => context.JobHistories).
                HasForeignKey(key => key.Id).OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<User>().HasKey(user => user.Id);
           
           modelBuilder.Entity<Employee>().HasOne(employee => employee.User)
                .WithOne(user => user.Employee)
                .HasForeignKey<User>(key=> key.Id);
            

        }
    }
}
