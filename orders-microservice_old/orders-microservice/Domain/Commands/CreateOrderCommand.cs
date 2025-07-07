using CSharpFunctionalExtensions;
using orders_microservice.Application;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Domain.Commands;

public class CreateOrderCommand
{
    public Guid CustomerId { get; set; }
    public List<OrderItemModel> Items { get; set; } = new();
    public AddressModel BillingAddress { get; set; }

    public async Task<Result<Order?>> Execute(IOrderRepository repository)
    {
        var orderResult = Order.Create(this);

        if (orderResult.IsFailure)
            return orderResult!;

        var order = orderResult.Value;
        await repository.Save(order);

        return order;
    }
}