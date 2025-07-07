using CSharpFunctionalExtensions;
using orders_microservice.Domain;
using orders_microservice.Domain.Commands;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Application;

public class CreateOrderCommandHandler(IOrderRepository repository)
{
    public async Task<Result<Order>> Handle(CreateOrderCommand command)
    {
        var commandResult = await command.Execute(repository);

        if (commandResult.IsFailure)
            return commandResult!;

        return commandResult.Value!;
    }
}