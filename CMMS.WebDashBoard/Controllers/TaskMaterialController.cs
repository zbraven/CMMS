using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class TaskMaterialController : Controller
    {
        private readonly ITaskMaterialService _taskMaterialService;

        public TaskMaterialController(ITaskMaterialService taskMaterialService)
        {
            _taskMaterialService = taskMaterialService;
        }

        // GET: TaskMaterial
        public async Task<IActionResult> Index()
        {
            var taskMaterials = await _taskMaterialService.GetAllTaskMaterialsAsync();
            return View(taskMaterials);
        }

        // GET: TaskMaterial/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var taskMaterial = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (taskMaterial == null)
            {
                return NotFound();
            }
            return View(taskMaterial);
        }

        // GET: TaskMaterial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskMaterial/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskMaterialDto taskMaterialDto)
        {
            if (ModelState.IsValid)
            {
                await _taskMaterialService.CreateTaskMaterialAsync(taskMaterialDto);
                return RedirectToAction(nameof(Index));
            }
            return View(taskMaterialDto);
        }

        // GET: TaskMaterial/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var taskMaterial = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (taskMaterial == null)
            {
                return NotFound();
            }
            return View(taskMaterial);
        }

        // POST: TaskMaterial/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskMaterialDto taskMaterialDto)
        {
            if (id != taskMaterialDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _taskMaterialService.UpdateTaskMaterialAsync(taskMaterialDto);
                return RedirectToAction(nameof(Index));
            }
            return View(taskMaterialDto);
        }

        // GET: TaskMaterial/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var taskMaterial = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (taskMaterial == null)
            {
                return NotFound();
            }
            return View(taskMaterial);
        }

        // POST: TaskMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskMaterialService.DeleteTaskMaterialAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
