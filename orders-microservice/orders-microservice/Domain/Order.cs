using CSharpFunctionalExtensions;
using orders_microservice.Application;
using orders_microservice.Domain.Commands;

namespace orders_microservice.Domain;

public enum Status
{
    Pending,
    Completed,
    Cancelled
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? Number { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public static Address? Create(AddressModel? addressModel)
    {
        if (addressModel is null)
            return null;

        return new Address
        {
            Street = addressModel.Street,
            City = addressModel.City,
            State = addressModel.State,
            Number = addressModel.Number,
            Country = addressModel.Country,
            ZipCode = addressModel.ZipCode
        };
    }
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public static List<OrderItem> Create(List<OrderItemModel> items)
    {
        return items.ConvertAll(item => new OrderItem
        {
            Id = Guid.CreateVersion7(),
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            Price = item.Price
        });
    }
}

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal Total { get; set; }
    public Status Status { get; set; }
    public Address BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static Result<Order> Create(CreateOrderCommand command)
    {
        var items = OrderItem.Create(command.Items);
        var billingAddress = Address.Create(command.BillingAddress);
        
        var total = items.Sum(i => i.Price * i.Quantity);
        
        return new Order
        {
            Id = Guid.CreateVersion7(),
            UserId = command.UserId,
            Items = items,
            Total = total,
            Status = Status.Pending,
            BillingAddress = billingAddress!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }
    
    public Result<Order> Complete()
    {
        if (Status != Status.Pending)
            return Result.Failure<Order>("ORDER_NOT_PENDING");
        
        Status = Status.Completed;
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }
    
    public Result<Order> Cancel()
    {
        switch (Status)
        {
            case Status.Completed:
                return Result.Failure<Order>("ORDER_ALREADY_COMPLETED");
            case Status.Cancelled:
                return Result.Failure<Order>("ORDER_ALREADY_CANCELLED");
            case Status.Pending:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Status = Status.Cancelled;
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }
}