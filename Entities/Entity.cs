public class Entity<T> : Entity where T : Entity, IEntity, new()
{
    public T Data {get; set;}
    public Entity()
    {
        Data = Activator.CreateInstance<T>();
    }
    public override string GetIdentifier() => Data.GetIdentifier();
}

public abstract class Entity
{
    public int ID { get; set; }
    public abstract string GetIdentifier();
}