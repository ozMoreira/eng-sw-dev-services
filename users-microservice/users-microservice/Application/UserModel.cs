using users_microservice.Domain;

namespace users_microservice.Application;

public class AddressModel
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? Number { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    
    public static explicit operator AddressModel?(Address? addressModel)
    {
        if (addressModel is null)
            return null;

        return new AddressModel
        {
            Street = addressModel.Street,
            City = addressModel.City,
            State = addressModel.State,
            Number = addressModel.Number,
            Country = addressModel.Country,
            ZipCode = addressModel.ZipCode
        };
    }
}

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public AddressModel ShippingAddress { get; set; }
    public AddressModel? BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static explicit operator UserModel?(User? user)
    {
        if (user is null)
            return null;
        
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Status = user.Status.ToString(),
            ShippingAddress = (AddressModel)user.ShippingAddress!,
            BillingAddress = (AddressModel?)user.BillingAddress,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}