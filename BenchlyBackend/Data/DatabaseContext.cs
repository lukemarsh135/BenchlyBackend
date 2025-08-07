using BenchlyBackend.Infrastructure.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BenchlyBackend.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<EntityAttribute>() != null);

        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type).ToTable(type.Name);
        }
    }
}