using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using users_microservice.Application;
using users_microservice.Domain.Commands;
using users_microservice.Domain.Contracts;

namespace users_microservice.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    [HttpPost(Name = "Create user")]
    public async Task<IActionResult> Get(
        CreateUserCommand command, 
        [FromServices] CreateUserCommandHandler handler)
    {
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((UserModel)result.Value!);
    }
    
    [HttpGet("{id:guid}", Name = "Get user")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id, 
        [FromServices] IUserRepository repository)
    {
        var user = await repository.Load(id);
        
        if (user is null)
            return NotFound();
        
        return Ok((UserModel)user!);
    }
    
    [HttpPatch("{id:guid}", Name = "Update user")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id, 
        [FromBody] UpdateUserCommand command,
        [FromServices] UpdateUserCommandHandler handler)
    {
        command.WithId(id);
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok((UserModel)result.Value!);
        
    }
    
    [HttpDelete("{id:guid}", Name = "Deactivate user")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeactivateUserCommandHandler handler)
    {
        var command = new DeactivateUserCommand(id);
        var result = await handler.Handle(command);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
    
    [HttpGet(Name = "Get users")]
    public async Task<IActionResult> Get(
        [FromQuery] Pagination pagination,
        [FromServices] IUserRepository repository)
    {
        var users = await repository.Load(pagination);
        
        if (users.Count == 0)
            return NotFound();
        
        return Ok(users.ConvertAll<UserModel>(user => (UserModel)user!));
    }
}