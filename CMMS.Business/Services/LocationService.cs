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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto> GetLocationByIdAsync(int locationId)
        {
            var location = await _locationRepository.GetByIdAsync(locationId);
            return _mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> CreateLocationAsync(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _locationRepository.AddAsync(location);
            return _mapper.Map<LocationDto>(location);
        }

        public async Task UpdateLocationAsync(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _locationRepository.UpdateAsync(location);
        }

        public async Task DeleteLocationAsync(int locationId)
        {
            var location = await _locationRepository.GetByIdAsync(locationId);
            if (location != null)
            {
                await _locationRepository.DeleteAsync(location);
            }
        }
    }
}
