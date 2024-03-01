using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // Tüm Contract kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        // Belirli bir ID'ye sahip Contract kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(contract);
        }

        // Yeni bir Contract kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] ContractDto contractDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdContract = await _contractService.CreateContractAsync(contractDto);
            return CreatedAtAction(nameof(GetContractById), new { id = createdContract.Id }, createdContract);
        }

        // Var olan bir Contract kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] ContractDto contractDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingContract = await _contractService.GetContractByIdAsync(id);
            if (existingContract == null)
            {
                return NotFound();
            }

            await _contractService.UpdateContractAsync(contractDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip Contract kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var contractToDelete = await _contractService.GetContractByIdAsync(id);
            if (contractToDelete == null)
            {
                return NotFound();
            }

            await _contractService.DeleteContractAsync(id);
            return NoContent();
        }
    }
}
