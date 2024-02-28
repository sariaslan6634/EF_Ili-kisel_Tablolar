using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

//Geriye bir değer döndürür.
#region Scalar Functions Nedir ?
//Geriye herhangi bir türde değer döndüren fonksiyonlardır.
#endregion
#region Scalar funtion Oluşturma
//1. Adım : Boş bir migration oluşturulmalı.
//2. Adım : Bu migration içersinde Up metodunda Sql metodu eşliğinde fonksiyonun create kodları yazılıcak down metodu içerisinde de Drop kodları yazılacaktır.
//3. Adım : Migrate etme
#endregion
#region Scalar function'ı EF Core'a Entegre Etme

#region HasDbFunction
//veritabanı seviyesindeki herhangi bir fonksşyonu  EF Core/Yazılım kısmında bir metoda bind etmemizi sağlayan fonksiyondur.
#endregion

//var persons = await (from person in context.Persons
//                     where context.GetPersonTotalOrderPrice(person.PersonId) > 500
//                     select person).ToListAsync();

#endregion



//geriye bir tablo döndürür.
#region Inline Funcltions Nedir ?
//Geriye bir değer değil tablo donduren bir fonksiyondur.
#endregion
#region Inline Function Oluşturma
//1. Adım : Boş bir migration oluşturulmalı.
//2. Adım : Bu migration içersinde Up metodunda Sql metodu eşliğinde fonksiyonun create kodları yazılıcak down metodu içerisinde de Drop kodları yazılacaktır.
//3. Adım : Migrate etme
#endregion
#region Inline Function'ı EF Core'a Entegre Etme
//Default deger olarak 0 verdik o yüzden bütün veriler arasından kosulsuz sorguluyor.
var persons = await context.BestSellingStaff().ToListAsync();
foreach (var person in persons)
{
    Console.WriteLine($"Name : {person.Name} | Order Count : {person.OrderCount} |Total Order Price : {person.TotalOrderPrice} |");

}

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

public class BestSellingStaff
{
    public string Name { get; set; }
    public int OrderCount { get; set; }
    public int TotalOrderPrice { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Scalar
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext.GetPersonTotalOrderPrice), new[] { typeof(int) }))
            .HasName("getPersonTotalOrderPrice");
        #endregion
        #region Inline
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext.BestSellingStaff), new[] { typeof(int) }))
            .HasName("bestSellingStaff");

        modelBuilder.Entity<BestSellingStaff>()
            .HasNoKey();
        #endregion

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }  
    #region Scalar Functions
    public int GetPersonTotalOrderPrice(int personId)
        => throw new Exception();
    #endregion
    #region Inline Functions
    public IQueryable<BestSellingStaff> BestSellingStaff(int totalOrderPrice = 0)
        => FromExpression(() => BestSellingStaff(totalOrderPrice));
    #endregion
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
}