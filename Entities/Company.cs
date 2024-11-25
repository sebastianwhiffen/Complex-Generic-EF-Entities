public class Company : Entity, IClient, ICompany, IEntity
{
    public int ABN { get; set; } = 000000;
    public string TradingName { get; set; } = "unknown";
    public override string GetIdentifier() => TradingName + " " + ABN;
}
