using Microsoft.EntityFrameworkCore;
using orders_microservice.Domain;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Infra;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<Order> Save(Order order)
    {
        var orderDbModel = (OrderDbModel)order!;
        
        await context.Users.AddAsync(orderDbModel);
        await context.SaveChangesAsync();
        
        return order;
    }

    public async Task<Order> Update(Order order)
    {
        var orderDbModel = (OrderDbModel)order!;
        
        context.Users.Update(orderDbModel);
        await context.SaveChangesAsync();
        
        return order;
    }

    public async Task<Order?> Load(Guid id)
    {
        var order = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return (Order?)order;
    }

    public async Task<List<Order>> Load(Pagination pagination)
    {
        var skip = (pagination.Page - 1) * pagination.PageSize;   
        var take = pagination.PageSize;
        
        var orders = await context
            .Users
            .Skip(skip)
            .Take(take).ToListAsync();
        
        return orders.Count == 0 
            ? Enumerable.Empty<Order>().ToList() 
            : orders.ConvertAll<Order>(order => (Order)order!); 
    }
}