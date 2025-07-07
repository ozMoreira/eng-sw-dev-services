namespace orders_microservice.Domain.Contracts;

public interface IOrderRepository
{
    public Task<Order> Save(Order order);
    public Task<Order> Update(Order order);
    public Task<Order?> Load(Guid id);
    public Task<List<Order>> Load(Pagination pagination);
}