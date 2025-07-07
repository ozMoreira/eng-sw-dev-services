using System.ComponentModel.DataAnnotations.Schema;
using orders_microservice.Domain;

namespace orders_microservice.Infra;

public class AddressDbModel
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? Number { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    
    public static explicit operator Address?(AddressDbModel? addressDbModel)
    {
        if (addressDbModel == null)
            return null;

        return new Address
        {
            Street = addressDbModel.Street,
            City = addressDbModel.City,
            State = addressDbModel.State,
            Number = addressDbModel.Number,
            Country = addressDbModel.Country,
            ZipCode = addressDbModel.ZipCode
        };
    }
    
    public static explicit operator AddressDbModel?(Address? address)
    {
        if (address == null)
            return null;
        
        return new AddressDbModel
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

public class OrderItemDbModel
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public OrderItemDbModel WithOrderId(Guid orderId)
    {
        OrderId = orderId;
        return this;
    }
    
    public static explicit operator OrderItem?(OrderItemDbModel? orderItemDbModel)
    {
        if (orderItemDbModel == null)
            return null;
        
        return new OrderItem
        {
            Id = orderItemDbModel.Id,
            ProductId = orderItemDbModel.ProductId,
            ProductName = orderItemDbModel.ProductName,
            Quantity = orderItemDbModel.Quantity,
            Price = orderItemDbModel.Price
        };
    }
    
    public static explicit operator OrderItemDbModel?(OrderItem? orderItem)
    {
        if (orderItem == null)
            return null;
        
        return new OrderItemDbModel
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            ProductName = orderItem.ProductName,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }
}

public class OrderDbModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<OrderItemDbModel> Items { get; set; }
    public decimal Total { get; set; }
    public Status Status { get; set; }
    [Column(TypeName = "jsonb")] public AddressDbModel BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static explicit operator Order?(OrderDbModel? orderDbModel)
    {
        if (orderDbModel == null)
            return null;
        
        return new Order
        {
            Id = orderDbModel.Id,
            CustomerId = orderDbModel.CustomerId,
            Items = orderDbModel.Items.ConvertAll(item => (OrderItem)item!),
            Total = orderDbModel.Total,
            Status = orderDbModel.Status,
            BillingAddress = (Address)orderDbModel.BillingAddress!,
            CreatedAt = orderDbModel.CreatedAt,
            UpdatedAt = orderDbModel.UpdatedAt
        };
    }
    
    public static explicit operator OrderDbModel?(Order? order)
    {
        if (order == null)
            return null;
        
        var items = order
            .Items
            .ConvertAll(item => (OrderItemDbModel)item!)
            .Select(item => item.WithOrderId(order.Id));
        
        return new OrderDbModel
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Items = order.Items.ConvertAll(item => (OrderItemDbModel)item!),
            Total = order.Total,
            Status = order.Status,
            BillingAddress = (AddressDbModel)order.BillingAddress!,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }
}