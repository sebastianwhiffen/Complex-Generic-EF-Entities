this repo is an extension on my other repo about generic entities with ef core

I have this idea for infinitely nested generic types being mapped to a db

it will involve a bit of reflection but otherwise should work pretty good.

Writing this as a reminder to myself incase I need to pick this back up in the fututre but the idea is that:

starting from the entity table, we can map (inside the OnModelCreating() call) each possible type of entity. for example

Entity<Individual>

would make the entity table look like this:

| ID    | Individual_ID |
| -------- | ------- |
| 1        | 1       |
| 2        | NULL    |
| 2        | 2       |

then say we want there to be a Company type, after creating it a new migration should produce

| ID    | Individual_ID | Company_ID |
| -------- | ------- | -------|
| 1        | 1       | NULL   |
| 2        | NULL    | 1      |
| 2        | 2       | NULL   |


and the api on the code side could look like:

var res = new GenericRepository<Entity<Individual>>(context).Get();

or

repo.Get().OfType<Client<Individual>>().ToList().ForEach(x => Console.WriteLine(x.GetDisplayName()));

repo.Get().OfType<Client<Company>>().ToList().ForEach(x => Console.WriteLine(x.GetDisplayName()));

as seen working in: https://github.com/sebastianwhiffen/Generically-Typed-Entities-with-EF-Core

moving on (and making this harder (but also showing the true purpose of this exercise))

types can have a combination of nesting

like so: Entity<Client<Individual>>

or 

Entity<Partner<Individual>>

what would the table structure look like then?

it would look like this:

(Entities Table)
| ID    | Individual_ID | Company_ID | Client_ID |
| -------- | ------- | -------|------- |
| 1        | 1       | NULL   | NULL|
| 2        | NULL    | 1      | NULL|
| 3        | 2       | NULL   | NULL|
| 4        | 2       | NULL   | 1 |

see here this would denote that this entity is an individual AND a client

then you can join any info you need for this entity from the individual and the client table

basically you'll be joining any table that doesnt have a null value

also with some table constraints you can represent combos that arent valid

like 

   entity.ToTable("Entities", t =>
            {
                t.HasCheckConstraint("CK_Entity_Exclusive_Arc",
                    "((IndividualId IS NULL AND CompanyId IS NOT NULL) OR (IndividualId IS NOT NULL AND CompanyId IS NULL))");
            });

this basically says an entity must have either be a company or an individual. and that can be expaned upon. although I really don't like it being in plain text. maybe that's a job for another day

