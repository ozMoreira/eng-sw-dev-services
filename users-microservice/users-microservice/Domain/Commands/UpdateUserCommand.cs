using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using users_microservice.Application;
using users_microservice.Domain.Contracts;

namespace users_microservice.Domain.Commands;

public class UpdateUserCommand
{
    [JsonIgnore] public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public AddressModel? ShippingAddress { get; set; }
    public AddressModel? BillingAddress { get; set; }
    
    public UpdateUserCommand WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public async Task<Result<User?>> Execute(IUserRepository repository)
    {
        var user = await repository.Load(Id);

        if (user is null)
            return Result.Failure<User?>("USER_NOT_FOUND");

        var result = user.Update(this);

        if (result.IsFailure)
            return result!;
        
        await repository.Update(user);
        
        return user;
    }
}