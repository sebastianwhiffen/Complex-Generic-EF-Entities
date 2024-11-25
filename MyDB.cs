using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class MyDB : DbContext
{
    public MyDB(DbContextOptions options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Entity> Entities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.MapTypes();
    }
}

public static class ModelBuilderHelper
{
    private static List<(Type InterfaceType, Type ImplementationType)> TypesToMap => new List<(Type, Type)>
    {
        (typeof(IClient), typeof(Client<>)),
        (typeof(IEntity), typeof(Entity<>))
    };
    public static void MapTypes(this ModelBuilder modelBuilder)
    {
        foreach (var typeToMap in TypesToMap)
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeToMap.InterfaceType.IsAssignableFrom(t))
                .ToList();

            foreach (var type in types)
            {
                var genericType = typeToMap.ImplementationType.MakeGenericType(type);
                var entityTypeBuilder = modelBuilder.Entity(genericType);
                var dataProperty = genericType.GetProperty("Data");
                entityTypeBuilder.OwnsOne(dataProperty.PropertyType, dataProperty.Name);
            }
        }
    }
}
