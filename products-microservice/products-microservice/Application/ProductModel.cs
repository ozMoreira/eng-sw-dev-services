using products_microservice.Domain;

namespace products_microservice.Application;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static explicit operator ProductModel?(Product? product)
    {
        if (product is null)
            return null;
        
        return new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            ImageUrl = product.ImageUrl,
            Categories = product.Categories,
            Tags = product.Tags,
            Status = product.Status.ToString(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}