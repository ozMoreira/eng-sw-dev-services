using CSharpFunctionalExtensions;
using users_microservice.Application;
using users_microservice.Domain.Commands;

namespace users_microservice.Domain;

public enum Status
{
    Active,
    Inactive
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? Number { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public static Address? Create(AddressModel? addressModel)
    {
        if (addressModel is null)
            return null;

        return new Address
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

public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }
        public Address ShippingAddress { get; set; }
        public Address? BillingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static Result<User> Create(CreateUserCommand command)
        {
            var shippingAddress = Address.Create(command.ShippingAddress);
            var billingAddress = Address.Create(command.BillingAddress);

            return new User
            {
                Id = Guid.CreateVersion7(),
                Name = $"{command.FirstName} {command.LastName}",
                Email = command.Email,
                Status = Status.Active,
                ShippingAddress = shippingAddress!,
                BillingAddress = billingAddress,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
        }
        
        public Result<User> Update(UpdateUserCommand command)
        {
            if (command.FirstName is not null && command.LastName is not null)
                Name = $"{command.FirstName} {command.LastName}";
            
            if (command.ShippingAddress is not null)
                ShippingAddress = Address.Create(command.ShippingAddress)!;
            
            if (command.BillingAddress is not null)
                BillingAddress = Address.Create(command.BillingAddress)!;
            
            UpdatedAt = DateTime.UtcNow;
            
            return this;
        }

        public Result<User> Deactivate()
        {
            Status = Status.Inactive;
            UpdatedAt = DateTime.UtcNow;
            
            return this;
        }
    }