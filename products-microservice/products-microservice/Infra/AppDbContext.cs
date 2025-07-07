using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace products_microservice.Infra;

public class AppDbContext : DbContext
{
    internal DbSet<ProductDbModel> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}