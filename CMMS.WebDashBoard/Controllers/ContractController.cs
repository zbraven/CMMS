using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // GET: Contract
        public async Task<IActionResult> Index()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return View(contracts);
        }

        // GET: Contract/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // GET: Contract/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractDto contractDto)
        {
            if (ModelState.IsValid)
            {
                await _contractService.CreateContractAsync(contractDto);
                return RedirectToAction(nameof(Index));
            }
            return View(contractDto);
        }

        // GET: Contract/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContractDto contractDto)
        {
            if (id != contractDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _contractService.UpdateContractAsync(contractDto);
                return RedirectToAction(nameof(Index));
            }
            return View(contractDto);
        }

        // GET: Contract/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contractService.DeleteContractAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
