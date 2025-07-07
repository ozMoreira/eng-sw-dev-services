using Microsoft.AspNetCore.Mvc;
using orders_microservice.Application;
using orders_microservice.Domain.Commands;
using orders_microservice.Domain.Contracts;

namespace orders_microservice.Controllers;

public class OrdersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderCommand command, 
        [FromServices] CreateOrderCommandHandler handler)
    {
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((OrderModel)result.Value!);
    }
    
    [HttpPatch("{id}:complete")]
    public async Task<IActionResult> Complete(
        [FromRoute] Guid id,
        [FromServices] CompleteOrderCommandHandler handler)
    {
        var result = await handler.Handle(new CompleteOrderCommand(id));
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((OrderModel)result.Value!);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(
        [FromRoute] Guid id, 
        [FromServices] CancelOrderCommandHandler handler)
    {
        var result = await handler.Handle(new CancelOrderCommand(id));
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((OrderModel)result.Value!);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> FindOrder(
        [FromRoute] Guid id, 
        [FromServices] IOrderRepository repository)
    {
        var order = await repository.Load(id);
        
        if (order is null)
            return NotFound();
        
        return Ok((OrderModel)order!);
    }
    
    [HttpGet]
    public async Task<IActionResult> FindAll(
        [FromQuery] Pagination pagination, 
        [FromServices] IOrderRepository repository)
    {
        var orders = await repository.Load(pagination);
        
        if (orders.Count == 0)
            return NotFound();
        
        return Ok(orders.ConvertAll<OrderModel>(order => (OrderModel)order!));
    }
}