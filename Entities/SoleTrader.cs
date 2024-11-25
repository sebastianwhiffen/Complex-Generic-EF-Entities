public class SoleTrader : Entity, IClient, ICompany, IIndividual, IEntity
{
    public int ABN { get; set; } = 000000;
    public string TradingName { get; set; } = "unknown";
    public string FirstName { get; set; } = "unknown";
    public string LastName { get; set; } = "unknown";
    public override string GetIdentifier()  => FirstName + " " + LastName + " From " + TradingName + " " + ABN;
}
