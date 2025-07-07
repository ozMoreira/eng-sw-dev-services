using CSharpFunctionalExtensions;
using MassTransit;
using orders_microservice.Domain;
using orders_microservice.Domain.Commands;
using orders_microservice.Domain.Contracts;
using orders_microservice.Domain.Events;

namespace orders_microservice.Application;

public class CompleteOrderCommandHandler(IOrderRepository repository, IBus bus)
{
    public async Task<Result<Order>> Handle(CompleteOrderCommand command)
    {
        var result = await command.Execute(repository);

        if (result.IsFailure)
            return result;
        
        await bus.Publish(new OrderCompletedEvent(result.Value));

        return result.Value;
    }
}