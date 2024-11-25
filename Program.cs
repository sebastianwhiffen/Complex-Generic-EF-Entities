using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<MyDB>()
    .UseSqlite("Data Source=:memory:")
    .Options;

using var context = new MyDB(options);
context.Database.OpenConnection();
context.Database.EnsureCreated();

var repo = new GenericRepository<Entity>(context);

repo.SeedData();

context.SaveChanges();

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every entity in the DB");
Console.WriteLine();
var test = repo.Get().OfType<Entity>().ToList();

test.ForEach(x => Console.WriteLine(x.GetIdentifier()));

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every client in the DB");
Console.WriteLine();
repo.Get().OfType<Client>().ToList().ForEach(x => Console.WriteLine(x.GetIdentifier()));

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every client who is an individual in the DB");
Console.WriteLine();
repo.Get().OfType<Client<Individual>>().ToList().ForEach(x => Console.WriteLine(x.GetIdentifier()));

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every client who is an company in the DB");
Console.WriteLine();
repo.Get().OfType<Client<Company>>().ToList().ForEach(x => Console.WriteLine(x.GetIdentifier()));

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every client who is an SoleTrader in the DB");
Console.WriteLine();
repo.Get().OfType<Client<SoleTrader>>().ToList().ForEach(x => Console.WriteLine(x.GetIdentifier()));

Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.WriteLine("Get every individual in the DB");
Console.WriteLine();
repo.Get().OfType<Individual>().ToList().ForEach(x => Console.WriteLine(x.GetIdentifier()));

