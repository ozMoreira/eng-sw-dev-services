using CSharpFunctionalExtensions;
using users_microservice.Domain;
using users_microservice.Domain.Commands;
using users_microservice.Domain.Contracts;

namespace users_microservice.Application;

public class DeactivateUserCommandHandler(IUserRepository repository)
{
    public async Task<Result<User?>> Handle(DeactivateUserCommand command)
    {
        return await command.Execute(repository);
    }
}