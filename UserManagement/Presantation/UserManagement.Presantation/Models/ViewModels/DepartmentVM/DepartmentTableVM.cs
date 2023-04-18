using UserManagement.Domain.Entities;

namespace UserManagement.Presantation.Models.ViewModels.DepartmentVM
{
    public class DepartmentTableVM
    {
        public string Department { get; set; }
        public string Manager { get; set; }
        public string City { get; set; }
        public string Country{get; set;}
        public string Region { get; set; }
        public int ManagerId { get; set; }
        public int EmployeeId { get; set; }
    }
}
