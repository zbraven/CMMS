using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        // Tüm ActivityLog kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllActivityLogs()
        {
            var activityLogs = await _activityLogService.GetAllActivityLogsAsync();
            return Ok(activityLogs);
        }

        // Belirli bir ID'ye sahip ActivityLog kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityLogById(int id)
        {
            var activityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return Ok(activityLog);
        }

        // Yeni bir ActivityLog kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateActivityLog([FromBody] ActivityLogDto activityLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdActivityLog = await _activityLogService.CreateActivityLogAsync(activityLogDto);
            return CreatedAtAction(nameof(GetActivityLogById), new { id = createdActivityLog.Id }, createdActivityLog);
        }

        // Var olan bir ActivityLog kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivityLog(int id, [FromBody] ActivityLogDto activityLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityLogDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingActivityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (existingActivityLog == null)
            {
                return NotFound();
            }

            await _activityLogService.UpdateActivityLogAsync(activityLogDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip ActivityLog kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityLog(int id)
        {
            var activityLogToDelete = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLogToDelete == null)
            {
                return NotFound();
            }

            await _activityLogService.DeleteActivityLogAsync(id);
            return NoContent();
        }
    }
}
