using MassTransit;
using Microsoft.EntityFrameworkCore;
using orders_microservice.Application;
using orders_microservice.Domain.Contracts;
using orders_microservice.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextPool<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpLogging();

builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddScoped<CompleteOrderCommandHandler>();
builder.Services.AddScoped<CancelOrderCommandHandler>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddMassTransit(x =>
{
    var config = builder.Configuration.GetSection("RabbitMQ");
    
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(config["Host"], h =>
        {
            h.Username(config["Username"]!);
            h.Password(config["Password"]!);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();
    
app.MapOpenApi();
app.UseAuthorization();
app.MapControllers();

app.Run();