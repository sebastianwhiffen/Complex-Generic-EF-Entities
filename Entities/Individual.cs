public class Individual : Entity, IIndividual, IEntity, IClient
{
    public string FirstName { get; set; } = "unknown";
    public string LastName { get; set; } = "unknown";
    public override string GetIdentifier() => FirstName + " " + LastName;
}
