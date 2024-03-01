using CMMS.DataAccess.Context;
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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly CMMSDbContext _context;

        public RoleRepository(CMMSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetRolesByEmployeeIdAsync(int employeeId)
        {
            return await _context.Set<EmployeeRole>().Where(x => x.EmployeeId == employeeId).Select(y => y.Role).ToListAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _context.Set<Role>().FirstOrDefaultAsync(x => x.Name == name);
        }
    }

}
