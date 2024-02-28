using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

var datas = await context.Persons.ToListAsync();
#region Neden Loglama Yaparız ?
//Çalışan bir sistemin runtime'da nasıl davranış gerçekleştirdiğni gözlemlemek için log mekanizmasını kullanırız.
#endregion
#region Neleri Loglarız ?
//Yapılan sorguların çalışma süreçlerindeki davranışları.
//Gerekirse hassas veriler üzerinde de loglama işlemleri gerçekleştirilebilir.
#endregion
#region Basit Olarak Loglama Nasıl Yapılır ?
//Minumum Yapılandırma gerektirmesi
//Herhangi bir nuget paketine ihtiyac duyulmaksızın loglanması.

#region Debug Penceresine Log Nasıl Atılır?
//Loglama override OnConfiguring fonksiyonunun icinde yapılır.
#endregion
#region Bir dosyaya Log Nasıl Atılır ?
//Normalde console yahut debug pencerelerıne atılan loglar pek takip edilebilir nitelikte değildir.
//logları kalıcı hale getirmek istediğimiz durumlarda en basit haliyle bu logları harici bir dosyaya atmak isteyebiliriz.
#endregion

#endregion

#region Hassas Verilerin Loglanması - EnableSensitiveDataLogging
//Default olarak EF Core log mesajlarında herhangi bir verinin değerini içermemektedir.Bunun nedeni ,gizlilik teşkil edebilecek verilerin loglama sürecinde yanlışlıklada olsa acığa cıkmamasıdır.
//Bazen alınan hatalarda verinin değerini bilmek hatayı debug edebilmek için oldukca yardımcı olabilmketedir.Bu durumda hassas verilerinde loglamasını sağlayabiliriz.
#endregion

#region Exception Ayrıntısını Loglama - EnableDetailedErrors

#endregion

#region Log Levels
//EF Core default olarak degub seviyesinin üstündeki (debug dahil) tüm davarnışları loglar
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

    //_log.txt dosyasını olusturur. ve üzerine sürekli yazar.Varsa yazar yoksa yenıden olusturur yazar.
    StreamWriter _log = new("logs.txt", append: true);
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");

        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));

        //Asekron olarak tanımlama 
        //optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message));
        //Normal olarak tanımlama 2 side aynı
        //optionsBuilder.LogTo(message =>  _log.WriteLine(message));

                                                                                 //LogLevel
        optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message),LogLevel.Information)
            .EnableSensitiveDataLogging()//Hassas verileri de gösterir personId 
            .EnableDetailedErrors();//Alınan hataları detaylı listeler.
    }

    public override void Dispose()
    {
        base.Dispose();
        _log.Dispose();
    }
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _log.DisposeAsync();
    }
}