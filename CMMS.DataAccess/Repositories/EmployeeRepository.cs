using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly CMMSDbContext _context;

        public EmployeeRepository(CMMSDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department)
        {
            return await _context.Set<Employee>().Where(x=>x.Department==department).ToListAsync();
        }

      

        public async Task<IEnumerable<Employee>> GetActiveUsersAsync()
        {
            return await _context.Set<Employee>().Where(e => e.IsActive).ToListAsync();
        }

        public async Task<int> CountActiveUsersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Employee>()
                                 .CountAsync(e => e.IsActive && e.DateOfRecruitment >= startDate && e.DateOfRecruitment <= endDate);
        }
    }
}
