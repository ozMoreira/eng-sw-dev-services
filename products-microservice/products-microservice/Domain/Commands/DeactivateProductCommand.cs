using CSharpFunctionalExtensions;
using products_microservice.Domain.Contracts;

namespace products_microservice.Domain.Commands;

public class DeactivateProductCommand
{
    public DeactivateProductCommand(Guid id) => Id = id;
    public Guid Id { get; set; }
    
    public async Task<Result<Product?>> Execute(IProductRepository repository)
    {
        var product = await repository.Load(Id);
        
        if (product is null)
            return Result.Failure<Product?>("PRODUCT_NOT_FOUND");
        
        var result = product.Deactivate();
        
        if (result.IsFailure)
            return result!;
        
        await repository.Update(product);
        
        return product;
    }
}