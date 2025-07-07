namespace products_microservice.Domain.Contracts;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public interface IProductRepository
{
    public Task<Product> Save(Product product);
    public Task<Product> Update(Product product);
    public Task<Product?> Load(Guid id);
    public Task<List<Product>> Load(Pagination pagination);
}