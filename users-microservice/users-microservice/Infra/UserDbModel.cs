using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json;
using users_microservice.Application;
using users_microservice.Domain;

namespace users_microservice.Infra;

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

public class UserDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    [Column(TypeName = "jsonb")] public string ShippingAddressDb { get; set; }
    [Column(TypeName = "jsonb")] public string? BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static explicit operator User?(UserDbModel? userDbModel)
    {
        if (userDbModel == null)
            return null;

        return new User
        {
            Id = userDbModel.Id,
            Name = userDbModel.Name,
            Email = userDbModel.Email,
            Status = Enum.Parse<Status>(userDbModel.Status),
            ShippingAddress = (Address) JsonSerializer.Deserialize<AddressDbModel>(userDbModel.ShippingAddressDb)!,
            BillingAddress = (Address?) JsonSerializer.Deserialize<AddressDbModel?>(userDbModel.BillingAddress ?? "{}"),
            CreatedAt = userDbModel.CreatedAt,
            UpdatedAt = userDbModel.UpdatedAt
        };
    }

    public static explicit operator UserDbModel?(User? user)
    {
        if (user is null)
            return null;

        return new UserDbModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Status = user.Status.ToString(),
            ShippingAddressDb =  JsonSerializer.Serialize(user.ShippingAddress),
            BillingAddress = user.BillingAddress is null ? null : JsonSerializer.Serialize(user.BillingAddress),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}   