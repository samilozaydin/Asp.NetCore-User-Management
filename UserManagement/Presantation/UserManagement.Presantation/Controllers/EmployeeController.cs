using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Presantation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;

        public EmployeeController(IEmployeeReadRepository employeeReadRepository)
        {
            _employeeReadRepository = employeeReadRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> mylist =  _employeeReadRepository.GetAll().ToList();
            return View(mylist);
        }
    }
}
