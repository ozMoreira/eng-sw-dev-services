using CSharpFunctionalExtensions;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Domain.Commands;

public class CompleteOrderCommand
{
    public CompleteOrderCommand(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; set; }
    
    public async Task<Result<Order>> Execute(IOrderRepository repository)
    {
        var order = await repository.Load(Id);
        
        if (order is null)
            return Result.Failure<Order>("ORDER_NOT_FOUND");
        
        var result = order.Complete();
        
        if (result.IsFailure)
            return result!;
        
        return order;
    }
}