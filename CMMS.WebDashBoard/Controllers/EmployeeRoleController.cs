using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class EmployeeRoleController : Controller
    {
        private readonly IEmployeeRoleService _employeeRoleService;

        public EmployeeRoleController(IEmployeeRoleService employeeRoleService)
        {
            _employeeRoleService = employeeRoleService;
        }

        // GET: EmployeeRole
        public async Task<IActionResult> Index()
        {
            var employeeRoles = await _employeeRoleService.GetAllEmployeeRolesAsync();
            return View(employeeRoles);
        }

        // GET: EmployeeRole/Assign
        public IActionResult Assign()
        {
            // You might need to pass employee and role data to the view for selection
            return View();
        }

        // POST: EmployeeRole/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(EmployeeRoleDto employeeRoleDto)
        {
            if (ModelState.IsValid)
            {
                await _employeeRoleService.AssignRoleToEmployeeAsync(employeeRoleDto);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeRoleDto);
        }

        // GET: EmployeeRole/Remove/{employeeId}/{roleId}
        public async Task<IActionResult> Remove(int employeeId, int roleId)
        {
            // This method might display a confirmation view before removing the role
            var employeeRole = await _employeeRoleService.GetEmployeeRoleByIdsAsync(employeeId, roleId);
            if (employeeRole == null)
            {
                return NotFound();
            }
            return View(employeeRole);
        }

        // POST: EmployeeRole/RemoveConfirmed/{employeeId}/{roleId}
        [HttpPost, ActionName("RemoveConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int employeeId, int roleId)
        {
            await _employeeRoleService.RemoveRoleFromEmployeeAsync(employeeId, roleId);
            return RedirectToAction(nameof(Index));
        }
    }
}
