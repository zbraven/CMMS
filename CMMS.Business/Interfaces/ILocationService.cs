using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> GetLocationByIdAsync(int locationId);
        Task<LocationDto> CreateLocationAsync(LocationDto locationDto);
        Task UpdateLocationAsync(LocationDto locationDto);
        Task DeleteLocationAsync(int locationId);
    }
}
