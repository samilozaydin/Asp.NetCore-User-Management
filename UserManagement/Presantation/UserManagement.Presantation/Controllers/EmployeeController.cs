using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Abstractions.Token;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Repositories;
using UserManagement.Presantation.Models.ViewModels.EmployeeVM;

namespace UserManagement.Presantation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IJobReadRepository _jobReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private IEmployeeWriteRepository _employeeWriteRepository;
        private ITokenHandler _handler;
        public EmployeeController(IEmployeeReadRepository employeeReadRepository, 
            IJobReadRepository jobReadRepository,
            IDepartmentReadRepository departmentReadRepository,
            IEmployeeWriteRepository employeeWriteRepository,
            ITokenHandler handler)
        {
            _employeeReadRepository = employeeReadRepository;
            _jobReadRepository = jobReadRepository;
            _departmentReadRepository = departmentReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
            _handler = handler;
        }

        [HttpGet]
        [Route("[controller]/Graphical")]
        [Route("[controller]")]
        [Route("[controller]/Index")]
        public async Task<IActionResult> Index()
        {
            var accessToken  = Request.Cookies["JwtAccessToken"];
            var expiration = Request.Cookies["JwtExpirationTime"];
            SecurityToken token;
            try { 
            token = _handler.VerifyAccessToken(accessToken,expiration);
            }
            catch (System.Exception)
            {
                return Unauthorized();
            }

            EmployeeMainVM EmployeeVM = new EmployeeMainVM();
            
            EmployeeVM.Employee = await _employeeReadRepository.GetByIdAsync(Request.Cookies["UserId"]);
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
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            List<string> departments = await _departmentReadRepository.Table
                .Select(dep => dep.DepartmentName).ToListAsync();

            List<SelectListItem> Departments = new List<SelectListItem>();
            foreach(var element in departments)
            {
                Departments.Add(new SelectListItem
                { Value = element, 
                  Text = element });
            }

            List<string> jobs = await _jobReadRepository.Table
                .Select(job => job.JobTitle).ToListAsync();

            List<SelectListItem> Jobs = new List<SelectListItem>();
            foreach (var element in jobs)
            {
                Jobs.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }


            ViewData["Departments"] = Departments;
            ViewData["Jobs"] = Jobs;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(IFormCollection data)
        {
            string FirstName = data["FirstName"];
            string LastName = data["LastName"];
            string Email = data["Email"];
            string PhoneNumber = data["PhoneNumber"];
            string Salary = data["Salary"];
            string Commission = data["Commission"];
            string Department = data["Department"];
            string Job = data["Job"];

            
            Department department = await _departmentReadRepository
                .GetWhere(department => department.DepartmentName.Equals(Department))
                .FirstOrDefaultAsync();

            int ManagerId = department.ManagerId;
            int depid = department.Id;
            int jobid = await _jobReadRepository
                .GetWhere(job => job.JobTitle.Equals(Job))
                .Select(job => job.Id)
                .FirstOrDefaultAsync();

            Employee employee = new Employee()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = PhoneNumber,
                CommissionPCT= double.Parse(Commission),
                Salary = int.Parse(Salary),
                DepartmentId= depid,
                JobId= jobid,
                HireDate = DateTime.Now,
                ManagerId=ManagerId
            };

           await _employeeWriteRepository.AddAsync(employee);
           await _employeeWriteRepository.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}
