using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace users_microservice.Infra;

public class AppDbContext : DbContext
{
    internal DbSet<UserDbModel> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}