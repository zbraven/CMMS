using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllContractsAsync();
        Task<ContractDto> GetContractByIdAsync(int contractId);
        Task<ContractDto> CreateContractAsync(ContractDto contractDto);
        Task UpdateContractAsync(ContractDto contractDto);
        Task DeleteContractAsync(int contractId);
    }
}
