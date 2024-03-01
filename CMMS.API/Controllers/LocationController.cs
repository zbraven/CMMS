using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // Tüm Location kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        // Belirli bir ID'ye sahip Location kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        // Yeni bir Location kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDto locationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLocation = await _locationService.CreateLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.Id }, createdLocation);
        }

        // Var olan bir Location kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDto locationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingLocation = await _locationService.GetLocationByIdAsync(id);
            if (existingLocation == null)
            {
                return NotFound();
            }

            await _locationService.UpdateLocationAsync(locationDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip Location kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var locationToDelete = await _locationService.GetLocationByIdAsync(id);
            if (locationToDelete == null)
            {
                return NotFound();
            }

            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
    }
}
