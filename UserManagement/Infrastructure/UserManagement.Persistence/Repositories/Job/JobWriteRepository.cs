﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Contexts;

namespace UserManagement.Persistence.Repositories
{
    public class JobWriteRepository : WriteRepository<Job>,IJobWriteRepository
    {
        public JobWriteRepository(UserManagementDbContext context) : base(context) { }
    }
}
