using CSharpFunctionalExtensions;
using users_microservice.Domain;
using users_microservice.Domain.Commands;
using users_microservice.Domain.Contracts;

namespace users_microservice.Application;

public class UpdateUserCommandHandler(IUserRepository repository)
{
    public async Task<Result<User?>> Handle(UpdateUserCommand command)
    {
        return await command.Execute(repository);
    }
}