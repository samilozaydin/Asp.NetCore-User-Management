using UserManagement.Domain.Entities;

namespace UserManagement.Presantation.Models.ViiewModels
{
    public class EmployeeMainVM
    {
        public Employee Employee { get; set; }
        public string DepartmentName { get; set; }
        public string JobName { get; set; }
        public int SameDepartment { get; set; }
        public int DepartmentAmount { get; set; }
        public int SameJob { get; set; }

        public Employee Manager { get; set; }

    }
}
