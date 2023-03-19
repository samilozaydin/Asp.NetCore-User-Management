using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.Common;

namespace UserManagement.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
        public int LocationId { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Location Location { get; set; }
        public ICollection <JobHistory> JobHistories { get; set; } 

    }
}
