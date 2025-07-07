using Microsoft.AspNetCore.Mvc;
using products_microservice.Application;
using products_microservice.Domain.Commands;
using products_microservice.Domain.Contracts;

namespace products_microservice.Controllers;

[ApiController]
[Route("/products")]
public class ProductsController : ControllerBase
{
    [HttpPost(Name = "Create product")]
    public async Task<IActionResult> Get(
        CreateProductCommand command, 
        [FromServices] CreateProductCommandHandler handler)
    {
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((ProductModel)result.Value!);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id, 
        [FromBody] UpdateProductCommand command,
        [FromServices] UpdateProductCommandHandler handler)
    {
        command.WithId(id);
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((ProductModel)result.Value!);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeactivateProductCommandHandler handler)
    {
        var command = new DeactivateProductCommand(id);
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id, 
        [FromServices] IProductRepository repository)
    {
        var product = await repository.Load(id);
        
        if (product is null)
            return NotFound();
        
        return Ok((ProductModel)product!);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] Pagination pagination,
        [FromServices] IProductRepository repository)
    {
        var products = await repository.Load(pagination);
        
        if (products.Count == 0)
            return NotFound();
        
        return Ok(products.ConvertAll<ProductModel>(product => (ProductModel)product!));
    }
}