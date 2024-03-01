using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface ILocationRepository :IRepository<Location>
    {
        Task<Location> GetLocationAssets(int id);
        Task<IEnumerable<Location>> GetLocationsByCityAsync(string city);
        Task<IEnumerable<Location>> GetLocationsByNameAsync(string name);
        Task<IEnumerable<Location>> GetLocationsByDistrictAsync(string district); 
    }
}
