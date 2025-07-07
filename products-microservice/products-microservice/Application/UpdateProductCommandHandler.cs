using CSharpFunctionalExtensions;
using products_microservice.Domain;
using products_microservice.Domain.Commands;
using products_microservice.Domain.Contracts;

namespace products_microservice.Application;

public class UpdateProductCommandHandler(IProductRepository repository)
{
    public async Task<Result<Product?>> Handle(UpdateProductCommand command)
    {
        var commandResult = await command.Execute(repository);

        if (commandResult.IsFailure)
            return commandResult!;
        
        return commandResult.Value!;
    }
}