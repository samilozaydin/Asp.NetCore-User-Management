using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.Common;

namespace UserManagement.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string JobTitle { get; set; }
        public int MaxSalary { get; set; }
        public int MinSalary { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }
        public ICollection<Employee> Employees { get; set; } 
    }
}
