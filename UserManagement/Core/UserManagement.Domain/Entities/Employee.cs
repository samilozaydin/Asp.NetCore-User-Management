using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.Common;

namespace UserManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public int JobId { get; set; }
        public int Salary { get; set; }
        public double CommissionPCT { get; set; }
        public int ManagerId { get; set; }
        public int DepartmentId { get; set; }

        public Department Departments { get; set; }
        public Job Job { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }

    }
}
