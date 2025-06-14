using Microsoft.EntityFrameworkCore;
using users_microservice.Domain;
using users_microservice.Domain.Contracts;

namespace users_microservice.Infra;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> Save(User user)
    {
        var userModel = (UserDbModel)user!;
        
        await context.Users.AddAsync(userModel);
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User> Update(User user)
    {
        var userModel = (UserDbModel)user!;
        
        context.Users.Update(userModel);
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> Load(Guid id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return (User?)user;
    }

    public async Task<List<User>> Load(Domain.Contracts.Pagination pagination)
    {
        var skip = (pagination.Page - 1) * pagination.PageSize;   
        var take = pagination.PageSize;
        
        var users = await context
            .Users
            .Skip(skip)
            .Take(take).ToListAsync();
        
        return users.Count == 0 
            ? Enumerable.Empty<User>().ToList() 
            : users.ConvertAll<User>(user => (User)user!);
    }
}