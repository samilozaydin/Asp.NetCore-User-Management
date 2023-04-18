using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Presantation.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationReadRepository _locationReadRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly IRegionReadRepository _regionReadRepository;
        private readonly IEmployeeReadRepository _employeeReadRepository;
        public LocationController(ILocationReadRepository locationReadRepository,
            ICountryReadRepository countryReadRepository,
            IRegionReadRepository regionReadRepository,
            IEmployeeReadRepository employeeReadRepository)
        {
            _locationReadRepository = locationReadRepository;
            _countryReadRepository = countryReadRepository;
            _regionReadRepository = regionReadRepository;
            _employeeReadRepository = employeeReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Employee employee = await _employeeReadRepository.GetByIdAsync(Request.Cookies["UserId"]);

            List<Location> Locations = await _locationReadRepository.Table
                .Include(location => location.Country)
                .Include(location => location.Country.Region)
                .ToListAsync();

            ViewData["Locations"] = Locations;
            ViewData["Employee"] = employee;

            return View();
        }
    }
}
