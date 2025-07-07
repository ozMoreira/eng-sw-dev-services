using orders_microservice.Domain;

namespace orders_microservice.Application;

public class AddressModel
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? Number { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    
    public static explicit operator AddressModel?(Address? address)
    {
        if (address is null)
            return null;
        
        return new AddressModel
        {
            Street = address.Street,
            City = address.City,
            State = address.State,
            Number = address.Number,
            Country = address.Country,
            ZipCode = address.ZipCode
        };
    }
}

public class OrderItemModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public static explicit operator OrderItemModel?(OrderItem? orderItem)
    {
        if (orderItem is null)
            return null;
        
        return new OrderItemModel
        {
            ProductId = orderItem.ProductId,
            ProductName = orderItem.ProductName,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }
}

public class OrderModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemModel> Items { get; set; } = new();
    public decimal Total { get; set; }
    public string Status { get; set; }
    public AddressModel BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static explicit operator OrderModel?(Order? order)
    {
        if (order is null)
            return null;
        
        return new OrderModel
        {
            Id = order.Id,
            UserId = order.UserId,
            Items = order.Items.ConvertAll(item => (OrderItemModel)item!),
            Total = order.Total,
            Status = order.Status.ToString(),
            BillingAddress = (AddressModel)order.BillingAddress!,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }   
}