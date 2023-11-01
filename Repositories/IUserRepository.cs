using MyProject.Models.Domain;

namespace MyProject.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetAsync(Guid Id);

        Task<User> AddAsync(User user);

        Task<User?> UpdateAsync(User user);

        Task<User?> DeleteAsync(Guid Id);    
    }
}
