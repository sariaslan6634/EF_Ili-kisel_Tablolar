// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region Query Log Nedir ?
//LINQ sorguları neticesinde generate edilen sorguları izleyebilmek ve olası teknik hataları ayıklabilmek amacıyla query log mekanizmasından istifade etmekteyiz.
#endregion
#region Nasıl Konfigüre Edilir ?

await context.Persons.ToListAsync();

await context.Persons
    .Include(p=>p.Orders)
    .Where(p=>p.Name.Contains("a"))
    .Select(p=>new { p.Name,p.PersonId })
    .ToListAsync();
#endregion
#region Filtreleme Nasıl Yapılır ?

#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
    .AddFilter((category, level) =>
    {
        return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information; 
    })
    .AddConsole());
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
        optionsBuilder.UseLoggerFactory(loggerFactory);
    }
}