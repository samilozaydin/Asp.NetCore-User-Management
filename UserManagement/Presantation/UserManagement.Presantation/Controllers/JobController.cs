using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Contexts;
using UserManagement.Presantation.Models.ViewModels.JobVM;

namespace UserManagement.Presantation.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobHistoryReadRepository _jobHistoryReadRepository;
        private readonly IJobReadRepository _jobReadRepository;
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;

        public JobController(IJobHistoryReadRepository jobHistoryReadRepository,
            IJobReadRepository jobReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IDepartmentReadRepository departmentReadRepository)
        {
            _jobHistoryReadRepository = jobHistoryReadRepository;
            _jobReadRepository = jobReadRepository;
            _employeeReadRepository = employeeReadRepository;
            _departmentReadRepository = departmentReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Employee employee = await _employeeReadRepository.GetByIdAsync(Request.Cookies["UserId"]);

            Job Max =  await _jobReadRepository.GetAll().OrderBy(job => job.MaxSalary).FirstOrDefaultAsync();
            Job Min = await _jobReadRepository.GetAll().OrderBy(job => job.MinSalary).FirstOrDefaultAsync();

            JobSalaryVM SalaryVM = new JobSalaryVM();
            SalaryVM.MaxSalary = Max.MaxSalary;
            SalaryVM.MinSalary = Min.MinSalary;

            List<Job> Jobs = await _jobReadRepository.GetAll().ToListAsync();

            List<JobHistory> jobHistories = await _jobHistoryReadRepository.Table.Include(jh => jh.Employee)
                .Include(jh => jh.Department)
                .Include(jh => jh.Job)
                .ToListAsync();

            ViewData["JobHistories"] = jobHistories;
            ViewData["Jobs"] = Jobs;
            ViewData["SalaryVM"] = SalaryVM;
            ViewData["Employee"] = employee;

            return View();
        }
    }
}
