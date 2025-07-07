using Microsoft.EntityFrameworkCore;
using products_microservice.Application;
using products_microservice.Domain.Contracts;
using products_microservice.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextPool<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpLogging();

builder.Services.AddScoped<CreateProductCommandHandler>();
builder.Services.AddScoped<UpdateProductCommandHandler>();
builder.Services.AddScoped<DeactivateProductCommandHandler>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();
    
app.MapOpenApi();
app.UseAuthorization();
app.MapControllers();

app.Run();