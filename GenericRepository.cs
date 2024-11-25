using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> where T : class
{
    private readonly MyDB _context;

    public GenericRepository(MyDB context)
    {
        _context = context;
    }

    public IEnumerable<T> Get(Func<T, bool> predicate) => _context.GetDbSet<T>().Where(predicate);
        
    public IEnumerable<T> Get() => _context.GetDbSet<T>();

    public void Add(T newEntity) => _context.Add(newEntity);
}

public static class GenericRepositoryHelpers
{
    public static DbSet<T> GetDbSet<T>(this DbContext context) where T : class
    {
        var property = context.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

        if (property == null) throw new InvalidOperationException($"DbSet<{typeof(T).Name}> not found in context.");

        return (DbSet<T>)property.GetValue(context);
    }
}
