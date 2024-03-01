using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        // Tüm Plan kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        // Belirli bir ID'ye sahip Plan kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
        }

        // Yeni bir Plan kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] PlanDto planDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPlan = await _planService.CreatePlanAsync(planDto);
            return CreatedAtAction(nameof(GetPlanById), new { id = createdPlan.Id }, createdPlan);
        }

        // Var olan bir Plan kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] PlanDto planDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != planDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingPlan = await _planService.GetPlanByIdAsync(id);
            if (existingPlan == null)
            {
                return NotFound();
            }

            await _planService.UpdatePlanAsync(planDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip Plan kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var planToDelete = await _planService.GetPlanByIdAsync(id);
            if (planToDelete == null)
            {
                return NotFound();
            }

            await _planService.DeletePlanAsync(id);
            return NoContent();
        }

    }
}
