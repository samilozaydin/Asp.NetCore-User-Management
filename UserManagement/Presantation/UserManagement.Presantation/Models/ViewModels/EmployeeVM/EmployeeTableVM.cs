﻿using UserManagement.Domain.Entities;

namespace UserManagement.Presantation.Models.ViewModels.EmployeeVM
{
    public class EmployeeTableVM
    {
        public Employee Employee { get; set; }
        public string ManagerName { get; set; }
        public string DepartmentName { get; set; }
    }
}
