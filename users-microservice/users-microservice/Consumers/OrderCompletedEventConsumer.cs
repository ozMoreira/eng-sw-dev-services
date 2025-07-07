using MassTransit;
using orders_microservice.Domain.Events;
using users_microservice.Application;
using users_microservice.Domain.Commands;

namespace users_microservice.Consumers;

public class OrderCompletedEventConsumer(
    UpdateUserCommandHandler handler, 
    ILogger<OrderCompletedEventConsumer> logger) : IConsumer<OrderCompletedEvent>
{
    public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
    {
        var order = context.Message.Data;

        var command = new UpdateUserCommand()
        {
            Id = order.UserId,
            BillingAddress = order.BillingAddress
        };
        
        var result = await handler.Handle(command);

        if (result.IsFailure)
        {
            logger.LogError(result.Error);
        }
        
        logger.LogInformation("User address updated {}", order.UserId);
    }
}