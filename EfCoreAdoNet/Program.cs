
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region DataBase Property'si
//Database property'si veritabanını temsil eden ve EF Core'un bazı işlevlerini detaylarına erişmemizi sağlayan bir propertydir.
#endregion

#region BeginTransaction
//EF Core, Transaction yönetimini otomatik bir şekilde kendisi gerçekleştirmektedir.
//Eğerki transaction yönetimine manuel olarak anlık ele almak istiyorsak BeginTransaction fonksiyonunu kullanırız.

//IDbContextTransaction transaction = context.Database.BeginTransaction();
#endregion
#region CommitTransaction
//EF Core üzerinden yapılan çalışmaların commit edilebilmesi için kullanılan bir fonksiyondur.

//context.Database.CommitTransaction();
#endregion
#region RollbackTransaction
//EF Core üzerinden yapılan çalışmaların rollback edilebilmesi için kullanılan bir fonksiyondur.

//context.Database.RollbackTransaction();
#endregion

#region CanConnect
//Veritabanı bağlantısını kontrol eder. 
//Verilen connection string'e karşılık bağlantı kurulabilir bir veritabanı var mı yok mu bunun bilgisini bool türünde veren bir fonksiyondur.

//bool connect = context.Database.CanConnect();
//Console.WriteLine(connect);
#endregion

#region EnsureCreated
//EF Core'da tasarlanan veritabanını migration kullanmaksızın, runtime'da yani kod üzerinde veritabanı sunucusuna inşa edilebilmek için kullanılan bir fonksiyondur.

//context.Database.EnsureCreated();
#endregion
#region EnsureDeleted
//İnşa edilmiş veritabanını runtime'da silmemizi sağlayan bir fonksiyondur.

//context.Database.EnsureDeleted();
#endregion

#region GenerateCreateScript
//Context nesnesinde yapılmış olan veritabanı tasarımı her ne ise ona uygun bir SQL script'ini string olarak veren metotdur.

//var script = context.Database.GenerateCreateScript();
//Console.WriteLine(script);
#endregion

#region ExecuteSql
//Veritabanına yapılacak Insert , Update ve Delete sorgularını yazdığımız metottur.
//Bu metot işlevsel olarak alacagı parametreleri SQL Injection saldırılarına karşı korumaktadır.

//string name = Console.ReadLine();
//var result = context.Database.ExecuteSql($"INSERT Persons VALUES('{name}')");

#endregion
#region ExecuteSqlRaw
//Veritabanına yapılacak Insert , Update ve Delete sorgularını yazdığımız metottur.
//Bu metotda ise sorguyu SQL Injection saldırılarına karşı koruma görevi geliştiricinin sorumluluğundadır.

//string name = Console.ReadLine();
//var result = context.Database.ExecuteSqlRaw($"INSERT Persons VALUES('{name}')");
#endregion

#region SqlQuery
//SqlQuery fonksiyonu her ne kadar erişilebilir olsada artık desteklenmemektedir.
//Bunun yerıne DbSet Property'si üzerinden erişilebilen FromSql fonksiyonu gelmiştir/kullanılmaktadır.
#endregion
#region SqlQueryRaw
//SqlQuery fonksiyonu her ne kadar erişilebilir olsada artık desteklenmemektedir.
//Bunun yerıne DbSet Property'si üzerinden erişilebilen FromSqlRaw fonksiyonu gelmiştir/kullanılmaktadır.
#endregion

#region GetMigrations
//Uygulamada üretilmiş olan tüm migration'ları runtime'da programatik olarak elde etmemizi sağlayan metottur.

//var migs = context.Database.GetMigrations();
//Console.WriteLine();
#endregion
#region GetAppliedMigrations
//uygulamada migrate edilmiş olan tüm migrationları elde etmemizi sağlayan fonksiyondur.

//var migs = context.Database.GetAppliedMigrations();
#endregion
#region GetPendingMigrations
//uygulamada migrate edilmemiş olan tüm migrationları elde etmemizi sağlayan fonksiyondur.

//var migs = context.Database.GetPendingMigrations();
#endregion
#region Migrate
//Migration'ları programatik olarak runtime'da migrate etmek için kullanılan bir fonksiyondur.
//OverLoading özelleiği yoktur(Tüm ne var ne yok migrate eder.)

//context.Database.Migrate();

//EnsureCreated fonksiyonu migration'ları kapsamamaktadır. O yüzden migration'lar içerisinde yapılan çalışmalar ilgili fonksiyonda geçerli olmayacaktır.
#endregion

#region OpenConnection
//Veritabanındaki bağlantıyı acmak için kullanılır.

//context.Database.OpenConnection();
#endregion
#region CloseConnection
//veritabanındaki bağlantıyı kapatmak icin kullanılır.

//context.Database.CloseConnection();
#endregion

#region GetConnectionString
//İlgili context nesnesinin o anda kullandığı connectionstring değeri ne ise onu elde etmemizi sağlar.
//Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password="

//Console.WriteLine(context.Database.GetConnectionString());

#endregion
#region GetDbConnection
// EF Core'un kullanmış olduğu Ado.NET altyapısının kullandığı DbConnection nesnesini elde etmemizi sağlayan bir fonksiyondur.
//Bizleri Ado.NET kanadına götürür.

//SqlConnection connection = (SqlConnection) context.Database.GetDbConnection();
//Console.WriteLine();
#endregion
#region SetDbConnection
//Özelleştirilmiş connection nesnelerini EF Core mimarisine dahil etmemizi sağlan bir fonksiyondur.

//context.Database.SetDbConnection();
#endregion
#region ProviderName Property'si
//Microsoft.EntityFrameworkCore.SqlServer cıktısını verır.
//Console.WriteLine(context.Database.ProviderName);
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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
}