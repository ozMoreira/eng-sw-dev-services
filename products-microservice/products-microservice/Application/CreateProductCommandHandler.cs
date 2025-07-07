using CSharpFunctionalExtensions;
using products_microservice.Domain;
using products_microservice.Domain.Commands;
using products_microservice.Domain.Contracts;

namespace products_microservice.Application;

public class CreateProductCommandHandler(IProductRepository repository)
{
    public async Task<Result<Product>> Handle(CreateProductCommand command)
    {
        var commandResult = await command.Execute(repository);

        if (commandResult.IsFailure)
            return commandResult!;
        
        return commandResult.Value!;
    }
}