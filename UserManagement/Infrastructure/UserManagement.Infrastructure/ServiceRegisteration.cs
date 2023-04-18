using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Abstractions.Token;
using UserManagement.Infrastructure.Services.Token;

namespace UserManagement.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static void AddInfrastructureServices(this IServiceCollection services) {

            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
