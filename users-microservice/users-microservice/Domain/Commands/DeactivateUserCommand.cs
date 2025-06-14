using CSharpFunctionalExtensions;
using users_microservice.Domain.Contracts;

namespace users_microservice.Domain.Commands;

public class DeactivateUserCommand(Guid id)
{
    public async Task<Result<User?>> Execute(IUserRepository repository)
    {
        var user = await repository.Load(id);
        
        if (user is null)
            return Result.Failure<User?>("USER_NOT_FOUND");
        
        var result = user.Deactivate();
        
        if (result.IsFailure)
            return result!;
        
        return user;
    }
}