using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{

    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly CMMSDbContext _context;

        public LocationRepository(CMMSDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Location> GetLocationAssets(int id)
        {
            return await _context.Set<Location>().Include(x => x.Assets).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Location>> GetLocationsByCityAsync(string city)
        {
            return await _context.Set<Location>().Where(x => x.LocationCity == city).ToListAsync();
        }

        public async Task<IEnumerable<Location>> GetLocationsByDistrictAsync(string district)
        {
            return await _context.Set<Location>().Where(x => x.LocationDistrict == district).ToListAsync();
        }
        
        public async Task<IEnumerable<Location>> GetLocationsByNameAsync(string name)
        {
            return await _context.Set<Location>().Where(x => x.LocationName.Contains(name)).ToListAsync();
        }
    }
}
