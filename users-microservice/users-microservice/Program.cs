using Microsoft.EntityFrameworkCore;
using users_microservice.Application;
using users_microservice.Domain.Contracts;
using users_microservice.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextPool<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpLogging();

builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
builder.Services.AddScoped<DeactivateUserCommandHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();
    
app.MapOpenApi();
app.UseAuthorization();
app.MapControllers();

app.Run();