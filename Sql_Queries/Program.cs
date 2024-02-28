using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

//Eğer ki , sorgunuzu LINQ ile ifade edemiyorsanız yahut LINQ'in ürettiği sorguya nazaran daha optimize bir sorguyu manuel geliştirmek ve EF Core üzerinden execute etmek istiyorsanız EF Core'ın bu davranısı desteklediğini bilmelisiniz.

//Manuel bir şekilde/tarafımızca oluşturulmuş olan sorguları EF Core tarafından execute edebilmek icin o srogunun sonucunu karşılıyacak bir entity model'ın tasarlanmış ve bunun DbSet<> olarak context nesnesine tanımlanmış olması gerekiyor.

#region FromSqlInterpolated
//EF Core 7.0 sürümünden önce ham sorguları execute edebildiğimiz fonksiyondur.

//bu bir FormattableString olduğu icin sorgunun basına $ işareti koymamız gerekıyor.
//var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons").ToListAsync();

#endregion

#region FromSql -EF Core 7.0
//EF Core 7.0 ile gelen metottur.
#region Query Execute
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons").ToListAsync();
#endregion

#region Stored Procedure Execute
//Veritabanında  stored precedure olusturuyoruz 
/*Create Proc sp_GetAllPersons
(
	@PersonId INT NULL
) AS
BEGIN
	IF @PersonId IS NULL
		SELECT * FROM Persons
	ELSE
		SELECT * FROM Persons WHERE PersonId = @PersonId
END --> Bu şekilde olusturduk
 */ //sonra bunu kullanmak istiyorsak
    //-> Sorgudaki null yerine 4 yazarsak id = 4 olan'a göre sorgulama yapacaktır.
    //-> EXECUTE dbo.sp_GetAllPersons NULL ile cagırıyoruz fonksiyonu(SQL Sorgusu)

//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons NULL").ToListAsync();

#endregion

#region Parametreli Sorgu Oluşturma

#region Örnek 1
//Burada sorguya geçirelen personId değişkeni arkaplanda bir DbParameter tipine dönüştürülerek o şekilde sorguya dahil edilmektedir.
//int personId = 3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persosns WHERE PersonId = {personId}").ToListAsync();

#endregion
#region Örnek 2
//int personId = 3;
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}").ToListAsync();

#endregion
#region Örnek 3
//                Tablodaki Hangiparametreye gelicek  , Gelicek değer                     
//SqlParameter personId = new("PersonId","3");
//personId.DbType = System.Data.DbType.Int32;
//personId.Direction = System.Data.ParameterDirection.Input;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons WHERE PersonId ={personId}").ToListAsync();
#endregion
#region Örnek 4
//SqlParameter personId = new("PersonId", "3");
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}").ToListAsync();
#endregion
#region Örnek 5
//                      =new("herhangi bir string değer girilebilir.")
//SqlParameter personId = new("PersonId", "3");
//Parametreli @ ile işaretleyip gönderiyoruz 1 den fazla var ise (sp_GetAllPersons'tablosunun ıcınde yazan parametler)
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons @PersonId = {personId}").ToListAsync();
#endregion
#endregion
#endregion

#region Dynamic SQL Olusturma ve Parametre Girme - FromSqlRaw
//YANLIŞ KOD
//string columnName = "PersonId", value = "3";
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where {columnName} = {value}").ToListAsync();
//EF core dinamik olarak oluşturulan sorgularda özellikle kolon isimleri parametreleştirilmişse o sorguyu ÇALIŞTIRMAZ !!!

//Peki doğrusunu nasıl yapcagız ? 

//Colon adını tanımladık gelıcek değeri ise SqlParametre olarak tanımladık 
//string columnName = "PersonId";
//Dikkat buradaki PersonId yazısına ne yazıyorsak                                           @burayada aynısını yazmamız gerekiyor.
//SqlParameter value = new("PersonId", "3");
//var persons = await context.Persons.FromSqlRaw($"SELECT * FROM Persons WHERE {columnName} = @PersonId",value).ToListAsync();

//FromSql ve FromSqlInterpolated metotlarında sql injection vs. gibi güvenlik önlemleri alınmış vaziyettedir.
//lakin dinamik olarak sorguları oluşturuyorsanız eğer burada güvenlik geliştirici sorumludur.
//Yani gelen sorguda/veri yorumlar,noktalı virgüller yahut SQL'e özel karekterlerin algılanması ve bunların temizlenmesi geliştirici tarafından yapılması gerekmektedir.

#endregion

#region SqlQuery - Entity Olmayan Scalar Sorguların Çalıştrırılması - Non Entity - Ef Core 7.0
//scalar = tek bir kolon geri dödürecekse
//Entity'si olmayan scalar sorguların çalıştırılığ sonucu elde etmemizi sağlayan bir fonksiyondur

//var datas = await context.Database.SqlQuery<int>($"SELECT PersonId FROM Persons").ToListAsync();

//Eğer LİNQ ile oluşturduktan sonra WHERE koşulu kullanmak istiyorsak FROMSQL 'de kullanabiliriz
//var persons = await context.Persons.FromSql($"SELECT * FROM Persosn").Where(p=>p.Name.Contains("a")).ToListAsync(); 

//SubQuery olduğu icin burdaki  kolonun değerini value(default) olarak ayarlıyoruz ondan sonra WHERE kullanabiliriz
//var data = await context.Database.SqlQuery<int>($"SELECT PersonId value FROM Persons")
//    .Where(x=>x>5)//->Burdaki x int'ı temsil etmektedir.
//    .ToListAsync();

//      Hocanın acıklaması
//SqlQuery'de LINQ operatörleriyle sorguya ekstradan katkıda bulunmak istiyorsanız eğer bu sorgu neticesinde gelecek olan kolonun adını "VALUE" olarak bildirmemiz gerekmektedir.
//Cünkü SqlQuery metodu soruguyu bir subQuery olarak generate etmektedir.
//Haliyle bu durumdan dolayı LİNQ ile verilen şart ifadeleri statik olarak "VALUE" kolonuna göre tasarlanmıştır.Oyüzden bu şekilde çalışma zorunluluğu gerekmektedir.
#endregion

#region ExecuteSql
//Unsert Update Delete gibi işlemleri yapabilir ama biz bunları yapmasak daha iyi 

//id si 1 olanın name'ini fatma yap.
//await context.Database.ExecuteSqlAsync($"UPDATE Persons SET Name = 'Fatma' WHERE PersonId = 1");
#endregion

#region Sınırlılıklar
//Queryler entity türünün tüm özellikleri için kolonlarda değer döndürmelidir
//var persons =await context.Persons.FromSql($"SELECT Name FROM Persons").ToListAsync();//-> burda patlarız bütün kolonları gelmellidir(* kullanılmalıdır yani)

//Sütün isimleri property isimleriyle aynı olmalıdır.

//SQL Sorugusu JOİN yapısı İÇEREMEZ !!! Haliyle bu tarz ihtiyaç noktalarında Include fonksiyonu KULLANILMALIDIR
//Hata verir çalışmaz.
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons JOIN Ordes ON Persons.PersonId = Orders.PersonId").ToListAsync();

//DOĞRUSU
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//    .Include(p=>p.Orders)
//    .ToListAsync();

#endregion

//Console.WriteLine();

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
        optionsBuilder.UseSqlServer(@"server =(localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID =;Password = ;");
    }
}