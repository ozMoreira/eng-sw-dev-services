using users_microservice.Application;

namespace orders_microservice.Domain.Events; // order domain boundary

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AddressModel BillingAddress { get; set; }
}

public class OrderCompletedEvent
{
    public Guid Id { get; set; }
    public string Name => "ORDER_COMPLETED";
    public Guid? CorrelationId { get; set; }
    public DateTime Timestamp { get; set; }
    public Order Data { get; set; }
}