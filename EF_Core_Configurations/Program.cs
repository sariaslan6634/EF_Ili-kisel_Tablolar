using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

#region EF Core'da neden yapılandırmalara ihtiyacımız olur ?
//Default davranışları yeri geldiğinde geçersiz kılmak ve özelltirmek isteyebilirizç Bundan dolayı apılandırmalara ihtiyacımız olacaktır.
#endregion

#region OnModelCreating Metodu
//EF core'da yapılandırma deyince akla ilk gelen metot OnModelCreating metodurur.

//Bu metot,DbContext Sınıfı icerisinde virtual olarak ayarlanmış bir metottur.
//Bizlere bu metodu kullanarak model'larımızla ilgili temel konfigürasyonel davranışları sergilebiliriz.

#region GetEntityTypes 
//EF Core'da kullanılan entityleri elde etmek yada programatik olarak öğrenmek istiyorsak
//eğer GetEntityTypes fonksiyonunu kullanabiliriz.

#endregion

#endregion

#region Configurations | Data Annotations & Fluent API

#region Table - ToTable
//Generate edilecek tablonun ismini belirlememizi sağlayan yapılandırmadır.
//EF core normal şartlarda generate edecek tablonun adını DbSet<> den almaktadır.
//Eğer biz bunu özelleştirmek istiyorsak Table attribute'unu yahut ToTable API'ını kullanabilirz.
#endregion

#region Column - HasColumnName, HasColumnType,HasColumnOrder
//EF Core'da tabloların kolanları entity sınıfındaki propertylere karşılık gelir.
//Dafault olarak property'lerin adı,Kolan adıyken türleri/tipleri kolon türleridir.
//eğerki generate edılecek kolon isimlerine ve türlerine müdahale etmek istiyorsak bu konfigürasyon kullanılır.

#endregion

#region ForeignKey -HasForeignKey
//ilişkisel tablo tasarımlarında,bagımlı tabloda esas tabloya karşılık gelecek verilerin tutulduğu kolonun fereign key olarak temsil edilir.
//Ef Core'da foreign key kolunu genellikle entity tanımlama kuralları default olarak oluşturulur.
//ForeignKey Data annotatitons Attribute nu direkt kullanabiliriz
//Fluent API 'da ise iki entity arasındaki ilişkiyi modellememız gerekmekterdir.
#endregion

#region NotMapped - Ignore
//EF Core entity sınıfları icerisindeki tüm porpertyleri default olarak modellenen tabloya kolon şeklinde olarak migrate eder.
//Bazen bizler entity sınıfları icerisinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak mecburiyetinde kalabiliriz.
//bu propertylerin ef core tarafından kolon olarak oluşturmak istemediğimiz icin notMapped - Ignore kullanılırız.


#endregion

#region Key-HasKey
//EF Core'da default olarak convention olarak bir entity'nin içerisindeki(ID-EntityId,EntityIdyID vs.) şeklinde
// tanımlanan propertyler varsayılan olarak primarty key constraint uygulanır.
//Key Yada hasKey yapılandırmaları ile bunu yaparız.

//EF Core'da bir entity iöerinde PK olmak zorundadır. aksi takdirde EF Core magration oluştururken hata vericektir.
//Tablonun PK yoksa bunu bıldırmemız gerekir.
#endregion

#region TimesTamp - IsRowVersion
//Veri tutuarlığı ile ilgili derste göreceğiz.
//Bu derste bir satırdaki veririn bünsel olarak değişikliğini takip etmemizi sağlayacak olan versiyon mantığını konusuyor olacagız

#endregion

#region Required - IsRequired
//Bir kolonun nullable yada not null olup olmadıgını bu konfigürasyonla belirleye biliriz.
#endregion

#region MaxLenght - HasMaxLength - StringLength - HasMaxLength
//Bir kolonun max kareketerli olmasını belirler.
#endregion

#region Precision - HasPrecision
//Küsüratlı değerlerde .'dan sonraki gelicek basamak

//Küsüratlı sayılarda bir kesinlik belirtmemizi  ve
//Noktanın hanesini bildirmemizi sağlayan bir yapılandırmadır.

//Noktadan sonra 3 basamak gelmesi zorunludur.
#endregion

#region Unicode - IsUnicode
//Kolon içerisinde unicode karekterler kullanılacaksa bu yapılandırmadan istifade edilebilir.
#endregion

#region Comment - HasComment
//EF Core uzerınden oluşturulmuş olan veritabanına acıklama/yprum yapmak istiyorsak kullanılır.
#endregion

#region ConcurencyCheck - IsCConcurencyToken
//Veri tutuarlığı ile ilgili derste göreceğiz.
//Bu derste bir satırdaki veririn bünsel olarak değişikliğini takip etmemizi sağlayacak bir concurrecy token yapılanmasından bahsedeceğiz.
#endregion

