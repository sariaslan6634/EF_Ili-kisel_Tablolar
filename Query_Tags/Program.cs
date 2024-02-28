using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();


#region Query Tags Nedir ?
//EF Core ile generate edilen sorgulara acıklama eklememızı saglayarak: SQL Profiler , Query Log vs. gibi yapılarda bu acıklamalar eşliğinde sorguları gözlemlememizi sağlayan bir özelliktir.

#endregion
#region TagWith Metodu

//await context.Persons.TagWith("Örnek bir Acıklama").ToListAsync();
#endregion

#region Multiple TagWith
//await context.Persons.TagWith("Tüm personeller cekılmıştir.")
//    .Include(p => p.Orders).TagWith("Personellerin yaptığı satışlar tabloya eklenmiştir.")
//    .Where(p => p.Name.Contains("a")).TagWith("Adında 'a' harfi olan personeller listelenmiştir")
//    .ToListAsync();
#endregion

#region TagWithCallSite Metodu
//oluşturulan sorguya acıklama satırı ekler ve ek olarak bu sorgunun bu dosyada (.cs) hangi satırda üretildiğinin bilgisini verir.
//await context.Persons.TagWithCallSite("Tüm personeller cekılmıştir.")
//    .Include(p => p.Orders).TagWithCallSite("Personellerin yaptığı satışlar tabloya eklenmiştir.")
//    .Where(p => p.Name.Contains("a")).TagWithCallSite("Adında 'a' harfi olan personeller listelenmiştir")
//    .ToListAsync();
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