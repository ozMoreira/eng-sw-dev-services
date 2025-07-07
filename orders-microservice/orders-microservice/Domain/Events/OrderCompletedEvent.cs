namespace orders_microservice.Domain.Events;

public class OrderCompletedEvent
{
    public Guid Id { get; set; }
    public string Name => "ORDER_COMPLETED";
    public Guid? CorrelationId { get; set; }
    public DateTime Timestamp { get; set; }
    public Order Data { get; set; }
    
    public OrderCompletedEvent(Order order)
    {
        Id = Guid.NewGuid();
        CorrelationId = order.Id;
        Timestamp = DateTime.UtcNow;
        Data = order;
    }
}