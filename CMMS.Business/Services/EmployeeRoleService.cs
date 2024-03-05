using AutoMapper;
using CMMS.Business.Interfaces;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IMapper _mapper;

        public EmployeeRoleService(IEmployeeRoleRepository employeeRoleRepository, IMapper mapper)
        {
            _employeeRoleRepository = employeeRoleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeRoleDto>> GetAllEmployeeRolesAsync()
        {
            var employeeRoles = await _employeeRoleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeRoleDto>>(employeeRoles);
        }

        public async Task AssignRoleToEmployeeAsync(EmployeeRoleDto employeeRoleDto)
        {
            var employeeRole = _mapper.Map<EmployeeRole>(employeeRoleDto);
            await _employeeRoleRepository.AddAsync(employeeRole);
        }

        public async Task RemoveRoleFromEmployeeAsync(int employeeId, int roleId)
        {
            var employeeRole = await _employeeRoleRepository.FindByEmployeeIdAndRoleIdAsync(employeeId, roleId);
            if (employeeRole != null)
            {
                await _employeeRoleRepository.DeleteAsync(employeeRole);
            }
        }

        public Task<EmployeeRoleDto> GetEmployeeRoleByIdsAsync(int employeeId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
