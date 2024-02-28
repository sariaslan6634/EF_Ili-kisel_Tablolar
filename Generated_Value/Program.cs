// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");

#region Generated Value Nedir ?
//EF Core'da üretilen değerlerle ilgili çeşitli mnodellerin ayrıntılı yapılanmdırmayı sağlayan bir özelliktir.
#endregion

#region Default Values
//EF core'da herhangi bir tablonun herhangi bir kolonuyna yazılım tarafından değer gönderilmediği taktirde bu kolana
//hangi değerin(default olarak) üretilip yazdırılacagını belirleyen yapılanmalardır.

#region HasDefaultValue
//Statik veri veriyoruz
#endregion

#region HasDefaultValueSql
//SQL cümleciği veriyoruz.
#endregion

#endregion

#region Compueted Columns
//Tablo içerisindeki kolonlar üzerindeki yapılan aritmatik işlemler üzerinde neticesinde üretilen kolondur.

#region HasComputedColumnSql

#endregion

#endregion

#region Value Generation

#region Primary Keys
//PK = herhangi bir tablodaki kimlik vari şeklinde tanımlayan tekil olan(unique)olan sütün veya sütünlardır.
#endregion

#region Identity
//Yanlızca otomatik olarak artan bir sütündür.
//Bir sütün PK olmaksızın identity olarak tanımlanabilir.
//Bir tablo icerisinde Identity sadece 1 Tane olur.
#endregion
// bu iki özellik genellikle birlikte kullanır o yüzden EF core PK olan bir kolonu otomatik olarak Identity olacak şekilde yapılandırır.
//Ancak böyle olması icin bir gereklilik yoktur.

#region DatabaseGenerated

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse eğer none ile işaretlenecektir.
//EG Core'un degault olarak PK kolonlarda getirdiği Identity özelliğini kaldırmak istiyorsak eğer None'ı kullanabiliriz.
#endregion

#region DatabaseGeneratedOption.Identity - valueGenerateOnAdd
//İdentity = herhangi bir kolona Identity özelliğini vermemizi sağlayan bir konfigürasyondur.
#region Sayısal Türlerde
//Eğer ki Identity özelliği bir tabloda sayısal olan bir kolonda kullanılacaksa o durumda ilgili tablodaki pk olan kolondan özelliğinin kaldırılması gerekmektedir(None).
#endregion
#region Sayısal Olmayan Türlerde
//Eğer ki Identity özelliği bir tabloda sayısal olmayan bir kolonda kullanılacaksa o durumda ilgili tablodaki PK olan kolondan iradeli bir şekilde identityözelliğinin kaldırılmasına gerek yoktur.
//
#endregion

#endregion

#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate
//EF Core üzerinde bir kolon computed column ise ister computed olarak belirleyebilirsiniz istersenizde belirlemezseniz.

#endregion

#endregion

#endregion

//Person p = new()
//{
//    Name = "ibrahim",
//    Surname = "Yıldız",
//    Premium = 10,
//    TotalGain = 110,
//    PersonCode = 1
//};
//await context.Persons.AddAsync(p);
//await context.SaveChangesAsync();
class Person
{
    //Bunun identity(Sırayla artmasını) kaldırmamız gerek
    [DatabaseGenerated(DatabaseGeneratedOption.None)]

    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Premium { get; set; }

    //
    public int Salary { get; set; }
    public int TotalGain { get; set; }

    //Artık İdentity(sıra sıra artıcak)sensin
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersonCode { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person>Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Eğerki değer göndermediysek default olarak 100 gönder
        modelBuilder.Entity<Person>()
            .Property(p => p.Salary)
            .HasDefaultValue(100);

        modelBuilder.Entity<Person>()
            .Property(p => p.TotalGain)
            .HasComputedColumnSql("([Salary] + [Premium]) * 10");

        modelBuilder.Entity<Person>()
            .Property(p => p.PersonId)
            .ValueGeneratedNever();
    }
}