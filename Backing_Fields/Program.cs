// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;


BackingFieldDbContext context = new();

Console.WriteLine("Hello, World!");


#region Backing Fields
//Tablo içerisindeki kolonları, entity class'ları içerinde prop'ları ile değil field'larla temsil etmemizi sağlayan bir özelliktir.

//public class Person
//{
//    public int Id { get; set; }
//    public string name;
//    public string Name { get => name.Substring(0, 3); set => name = value.Substring(0, 3); } 
//    public string Department { get; set; }
//}
#endregion

#region BakingField Attributes

//public class Person
//{
//    public int Id { get; set; }
//    public string name;

//    [BackingField(nameof(name))]
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
#endregion

#region HasField Fluent API
//Fleunt API'da HasField metodu BackingField özelliğine karşılık gelmektedir.

//public class Person
//{
//    public int Id { get; set; }

//    public string name;

//    public string Name { get; set; }
//    public string Department { get; set; }
//}

#endregion

#region Field Anda Property Access
//EF Core sorgulama sürecinde entity içerisndeki propertyleri ya da field'ları kullanıp kullanmayacağının davranışını bizlere belirtmektedir.

//EF Core, hiçbir ayarlama yoksa varsayılan olarak propertyler üzerinden verileri işler, Eğer ki backing dield bildiriliyorsa field üzerinden işler
//yok eğer field bildirildiği halde davranış belirtiliyorsa ne belirtilmişse ona göre işlemeyi devam ettirir.

//UsePropertyAccessMode üzerinden davranış modellemesi gerçekleştirilebilir.



#endregion

#region Field And Property Access
//Entitylerde değerleri almak için property'ler yerine netotların kullanıldığı veyya belirli alanların hüç gössterilmemesi gerektiği durumlarda(örneğin Primary Key kolonu) kullanılabilir

//propery Name kolonu tanımlamadık ama name field'ının ıcıne gelsın ıstıyorsak veriler OnModelCreating gidiyoruz
public class Person
{
    public int Id { get; set; }

    public string name;
    public string Department { get; set; }

    //Metotlarda kullanmak sitiyorsak
    public string GetName()
        => name;
    public string SetName(string value)
        => this.name = value;
}
#endregion

class BackingFieldDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = BaskingFieldDb;User ID = ;Password=;");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .Property(x => x.Name)
        //    .HasField(nameof(Person.name))               //alacakları değerler
        //    .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        //1-Field : Veri erişim süreçlerinde sadece field'ların kullanılmasını söyler. Eğer field'in kullanılmacağını durum söz konusu olursa bir exception fırlatır.
        //2-FieldDuringConstruction : Veri erişim süreçlerinde ilgili entitylden bir nesne oluşturulma sürecinde field'ların kullnanılmasını söyler.
        //3-Property : Veri erişim süreçlerinde sadece property'nin kullanılmasını söyler. Eğer property'nin kullanılmacağını durum söz konusu(Read-Only, Write-only) olursa bir exception fırlatır.
        //4-PreferField,
        //5-PreferFieldConstruction,
        //6-PreferFieldProperty



                //Field And Property Access
        modelBuilder.Entity<Person>()
            .Property(nameof(Person.name));
    }
}

