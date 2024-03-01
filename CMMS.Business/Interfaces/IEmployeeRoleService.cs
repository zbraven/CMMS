    using CMMS.Services.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CMMS.Business.Interfaces
    {
        public interface IEmployeeRoleService
        {
            Task<IEnumerable<EmployeeRoleDto>> GetAllEmployeeRolesAsync();
            Task AssignRoleToEmployeeAsync(EmployeeRoleDto employeeRoleDto);
            Task RemoveRoleFromEmployeeAsync(int employeeId, int roleId);
        }
    }
