
public interface IEntity
{
    public string GetIdentifier();
};

public interface IClient;

public interface IIndividual
{
    string FirstName {get; set;}
    public string LastName { get; set; }
};

public interface ICompany
{
    int ABN {get; set;}
    string TradingName {get; set;}
};