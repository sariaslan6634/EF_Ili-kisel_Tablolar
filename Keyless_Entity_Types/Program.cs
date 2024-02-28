using System.Reflection;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region Keyless Entity Types
//Normal entity typlara  ek olarak primary key içermeyen querylere karşı veritabanı sorguları yürütmek için kullanılan bir özelliktir. KET(Keyless Entity Types)

//Genellikle aggreate operasyonların yapıldığı group by yahut pivot table gibi işlemler neticesinde elde edilen istatistiksel sonuçlar primary key kolonu barındırmazlar.
//Bizler bu tarz sorguları Keyless Entity Types özelliği ile sanki bir entity'e karşılık geliyormuş gibi çalıştırabiliriz.
#endregion

#region Keyless Entity Types tanımlama
//1.Adım : Hangi sorgu olursa olsun EF Core üzerinden bu sorgunun bir entity'e karşılık geliyormuş gibi işleme/execute'a/çalıştırmaya tabi tutulabilmesi için o sorgunun sonucunda bir entity'nin yine de tasarlanması gerekmektedir.
//2.Adım : Bu Entity'nin Fbset Property'si olarak DbContext nesnesine eklenmesi gerekmektedir.
//3.Adım : Tanımlanmış olduğumuz entity'e OnModelCreating fonksiyonu içerisinde girip bunun bir key'i olmadığını bildirmeli (HasNoKey) ve hangi sorgunun çalıştırılacagı da ToView vs. gibi işlemlerle ifade edilmelidir.

//var datas =await context.PersonOrderCount.ToListAsync();
//foreach (var data in datas)
//    Console.WriteLine("Adı: {0} | Satış: {1}",data.Name, data.Count);

#region Keyless Attribute'u

#endregion
#region HasNoKey Fluent API'ı

#endregion

#endregion

#region Keyless Entity Tpyes özellikleri nelerdir?
//Primary Key Kolonu OLMAZ !
//Table Per Hayraki(TPH) Olarak entity hiyerarşisinde kullanılabilir. Lakin diğer kalıtımsal ilişkilerde kullanılaamz. 
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

//[Keyless] PR Key olmadığını belirtiyoruz
public class PersonOrderCount
{
    public string Name { get; set; }
    public int Count { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrderCount> PersonOrderCount { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        modelBuilder.Entity<PersonOrderCount>()
            .HasNoKey()
            .ToView("vw_PersonOrderCount");
            
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
}