using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Location
        // This action retrieves all locations and displays them.
        public async Task<IActionResult> Index()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return View(locations);
        }

        // GET: Location/Details/5
        // This action shows the details of a specific location.
        public async Task<IActionResult> Details(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // GET: Location/Create
        // This action displays a form to create a new location.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // This action handles the submission of the location creation form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationDto locationDto)
        {
            if (ModelState.IsValid)
            {
                await _locationService.CreateLocationAsync(locationDto);
                return RedirectToAction(nameof(Index));
            }
            return View(locationDto);
        }

        // GET: Location/Edit/5
        // This action displays a form to edit an existing location.
        public async Task<IActionResult> Edit(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Location/Edit/5
        // This action handles the submission of the location editing form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationDto locationDto)
        {
            if (id != locationDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _locationService.UpdateLocationAsync(locationDto);
                return RedirectToAction(nameof(Index));
            }
            return View(locationDto);
        }

        // GET: Location/Delete/5
        // This action displays a confirmation page for deleting a location.
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Location/Delete/5
        // This action handles the confirmation of location deletion.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
