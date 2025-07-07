using CSharpFunctionalExtensions;
using orders_microservice.Domain;
using orders_microservice.Domain.Commands;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Application;

public class CompleteOrderCommandHandler(IOrderRepository repository)
{
    public async Task<Result<Order>> Handle(CompleteOrderCommand command)
    {
        var result = await command.Execute(repository);

        if (result.IsFailure)
            return result!;

        return result.Value;
    }
}