#region InverseProperty
//iki entity arasında birden fazla ilişki varsa eğer bu ilişkilerin hangi navigationproperty'ler üzerinden olacıgını ayarlamamızı sağlayan bir konfigürasyondur.
#endregion

#endregion

#region Configurations | Fluent API

#region Composite Key
//Birden Fazla PK yapılır.
//Tablolarda birden fazla kolunu kümüşatif olarak Primary Key yapmak istiyorsak composite key denir.
#endregion

#region HasDefaultSchema
//EF Core üzerinden inşa edilen herhangi bir veritabanı nesnesi default olarak (dbo) şemasına sahiptir.
//Bunu özelleştirebilmek için kullanılan bir yapılandırmadır.
#endregion

#region Property

#region HasDefaultValue
//Tablodaki herhangi bir kolonun değer gönderlilmediği durumlarda default olarak hangi değeri alacagını belirler.
#endregion

#region HasDefaultValueSql
//Tablodaki herhangi bir kolonun değer gönderlilmediği durumlarda default olarak hangi SQL cümleciğinden değeri alacagını belirler.
#endregion

#endregion

#region HasComputedColumnSql
//Tablolarda birden fazla kolondaki verileri işleyerek değer oluşturan kolanlara ComputedColumn denir.
//EF Core  üzerinden bu tarz computed column oluşturabilmek için kullanılan bir yapılandırmadır.
#endregion

#region HasConstraintName
//EF Core üzerinden oluşturulan constraint'lere default isim yerine özelleştirilmiş bir isim vermek icin kullanılır.
#endregion

#region HasData
//Sonraki derslerimizde SEED DATA isimli bir konuyu inceleyeceğiz.
//Migrate süresinde veritabanını inşa ederken bir yandan da yazılım üzerinden hazir veriler oluşturmak istiyorsak eğer
//bunun yöntemini usulünü incelkiyor olacagız.
// HasData configürasyonu bu operasyonunun yapılandırma ayağıdır.

//Migrat  sürecinde olusturulacak olan verilerin PK manuel olarak girilmesi zorunludur.
#endregion

#region HasDiscriminator
//İleride entityler arasında kalıtımsal ilişkilerin olduğu TPT ve TPH konuları inceliyor olacagız.
//İşte bu konularla ilgili yapılandırmalarımız HasDiscriminator ve HasValue fonksiyonlarıdır
#region HasValue

#endregion

#endregion

#region HasField
//Backinf field özelliğini kullanmamızı sağlaya nbir özelliktir.
#endregion

#region HasNoKey
//Normal şartlarda EF Core'da tüm entitylerin bir Primary Key kolonu olması zorundadır.
//Eğerki entityde pk kolanu olmayacaksa bunun bıldırılmesı gerekir.
//Bunun ıcın kullanılan fonksiyondur.

#endregion

#region HasIndex
//Sonraki derslerimizde EF Core üzerinden Index yapılanmasını dataylıca inceliyor olacağız.
//Bu yapılandırmaya dair konfigürasyonlarımız HasIndex ve Index attribute'dır.
#endregion

#region HasQueryFilter
//ilerde göreceğimişz Global Query Filter başlıklı dersımızdır.
//Temeldeki görevi bir entitye karşılık uygulama bazında global bir filtre koymaktadır.
#endregion

#region DataBaseGenerated - ValueGeneratedOnAddOrUpdate,ValueGeneratedOnAdd,ValueGeneratedOnNever

#endregion

#endregion

abstract class Entity
{
    public int Id { get; set; }
    public string X { get; set; }
}
class A : Entity
{
    public string Y { get; set; }
}
class B : Entity
{
    public string Z { get; set; }
}

class Example
{
    public int id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Computed { get; set; }

}

//Tablo adını böyle veririrz DATA 
//[Table("kisiler")]
class Person
{
    //[Key]
    //public int Deneme { get; set; }

    public int Id { get; set; }//Primary Key direkt
    //public int Id2 { get; set; }

    public int DepartmentId { get; set; }

    [ForeignKey(nameof(Department))]
    public int Ahmet { get; set; }
    //public int DepartmentId { get; set; }

    [Column("Adi",TypeName ="metin")]

    public string _name;
    public string Name { get => _name; set => _name = value; }

    //
    [Required] //NotNull işlemi yapar ? ise null gelebilir demek.
    [MaxLength(45)] // Max 45 karekter girilir.
    [StringLength(45)] // Max 45 karekter girilir.
    [Unicode]
    public string? Surname { get; set; }

    [Precision(5,3)]
    public decimal Salary { get; set; }

    //
    [Comment("Bu şu işe yaramaktadır.")]
    [Timestamp]
    public byte[] RowVersion { get; set; }

