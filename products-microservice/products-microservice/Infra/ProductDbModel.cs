using products_microservice.Domain;

namespace products_microservice.Infra;

public class ProductDbModel
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

    public static explicit operator Product?(ProductDbModel? productDbModel)
    {
        if (productDbModel == null)
            return null;

        return new Product
        {
            Id = productDbModel.Id,
            Name = productDbModel.Name,
            Description = productDbModel.Description,
            Price = productDbModel.Price,
            QuantityInStock = productDbModel.QuantityInStock,
            ImageUrl = productDbModel.ImageUrl,
            Categories = productDbModel.Categories,
            Tags = productDbModel.Tags,
            Status = productDbModel.Status,
            CreatedAt = productDbModel.CreatedAt,
            UpdatedAt = productDbModel.UpdatedAt
        };
    }

    public static explicit operator ProductDbModel?(Product? product)
    {
        if (product is null)
            return null;

        return new ProductDbModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            ImageUrl = product.ImageUrl,
            Categories = product.Categories,
            Tags = product.Tags,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}