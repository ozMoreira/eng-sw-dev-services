using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using products_microservice.Domain.Contracts;

namespace products_microservice.Domain.Commands;

public class UpdateProductCommand
{
    [JsonIgnore] public Guid Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? QuantityInStock { get; set; }
    public string? ImageUrl { get; set; }
    public List<string>? Categories { get; set; }
    public List<string>? Tags { get; set; }

    public UpdateProductCommand WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public async Task<Result<Product?>> Execute(IProductRepository repository)
    {
        var product = await repository.Load(Id);

        if (product is null)
            return Result.Failure<Product?>("PRODUCT_NOT_FOUND");

        var result = product.Update(this);

        if (result.IsFailure)
            return result!;
        
        await repository.Update(product);
        
        return product;
    }
}