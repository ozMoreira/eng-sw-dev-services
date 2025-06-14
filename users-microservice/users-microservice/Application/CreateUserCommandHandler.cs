using CSharpFunctionalExtensions;
using users_microservice.Domain;
using users_microservice.Domain.Commands;
using users_microservice.Domain.Contracts;

namespace users_microservice.Application;

public class CreateUserCommandHandler(
    IUserRepository userRepository, 
    ILogger<CreateUserCommandHandler> logger)
{
    public async Task<Result<User>> Handle(CreateUserCommand command)
    {
        logger.LogInformation("Creating user");
        
        var commandResult = await command.Execute(userRepository);

        if (commandResult.IsFailure)
        {
            logger.LogError(commandResult.Error);
            return commandResult!;
        }
        
        return commandResult.Value!;
    }
}