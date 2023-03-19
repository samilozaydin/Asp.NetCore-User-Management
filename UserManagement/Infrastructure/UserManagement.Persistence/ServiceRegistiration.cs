using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using UserManagement.Persistence;
using UserManagement.Application.Repositories;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersitanceServices(this IServiceCollection services)
        {
            services.AddDbContext<UserManagementDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<IDepartmentReadRepository,DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            services.AddScoped<ILocationReadRepository, LocationReadRepository>();
            services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();
            services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            services.AddScoped<IJobHistoryReadRepository, JobHistoryReadRepository>();
            services.AddScoped<IJobHistoryWriteRepository, JobHistoryWriteRepository>();
            services.AddScoped<IJobReadRepository, JobReadRepository>();
            services.AddScoped<IJobWriteRepository, JobWriteRepository>();
            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
            services.AddScoped<IRegionReadRepository, RegionReadRepository>();
            services.AddScoped<IRegionWriteRepository, RegionWriteRepository>();


        }
    }
}
