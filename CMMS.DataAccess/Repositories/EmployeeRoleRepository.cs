using CMMS.DataAccess.Context;
using CMMS.DataAccess.Repositories;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class EmployeeRoleRepository : Repository<EmployeeRole>, IEmployeeRoleRepository
    {
        private readonly CMMSDbContext _context;

        public EmployeeRoleRepository(CMMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeRole>> GetEmployeesByRoleIdAsync(int roleId)
        {
            return await _context.Set<EmployeeRole>().Where(x=>x.RoleId==roleId).ToListAsync();    
        }

        public async Task<EmployeeRole> FindByEmployeeIdAndRoleIdAsync(int employeeId, int roleId)
        {
            return await _context.Set<EmployeeRole>().FirstOrDefaultAsync(er => er.EmployeeId == employeeId && er.RoleId == roleId);
        }
    }
}



   