using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models.Domain;

namespace MyProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyProjectDbContext myProjectDbContext;

        public UserRepository(MyProjectDbContext myProjectDbContext)
        {
            this.myProjectDbContext = myProjectDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await myProjectDbContext.AddAsync(user);
            await myProjectDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(Guid Id)
        {
           var existingUser = await myProjectDbContext.Users.FindAsync(Id);

            if (existingUser != null)
            {
                myProjectDbContext.Users.Remove(existingUser);
                await myProjectDbContext.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           return await myProjectDbContext.Users.Include(x => x.Locations).ToListAsync();
        }

        public async Task<User?> GetAsync(Guid Id)
        {
            return await myProjectDbContext.Users.Include(x=> x.Locations).FirstOrDefaultAsync(x=> x.Id == Id);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await myProjectDbContext.Users.Include(x=>x.Locations).FirstOrDefaultAsync(x => x.Id == user.Id);

            if(existingUser != null)
            {
                existingUser.Id = user.Id;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Username = user.Username;
                existingUser.Team = user.Team;
                existingUser.Position = user.Position;
                existingUser.Locations = user.Locations;

                await myProjectDbContext.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }
    }
}
