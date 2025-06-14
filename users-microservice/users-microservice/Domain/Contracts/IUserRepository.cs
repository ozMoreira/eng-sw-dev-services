namespace users_microservice.Domain.Contracts;

public interface IUserRepository
{
    public Task<User> Save(User user);
    public Task<User> Update(User user);
    public Task<User?> Load(Guid id);
    public Task<List<User>> Load(Pagination pagination);
}