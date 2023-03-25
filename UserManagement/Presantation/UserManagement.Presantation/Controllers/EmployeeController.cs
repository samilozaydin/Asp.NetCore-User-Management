using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Repositories;
using UserManagement.Presantation.Models.ViiewModels;

namespace UserManagement.Presantation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IJobReadRepository _jobReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;

        public EmployeeController(IEmployeeReadRepository employeeReadRepository, 
            IJobReadRepository jobReadRepository,
            IDepartmentReadRepository departmentReadRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _jobReadRepository = jobReadRepository;
            _departmentReadRepository = departmentReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            EmployeeMainVM EmployeeVM = new EmployeeMainVM();
            
            EmployeeVM.Employee = await _employeeReadRepository.GetByIdAsync("15");
            //current emplooyee is assumed the one who has 7 as id
            EmployeeVM.Manager = await _employeeReadRepository.GetByIdAsync(EmployeeVM.Employee.ManagerId.ToString());
            EmployeeVM.SameDepartment = _employeeReadRepository.GetWhere(employee => employee.DepartmentId == EmployeeVM.Employee.DepartmentId)
                .ToList().Count;
            EmployeeVM.SameJob = _employeeReadRepository.GetWhere(employee => employee.JobId == EmployeeVM.Employee.JobId)
                .ToList().Count;
            EmployeeVM.DepartmentAmount = _departmentReadRepository.GetAll().ToList().Count;
            EmployeeVM.DepartmentName = (await _departmentReadRepository.GetByIdAsync(EmployeeVM.Employee.DepartmentId.ToString())).DepartmentName;
            EmployeeVM.JobName = (await _jobReadRepository.GetByIdAsync(EmployeeVM.Employee.JobId.ToString())).JobTitle;

            List<Employee> employeeList =  _employeeReadRepository.GetWhere(employee => employee.DepartmentId == EmployeeVM.Employee.DepartmentId).ToList();
            List<EmployeeTableVM> employeeTableList = new List<EmployeeTableVM>();
            foreach (Employee element in employeeList)
            {
                EmployeeTableVM tableElement = new EmployeeTableVM();
                tableElement.Employee = element;
                tableElement.DepartmentName = (await _departmentReadRepository.GetByIdAsync(element.DepartmentId.ToString())).DepartmentName;
                Employee temp = await _employeeReadRepository.GetByIdAsync(element.ManagerId.ToString());
                tableElement.ManagerName = temp.FirstName + " " + temp.LastName;
                employeeTableList.Add(tableElement);
            }

            ViewData["EmployeeVM"] = EmployeeVM;
            ViewData["EmployeeTableVM"] = employeeTableList;
            return View();
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> CreateGraph() {

            List<Department> Departments = _departmentReadRepository.GetAll().ToList();
            int[] departmentsId = new int[Departments.Count];
            string[] departmentsName = new string[Departments.Count];
            for(int i = 0; i < departmentsId.Length; i++)
            {
                departmentsId[i] = Departments.ElementAt(i).Id;
                departmentsName[i] = Departments.ElementAt(i).DepartmentName;
            }

            List<Employee> Employees = _employeeReadRepository.GetAll().ToList();
            foreach(Employee employee in Employees)
            {
                departmentsId[employee.DepartmentId-1]++;
            }
            
            
            List<EmployeeChartVM> list = new List<EmployeeChartVM>();

            for (int i = 0; i < departmentsId.Length; i++)
            {
                EmployeeChartVM temp = new EmployeeChartVM();
                temp.DepartmentId = departmentsId[i] + 1;
                temp.DepartmentName = departmentsName[i];
                list.Add(temp);
            }

            try
            {
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
