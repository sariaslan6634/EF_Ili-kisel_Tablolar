// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();
#region Index Nedir ?
//Bir sütüna dayalı sorgulamalrı daha verımlı ve performanslı hale getirmek icin kullanılan yapıdır.
#endregion

#region Index'leme Nasıl yapılır
//PK, ForeignKey,Alternate olan kolonlar otomatik olarak indexlenir.

#region Index Attribute'u
//index atacagımız sınıfın basında yapılır
#endregion

#region HasIndex Metodu
//Oncreating
#endregion

#endregion

#region Composite Index

#endregion

#region Birden Fazla Index Tanımlama

#endregion

#region Index Uniqueness
//[Index(nameof(Name), IsUnique = true)]
#endregion

#region Index Sort Order -Sıralama Düzeni(EF CORE 7.0)

#region AllDescending Attribute'u
// tüm indexlemelerde desending davranışının bütünsel olarak konfigürasyon sağlar.
//desending = Büyükten Kücüğe
#endregion
#region IsDescending Attribute'u
//Indexleme sürecindeki her bir kolona göre sıralama davarnışını hususi ayarlamak istiyorsak kullanılır.
#endregion
#region IsDescending Metodu

#endregion

#endregion

#region Index Name

#endregion

#region Index Filter

#endregion

#region Included Columns

#endregion

//[Index(nameof(Name),IsUnique = true)]
//[Index(nameof(Name)]
//[Index(nameof(Surname))]
//[Index(nameof(Name),nameof(Surname))] //1'den fazla composite olusturabılırız

//[Index(nameof(Name),AllDescending = true)]
//[Index(nameof(Name), nameof(Surname),IsDescending = new[] {true,false })]
//Desencing = true ,Desencing = false 

//[Index(nameof(Name), Name = "İndex'in adı")]
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

}
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Fluent API de index ypama
        //modelBuilder.Entity<Employee>()
        //    .HasIndex(i => i.Name);
        //.HasIndex(nameof(Employee.Name), nameof(Employee.Surname)); // buda aynı
        //.HasIndex(e => new { e.Name, e.Surname }); // 1 den fazla composit olusturablırız

        //indexlenen kolunun Uniq olmasını istiyorsak
        //modelBuilder.Entity<Employee>()
        //    .HasIndex(e => e.Name)
        //    .IsUnique();

        //IsDescendin tanımlama 
        //modelBuilder.Entity<Employee>()
        //    .HasIndex(e => e.Name)
        //    .IsDescending();

        //1 den fazla ise bu şekılde
        //modelBuilder.Entity<Employee>()
        //   .HasIndex(e => new { e.Name, e.Surname })
        //   .IsDescending(false, true);

        //İndex'in adını değiştirmek istersek
        //modelBuilder.Entity<Employee>()
        //    .HasIndex(e => e.Name)
        //    .HasDatabaseName("İndex'in adı");

        //Index yapmayıp sorguda hem name hem surname birde salary cagırdıysak salary'de bu sekılde ınclude edebilir.
        modelBuilder.Entity<Employee>()
            .HasIndex(e => new { e.Name, e.Surname })
            .IncludeProperties(e => e.Salary);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
}