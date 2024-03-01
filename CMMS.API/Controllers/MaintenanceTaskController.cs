using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceTaskController : ControllerBase
    {
        private readonly IMaintenanceTaskService _maintenanceTaskService;

        public MaintenanceTaskController(IMaintenanceTaskService maintenanceTaskService)
        {
            _maintenanceTaskService = maintenanceTaskService;
        }

        // Tüm MaintenanceTask kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllMaintenanceTasks()
        {
            var maintenanceTasks = await _maintenanceTaskService.GetAllMaintenanceTasksAsync();
            return Ok(maintenanceTasks);
        }

        // Belirli bir ID'ye sahip MaintenanceTask kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaintenanceTaskById(int id)
        {
            var maintenanceTask = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (maintenanceTask == null)
            {
                return NotFound();
            }
            return Ok(maintenanceTask);
        }

        // Yeni bir MaintenanceTask kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateMaintenanceTask([FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMaintenanceTask = await _maintenanceTaskService.CreateMaintenanceTaskAsync(maintenanceTaskDto);
            return CreatedAtAction(nameof(GetMaintenanceTaskById), new { id = createdMaintenanceTask.Id }, createdMaintenanceTask);
        }

        // Var olan bir MaintenanceTask kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenanceTask(int id, [FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maintenanceTaskDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingMaintenanceTask = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (existingMaintenanceTask == null)
            {
                return NotFound();
            }

            await _maintenanceTaskService.UpdateMaintenanceTaskAsync(maintenanceTaskDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip MaintenanceTask kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceTask(int id)
        {
            var maintenanceTaskToDelete = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);
            if (maintenanceTaskToDelete == null)
            {
                return NotFound();
            }

            await _maintenanceTaskService.DeleteMaintenanceTaskAsync(id);
            return NoContent();
        }
    }
}
