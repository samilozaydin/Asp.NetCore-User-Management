using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Contexts;

namespace UserManagement.Persistence.Repositories
{
    public class JobReadRepository : ReadRepository<Job>,IJobReadRepository
    {
        public JobReadRepository(UserManagementDbContext context) : base(context) { }
    }
}
