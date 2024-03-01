using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // Tüm Material kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        // Belirli bir ID'ye sahip Material kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        // Yeni bir Material kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] MaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMaterial = await _materialService.CreateMaterialAsync(materialDto);
            return CreatedAtAction(nameof(GetMaterialById), new { id = createdMaterial.Id }, createdMaterial);
        }

        // Var olan bir Material kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(int id, [FromBody] MaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingMaterial = await _materialService.GetMaterialByIdAsync(id);
            if (existingMaterial == null)
            {
                return NotFound();
            }

            await _materialService.UpdateMaterialAsync(materialDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip Material kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var materialToDelete = await _materialService.GetMaterialByIdAsync(id);
            if (materialToDelete == null)
            {
                return NotFound();
            }

            await _materialService.DeleteMaterialAsync(id);
            return NoContent();
        }
    }
}
