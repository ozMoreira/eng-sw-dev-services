using CSharpFunctionalExtensions;
using users_microservice.Application;
using users_microservice.Domain.Contracts;

namespace users_microservice.Domain.Commands;

public class CreateUserCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AddressModel ShippingAddress { get; set; }
    public AddressModel? BillingAddress { get; set; }
    
    public async Task<Result<User?>> Execute(IUserRepository repository)
    {
        var userResult = User.Create(this);
        
        if (userResult.IsFailure)
            return userResult!;
        
        var user = userResult.Value;
        await repository.Save(user);
        
        return user;
    }
}