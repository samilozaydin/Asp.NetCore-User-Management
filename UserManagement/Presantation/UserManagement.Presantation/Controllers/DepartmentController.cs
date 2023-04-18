using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Xml;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Presantation.Models.ViewModels.DepartmentVM;

namespace UserManagement.Presantation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly ILocationReadRepository _locationReadRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly IRegionReadRepository _regionReadRepository;

        public DepartmentController(IEmployeeReadRepository employeeReadRepository,
            IDepartmentReadRepository departmentReadRepository,
            ILocationReadRepository locationReadRepository,
            ICountryReadRepository countryReadRepository, 
            IRegionReadRepository regionReadRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _departmentReadRepository = departmentReadRepository;
            _locationReadRepository = locationReadRepository;
            _countryReadRepository = countryReadRepository;
            _regionReadRepository = regionReadRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Department()
        {
            Employee employee = await _employeeReadRepository.GetByIdAsync(Request.Cookies["UserId"]);

            List<DepartmentTableVM> deneme =
                await _departmentReadRepository.Table
                .Include(dp => dp.Employees)
                .Include(dp => dp.Location)
                .Include(dp => dp.Location.Country)
                .Include(dp => dp.Location.Country.Region)
                .SelectMany(dp => dp.Employees, (dp, em) =>
                new DepartmentTableVM {
                    Department = dp.DepartmentName,
                    Manager = em.FirstName + " " + em.LastName,
                    City = dp.Location.City,
                    Country = dp.Location.Country.CountryName,
                    Region = dp.Location.Country.Region.RegionName,
                    EmployeeId = em.Id,
                    ManagerId = dp.ManagerId
                }).Where(dp => dp.ManagerId== dp.EmployeeId)
                .ToListAsync();

            ViewData["DepartmentVM"] = deneme;
            ViewData["Employee"] = employee;
            return View();
        }

    }

}
