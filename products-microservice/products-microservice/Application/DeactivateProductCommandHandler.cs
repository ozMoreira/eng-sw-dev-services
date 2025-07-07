using CSharpFunctionalExtensions;
using products_microservice.Domain;
using products_microservice.Domain.Commands;
using products_microservice.Domain.Contracts;

namespace products_microservice.Application;

public class DeactivateProductCommandHandler(IProductRepository repository)
{
    public async Task<Result<Product?>> Handle(DeactivateProductCommand command)
    {
        var result = await command.Execute(repository);

        if (result.IsFailure)
            return result!;
        
        return result.Value!;
    }
}