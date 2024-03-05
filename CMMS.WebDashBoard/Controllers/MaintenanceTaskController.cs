using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class MaintenanceTaskController : Controller
    {
        private readonly IMaintenanceTaskService _maintenanceTaskService;

        public MaintenanceTaskController(IMaintenanceTaskService maintenanceTaskService)
        {
            _maintenanceTaskService = maintenanceTaskService;
        }

        // GET: MaintenanceTask
        public async Task<IActionResult> Index()
        {
            var maintenanceTasks = await _maintenanceTaskService.GetAllMaintenanceTasksAsync();
            return View(maintenanceTasks);
        }

        // GET: MaintenanceTask/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var maintenanceTask = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (maintenanceTask == null)
            {
                return NotFound();
            }
            return View(maintenanceTask);
        }

        // GET: MaintenanceTask/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaintenanceTaskDto maintenanceTaskDto)
        {
            if (ModelState.IsValid)
            {
                await _maintenanceTaskService.CreateMaintenanceTaskAsync(maintenanceTaskDto);
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceTaskDto);
        }

        // GET: MaintenanceTask/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var maintenanceTask = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (maintenanceTask == null)
            {
                return NotFound();
            }
            return View(maintenanceTask);
        }

        // POST: MaintenanceTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaintenanceTaskDto maintenanceTaskDto)
        {
            if (id != maintenanceTaskDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _maintenanceTaskService.UpdateMaintenanceTaskAsync(maintenanceTaskDto);
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceTaskDto);
        }

        // GET: MaintenanceTask/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var maintenanceTask = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (maintenanceTask == null)
            {
                return NotFound();
            }
            return View(maintenanceTask);
        }

        // POST: MaintenanceTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _maintenanceTaskService.DeleteMaintenanceTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
