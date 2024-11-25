public class Client<T> : Client where T : Entity, IClient, IEntity, new()
{
    public T Data { get; set; }
    public Client()
    {
        Data = Activator.CreateInstance<T>();
    }
    public override string GetIdentifier() => Data.GetIdentifier();
}

public abstract class Client : Entity;