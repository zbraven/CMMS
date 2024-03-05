using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class ActivityLogController : Controller
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        // GET: ActivityLog
        public async Task<IActionResult> Index()
        {
            var activityLogs = await _activityLogService.GetAllActivityLogsAsync();
            return View(activityLogs);
        }

        // GET: ActivityLog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var activityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return View(activityLog);
        }

        // GET: ActivityLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivityLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityLogDto activityLogDto)
        {
            if (ModelState.IsValid)
            {
                await _activityLogService.CreateActivityLogAsync(activityLogDto);
                return RedirectToAction(nameof(Index));
            }
            return View(activityLogDto);
        }

        // GET: ActivityLog/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var activityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return View(activityLog);
        }

        // POST: ActivityLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActivityLogDto activityLogDto)
        {
            if (id != activityLogDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _activityLogService.UpdateActivityLogAsync(activityLogDto);
                return RedirectToAction(nameof(Index));
            }
            return View(activityLogDto);
        }

        // GET: ActivityLog/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var activityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return View(activityLog);
        }

        // POST: ActivityLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _activityLogService.DeleteActivityLogAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
