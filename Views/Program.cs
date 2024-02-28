// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region View Nedir ?
//Oluşturdupumuz kompleks sorguları ihtiyaç durumlarında daha rahat bir şekilde kullanabilmek için basitleştiren bir veritabanı objesidir.

#endregion

#region EF Core İle View Kullanımı

#region View Oluşturma
//1.Adım : Boş bir migration oluşturulmalıdır.
//2.Adım : Migration içerisindeki up fonksiyonunda View'in creatin komutları Down fonksiyonunda ise drop komutları yazılmalıdır.
//3.Adım : Migrate ediniz.
#endregion

#region View'i DbSet Olarak Ayarlama
//View'i EF Core üzerinden sorgulayabilmek için view neticesini karşılayabilecek bir entity oluşturulması ve bu entity türünden DbSet property'sinin eklenmesi gerekmektedir.

#endregion

#region DbSet'in Bir View Olduğunu bildirmek

#endregion

//Bu view'e kendimizde şartlar koyabiliriz(LINQ)

//var personOrders = await context.PersonOrdes
//    .Where(po=> po.Count>5)
//    .ToListAsync();


#region EF Core'da View'lerin Özellikleri
//Primary Key olmaz!
//Bu yüzden ilgili Dbset'in HasNoKey ile işaretlenmesi gerekmektedir.

//View Neticesinde gelen veriler Change Tracker ile takip edilmezler.Üzerlerinde yapılan değişiklerden Ef Core veritabanına yansıtmaz.
//ilk veriyi getir ve adını abuzer yap demek istedik ama DEĞİŞİKLİK OLMAZ ! CHANGE TRACKER İZLEMEDİĞİ İCİN DEĞİŞİKLİK MEYDANA GELEMEZ
//var personOrder = await context.PersonOrdes.FirstAsync();
//personOrder.Name = "Abuzer";
//await context.SaveChangesAsync();   
#endregion

#endregion

Console.WriteLine();

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; }

}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

public class PersonOrdes
{
    public string Name { get; set; }
    public int Count { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<PersonOrdes> PersonOrdes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //Olusturalan PersonOrdes'ı bir view olduğunu bildirmek
        modelBuilder.Entity<PersonOrdes>()
            .ToView("vm_PersonOrders")
            .HasNoKey();
        
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server =(localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID =;Password = ;");
    }
}