using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IEmployeeRoleRepository : IRepository<EmployeeRole>
    {
        Task<IEnumerable<EmployeeRole>> GetEmployeesByRoleIdAsync(int roleId);
        Task<EmployeeRole> FindByEmployeeIdAndRoleIdAsync(int employeeId, int roleId);
    }
}

