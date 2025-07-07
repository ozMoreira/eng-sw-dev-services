using CSharpFunctionalExtensions;
using products_microservice.Domain.Contracts;

namespace products_microservice.Domain.Commands;

public class CreateProductCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    
    public async Task<Result<Product?>> Execute(IProductRepository repository)
    {
        var productResult = Product.Create(this);
        
        if (productResult.IsFailure)
            return productResult!;
        
        var product = productResult.Value;
        await repository.Save(product);
        
        return product;
    }
}