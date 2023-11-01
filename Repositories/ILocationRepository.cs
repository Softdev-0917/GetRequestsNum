using MyProject.Models.Domain;

namespace MyProject.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<Location?> GetAsync(Guid Id);

        Task<Location> AddAsync(Location location);

        Task<Location?> UpdateAsync(Location location);

        Task<Location?> DeleteAsync(Guid Id);
    }
}
