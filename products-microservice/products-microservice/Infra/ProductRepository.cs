using Microsoft.EntityFrameworkCore;
using products_microservice.Domain;
using products_microservice.Domain.Contracts;

namespace products_microservice.Infra;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> Save(Product product)
    {
        var productDbModel = (ProductDbModel)product!;
        
        await dbContext.Products.AddAsync(productDbModel);
        await dbContext.SaveChangesAsync();
        
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        var productDbModel = (ProductDbModel)product!;
        
        dbContext.Products.Update(productDbModel);
        await dbContext.SaveChangesAsync();
        
        return product;
    }

    public async Task<Product?> Load(Guid id)
    {
        var product = await dbContext
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return (Product?)product;
    }

    public async Task<List<Product>> Load(Pagination pagination)
    {
        var skip = (pagination.Page - 1) * pagination.PageSize;   
        var take = pagination.PageSize;
        
        var products = await dbContext
            .Products
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        return products.ConvertAll<Product>(product => (Product)product!);
    }
}