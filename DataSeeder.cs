public static class DataSeeder
{
    public static void SeedData(this GenericRepository<Entity> repo)
    {
        repo.Add(new Client<SoleTrader>()
        {
            Data = new()
            {
                FirstName = "Sole",
                LastName = "Trader",
                ABN = 123,
                TradingName = "Mr SoleTrader Business"
            }
        });

        repo.Add(new Client<Individual>()
        {
            Data = new()
            {
                FirstName = "Indy",
                LastName = "Vidual",
            }
        });

        repo.Add(new Client<Company>()
        {
            Data = new()
            {
                TradingName = "Company Incorporated",
                ABN = 321,
            }
        });
    }

}