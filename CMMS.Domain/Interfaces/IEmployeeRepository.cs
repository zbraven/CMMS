using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department);
        
        Task<IEnumerable<Employee>> GetActiveUsersAsync();
        Task<int> CountActiveUsersByDateRangeAsync(DateTime startDate, DateTime endDate);


    }
}
