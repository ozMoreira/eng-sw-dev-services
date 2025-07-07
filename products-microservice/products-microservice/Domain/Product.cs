using CSharpFunctionalExtensions;
using products_microservice.Domain.Commands;

namespace products_microservice.Domain;

public enum Status
{
    Active,
    Inactive
}

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<Product> Create(CreateProductCommand command)
    {
        var product = new Product
        {
            Id = Guid.CreateVersion7(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            QuantityInStock = command.QuantityInStock,
            ImageUrl = command.ImageUrl,
            Categories = command.Categories,
            Tags = command.Tags,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
        
        return product;
    }

    public Result<Product> Update(UpdateProductCommand command)
    {
        if (Status == Status.Inactive)
            return Result.Failure<Product>("PRODUCT_IS_INACTIVE");
        
        if (command.Name is not null)
            Name = command.Name;
        
        if (command.Description is not null)
            Description = command.Description;
        
        if (command.Price is not null)
            Price = command.Price.Value;

        if (command.QuantityInStock is not null)
            QuantityInStock = command.QuantityInStock.Value;
        
        if (command.ImageUrl is not null)
            ImageUrl = command.ImageUrl;
        
        if (command.Categories is not null)
            Categories = command.Categories;
        
        if (command.Tags is not null)
            Tags = command.Tags;
        
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }

    public Result<Product> Deactivate()
    {
        if (Status == Status.Inactive)
            return Result.Failure<Product>("PRODUCT_IS_INACTIVE");
        
        Status = Status.Inactive;
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }
}