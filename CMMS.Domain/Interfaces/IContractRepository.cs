using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IContractRepository : IRepository<Contract>
    {
        Task<IEnumerable<Contract>> GetContractsByAssetIdAsync(int assetId);
        Task<IEnumerable<Contract>> GetActiveContractsAsync();
    }
}
