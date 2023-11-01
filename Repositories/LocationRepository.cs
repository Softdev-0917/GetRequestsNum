using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models.Domain;

namespace MyProject.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MyProjectDbContext myProjectDbContext;

        public LocationRepository(MyProjectDbContext myProjectDbContext)
        {
            this.myProjectDbContext = myProjectDbContext;
        }
        public async Task<Location> AddAsync(Location location)
        {
            await myProjectDbContext.Locations.AddAsync(location);
            await myProjectDbContext.SaveChangesAsync();
            return location;
        }

        public async Task<Location?> DeleteAsync(Guid Id)
        {
            var exisitngLocation = await myProjectDbContext.Locations.FindAsync(Id);

            if (exisitngLocation != null)
            {
                myProjectDbContext.Locations.Remove(exisitngLocation);
                await myProjectDbContext.SaveChangesAsync();
                return exisitngLocation;
            }

            return null;
        }

        public Task<Location?> GetAsync(Guid Id)
        {
            return myProjectDbContext.Locations.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            //return await myProjectDbContext.Locations.ToListAsync();
            //To show user associate with location
            return await myProjectDbContext.Locations.Include(x => x.Users).ToListAsync();
        }

        public async Task<Location?> UpdateAsync(Location location)
        {
            var existingLocation = await myProjectDbContext.Locations.FindAsync(location.Id);

            if (existingLocation != null)
            {
                existingLocation.LocationName = location.LocationName;
                existingLocation.LocationRoom = location.LocationRoom;

                await myProjectDbContext.SaveChangesAsync();
                return existingLocation;
            }
            return null;
        }
    }
}
