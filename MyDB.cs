using System.ComponentModel;
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

                entityTypeBuilder.HasOne(dataProperty.PropertyType, dataProperty.Name)
                    .WithOne()
                    .HasForeignKey(genericType, $"{dataProperty.Name}ID");

                var foreignKeyProperty = genericType.GetProperty($"{dataProperty.Name}ID");
                if (foreignKeyProperty == null) entityTypeBuilder.Property<int>($"{dataProperty.Name}ID");
            }
        }
    }

}

//the entity table is a constant, just contains things like date updated, date created and ID

//the client table is variable and begins the true test, as it can and should work in conjunction with... eg: partner.
//we should be able to have as many of these umbrella types as we want while keeping the sub tables (Individuals, Companies, SoleTraders) untouched
//say in the future we want to add another umbrella like Employee<Individual>. it will just work.

//the individual table slots into the two above types as a generic param, eg. Client<Individual>, Partner<Individual>. 
//That Individual has its own record from its own table so it is technically the same Person as both a client and a partner

//and theoretically you could take it further and go Client<Individual<Male<Human>>>, Client<Individual<Female<Xenomorph>>>












