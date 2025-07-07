using Microsoft.EntityFrameworkCore;
using orders_microservice.Domain;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Infra;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<Order> Save(Order order)
    {
        var orderDbModel = (OrderDbModel)order!;
        
        await context.Orders.AddAsync(orderDbModel);
        await context.SaveChangesAsync();
        
        return order;
    }

    public async Task<Order> Update(Order order)
    {
        var orderDbModel = (OrderDbModel)order!;
        
        context.Orders.Update(orderDbModel);
        await context.SaveChangesAsync();
        
        return order;
    }

    public async Task<Order?> Load(Guid id)
    {
        var order = await context
            .Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        
        return (Order?)order;
    }

    public async Task<List<Order>> Load(Pagination pagination)
    {
        var skip = (pagination.Page - 1) * pagination.PageSize;   
        var take = pagination.PageSize;
        
        var orders = await context
            .Orders
            .Include(o => o.Items)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        return orders.Count == 0 
            ? Enumerable.Empty<Order>().ToList() 
            : orders.ConvertAll<Order>(order => (Order)order!); 
    }
}