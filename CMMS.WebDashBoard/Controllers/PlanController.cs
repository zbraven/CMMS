using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        // GET: Plan
        public async Task<IActionResult> Index()
        {
            var plans = await _planService.GetAllPlansAsync();
            return View(plans);
        }

        // GET: Plan/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // GET: Plan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanDto planDto)
        {
            if (ModelState.IsValid)
            {
                await _planService.CreatePlanAsync(planDto);
                return RedirectToAction(nameof(Index));
            }
            return View(planDto);
        }

        // GET: Plan/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Plan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanDto planDto)
        {
            if (id != planDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _planService.UpdatePlanAsync(planDto);
                return RedirectToAction(nameof(Index));
            }
            return View(planDto);
        }

        // GET: Plan/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _planService.DeletePlanAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
