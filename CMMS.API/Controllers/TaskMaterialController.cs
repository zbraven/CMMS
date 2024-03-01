using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMaterialController : ControllerBase
    {
        private readonly ITaskMaterialService _taskMaterialService;

        public TaskMaterialController(ITaskMaterialService taskMaterialService)
        {
            _taskMaterialService = taskMaterialService;
        }

        // Tüm TaskMaterial kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllTaskMaterials()
        {
            var taskMaterials = await _taskMaterialService.GetAllTaskMaterialsAsync();
            return Ok(taskMaterials);
        }

        // Belirli bir ID'ye sahip TaskMaterial kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskMaterialById(int id)
        {
            var taskMaterial = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (taskMaterial == null)
            {
                return NotFound();
            }
            return Ok(taskMaterial);
        }

        // Yeni bir TaskMaterial kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateTaskMaterial([FromBody] TaskMaterialDto taskMaterialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTaskMaterial = await _taskMaterialService.CreateTaskMaterialAsync(taskMaterialDto);
            return CreatedAtAction(nameof(GetTaskMaterialById), new { id = createdTaskMaterial.Id }, createdTaskMaterial);
        }

        // Var olan bir TaskMaterial kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskMaterial(int id, [FromBody] TaskMaterialDto taskMaterialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskMaterialDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingTaskMaterial = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (existingTaskMaterial == null)
            {
                return NotFound();
            }

            await _taskMaterialService.UpdateTaskMaterialAsync(taskMaterialDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip TaskMaterial kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskMaterial(int id)
        {
            var taskMaterialToDelete = await _taskMaterialService.GetTaskMaterialByIdAsync(id);
            if (taskMaterialToDelete == null)
            {
                return NotFound();
            }

            await _taskMaterialService.DeleteTaskMaterialAsync(id);
            return NoContent();
        }
    }
}
