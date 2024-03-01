using AutoMapper;
using CMMS.Business.Interfaces;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IMapper _mapper;

        public ContractService(IContractRepository contractRepository, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractDto>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        public async Task<ContractDto> GetContractByIdAsync(int contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);
            return _mapper.Map<ContractDto>(contract);
        }

        public async Task<ContractDto> CreateContractAsync(ContractDto contractDto)
        {
            var contract = _mapper.Map<Contract>(contractDto);
            await _contractRepository.AddAsync(contract);
            return _mapper.Map<ContractDto>(contract);
        }

        public async Task UpdateContractAsync(ContractDto contractDto)
        {
            var contract = _mapper.Map<Contract>(contractDto);
            await _contractRepository.UpdateAsync(contract);
        }

        public async Task DeleteContractAsync(int contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);
            if (contract != null)
            {
                await _contractRepository.DeleteAsync(contract);
            }
        }
    }
}