    //Sadece yazılımda kullanıyoruz notMapped
    [NotMapped]
    public string Laylaylom { get; set; }

    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }

    [ConcurrencyCheck]
    public int ConcurrencyCheck { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Person> Persons { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Department> Departments { get; set; }

    public DbSet<Example>Examples { get; set; }

    public DbSet<Entity> Entities { get; set; }
    public DbSet<A> As { get; set; }
    public DbSet<B> Bs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region GetEntityTypes
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entity in entities)
        //{
        //    Console.WriteLine(entity.Name);
        //}

        #endregion
        #region ToTable
        //modelBuilder.Entity<Person>()
        //    .ToTable("Tablonun_adi");
        #endregion
        #region Column
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasColumnName("Adi") // adı
        //    .HasColumnType("metin") // türü
        //    .HasColumnOrder(7); //oluşturulma sırası 7
        #endregion
        #region ForeignKey
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(p => p.Persons)
        //    .HasForeignKey(p => p.Ahmet);
        #endregion
        #region Ignore
        //modelBuilder.Entity<Person>()
        //    .Ignore(p => p.Laylaylom);            
        #endregion
        #region HasKey
        //modelBuilder.Entity<Person>()
        //    .HasKey(p => p.Deneme);
        #endregion
        #region IsRowVersion
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.RowVersion)
        //    .IsRowVersion();

        #endregion
        #region IsRequired
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .IsRequired();
        #endregion
        #region MaxLength
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .HasMaxLength(45);
        #endregion
        #region HasPrecision
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Salary)
        //    .HasPrecision(5,3);
        #endregion
        #region IsUnicode
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .IsUnicode();
        #endregion
        #region HasComment
        //modelBuilder.Entity<Person>()
        //    .HasComment("Bu tablo suna yarar") // Tabloya yazıcaksak
        //    .Property(p => p.RowVersion)//propertylere yazıcaksak
        //    .HasComment("Bu şu işe yarıyacaktır");
        #endregion
        #region ConcurrencyCheck
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.ConcurrencyCheck)
        //    .IsConcurrencyToken();
        #endregion

        #region CompositeKey
        //modelBuilder.Entity<Person>()
        //.HasKey("Id","Id2")//Bu şekilde yada 
        //.HasKey(p => new { p.Id, p.Id2 }); // yada böyle
        #endregion
        #region HasDefaultSchema
        //modelBuilder.HasDefaultSchema("Ahmet");
        #endregion
        #region Property
        #region HasDefaultValue
        //modelBuilder.Entity<Person>()
        //    .Property(x => x.Salary)
        //    .HasDefaultValue(100);
        #endregion
        #region HasDefaultValueSql
        //modelBuilder.Entity<Person>()
        //.Property(x => x.CreatedDate)
        //.HasDefaultValueSql("GETDATE()");
        #endregion
        #endregion
        #region HasComputedColumnSql
        //modelBuilder.Entity<Example>()
        //    .Property(p => p.Computed)
        //    .HasComputedColumnSql("[X]+[Y]");
        #endregion
        #region HasConstraintName
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.DepartmentId)
        //    .HasConstraintName("Ahmet");
        #endregion
        #region HasData
        //modelBuilder.Entity<Department>()
        //    .HasData(new Department()
        //    { 
        //        Id = 1,
        //        Name = "Yazılım"},
        //    new Department
        //    {
        //        Name = "Makine",
        //        Id = 2
        //    });
        //modelBuilder.Entity<Person>()
        //    .HasData(new Person
        //    {
        //        Id =1,
        //        DepartmentId = 1,
        //        Name = "İbrahim",
        //        Surname = "SARIASLAN",
        //        Salary = 1000,
        //        CreatedDate = DateTime.Now},
        //    new Person
        //    {
        //        Id =2,
        //        DepartmentId = 2,
        //        Name = "Yılmaz",
        //        Surname = "ERDOĞDU",
        //        Salary = 1000,
        //        CreatedDate = DateTime.Now
        //    });
        #endregion
        #region HasDiscriminator
        //modelBuilder.Entity<Entity>()
        //.HasDiscriminator<int>("Ayırıcı")
        //.HasValue<A>(1)
        //.HasValue<B>(2)
        //.HasValue<Entity>(3);

        //     .HasDiscriminator<string>("Ayırıcı");//stirng ise gonderilecek veri böyle

        #endregion
        #region HasField
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasField(nameof(Person._name));

        #endregion
        #region HasNoKey
        //modelBuilder.Entity<Department>()
        //    .HasNoKey();
        #endregion
        #region HasIndex
        //modelBuilder.Entity<Person>()
        //    .HasIndex(p => new { p.Name, p.Surname });
        #endregion
        #region HasQueryFilter
        //Bu yıl eklenen personeller hangısı ıse ona gore sorguları getir.
        //Bütün sorgularda gecerlidir.
        //modelBuilder.Entity<Person>()
        //    .HasQueryFilter(p=>p.CreatedDate.Year ==DateTime.Now.Year); 
        #endregion

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
}

public class Flight
{
    public int FlightID { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public string Name { get; set; }

    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}
public class Airport
{
    public int AirportID { get; set; }
    public string Name { get; set; }

    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight> DepartingFlights { get; set; }

    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight> ArrivingFlights { get; set; }
}