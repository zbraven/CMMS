using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        private readonly IEmployeeRoleService _employeeRoleService;

        public EmployeeRoleController(IEmployeeRoleService employeeRoleService)
        {
            _employeeRoleService = employeeRoleService;
        }

        // Bir Employee'ye Role atar
        [HttpPost]
        public async Task<IActionResult> AssignRoleToEmployee([FromBody] EmployeeRoleDto employeeRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeRoleService.AssignRoleToEmployeeAsync(employeeRoleDto);
            return Ok();
        }

        // Bir Employee'den Role kaldırır
        [HttpDelete]
        public async Task<IActionResult> RemoveRoleFromEmployee(int employeeId, int roleId)
        {
            await _employeeRoleService.RemoveRoleFromEmployeeAsync(employeeId, roleId);
            return Ok();
        }
    }
}
