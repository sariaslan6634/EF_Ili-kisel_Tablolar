
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");



#region Table Per Heararchy(TPH) Nedir ?
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir talo oluşturan davranıştır.
#endregion

#region Nede Table Per Heararchy(TPH) ihtiyacımız var ?
//İçerisinde benzer alanlara sahip olan entityleri magrate ettiğimizde her entitye karşılık bir tablo olusturmaktansa bu entityleri tek bir tabloda modellemek istebiliriz.
//Bu tabloki kayıtları discriminator kolpnu üzerinden birbirlerinden ayırabliriz.İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama,verekleme , veri silme , vs. gibi operasyonların şekillendirilmesi  icin TPH davranışısını kullanabikiriz.
#endregion

#region TPH Nasıl Uygulanır ?
//EF Core'da entityler arasında temel bir kalıtımsal ilişki söz konusuysa eğer default olarak kabul edilen davranıştır.
//O yüzden herhangi bir konfigürasyon gerektirmez.

//Entityler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext Nesnesi üzerine DbSet<> olarak eklenmelidir.
#endregion

#region Discriminator Kolonu Nedir ?
//TPH yaklaışı neticesinde kümülatif olarak inşa edilimiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlar.
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar

//Discriminator kolonu komple özelleştirilebilir.
#endregion

#region Discriminator Kolon Adı Nasıl Değiştirilir ?
//Öncelikle hiyeraşinin başında hangi sınıf varsa onun Fluent API'da konfigürasyonuna gidilmeli.
//Ardından HasDiscriminator fonksiyonu ile özelleştirilmeli.
#endregion

#region Discriminator Değerleri Nasıl Değiştirilir ?
//Yine hiyerarşinin başındaki entity konfigürasyonlarına gelip, HasDisciriminator fonksiyonu ile özellştirmede bulunarak ardından HasValue fonksiyonu ile hangi entitye karşılık hangi değerin gireceğini belirtilen türde ifade edebilirsiniz.
#endregion

#region TPH'da Verik Ekleme
//Davranışların hiçbirinde veri eklerken silerken güncellerken vs normal operasyonların dısında bir işlem yapılmaz.

//Hangi davranışı kullanıyorsak EF Core Arka planda onu modelleştiriyor.

//Employee e1 = new Employee() { Name = "ibrahim", Surname = "Sariaslan" , Department = "Yazılım" };
//Employee e2 = new Employee() { Name = "Burak", Surname = "Kurtulus",Department = "Bilgi işlem" };
//Customer c1 = new Customer() { Name = "Şuayip", Surname = "ABC", CompanyName = "Halı kilim travel" };
//Customer c2 = new Customer() { Name = "Ahmet", Surname = "XTAZ", CompanyName = "Halı kilim travel" };
//Technician t1 = new Technician() { Name = "Rıfkı", Surname = "TOK", Department = "Muhasebe", Branch = "Para Tutucu" };
//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);
//await context.SaveChangesAsync();
#endregion

#region TPH'da Verik Silme
//TPH davranmıışında silme opperasyonu yine entity üzerinden gercekleştiriliri.

//3 idsine sahip olan employees'ı getir  
//var sil = await context.Employees.FindAsync(3);
//context.Employees.Remove(sil);
//await context.SaveChangesAsync();

//Bütün customer'ları silmek istersek

//1.yol
//var silinecekveriler = await context.Customers.ToListAsync();
//context.Customers.RemoveRange(silinecekveriler);
//await context.SaveChangesAsync();

//2.Yol
//var silinecekveriler = await context.Customers.ToListAsync();
//foreach (Customer silinecekveri in silinecekveriler)
//{
//    context.Remove(silinecekveri);
//}
//await context.SaveChangesAsync();
#endregion

#region TPH'da Verik Güncelleme
//Thp davranışında güncelleme operasyonu yine entity üzerinden gercekleştirilecektir.

//Employee guncelle = await context.Employees.FindAsync(4);
//guncelle.Name = "Hilmi";
//await context.SaveChangesAsync();

#endregion

#region TPH'da Verik Sorgulama
//Veri sorgulama operasyonu bilinen DbSet propertysi üzerinden sorgulamadir. Ancak burada dikkat edilmesi gereken bir husus vardır. O da şu;

//var employees = await context.Employees.ToListAsync(); //Technicins dan da veriler gelir(Kalıtım verdıgımız icin)
//var techs = await context.Technicians.ToListAsync(); // SAdece Technicians verileri gelir.

//kalıtımsal ilişkiye gmre yapılan sorgulamada üst sınıf alt sınıftaki verileride kapsamaktadır. O yüzden üst sınıfların sorgulamalarında alt sınıfların verileride gelecektir. buna dikkat edilmelidir.
//Sorgulama süreclerinde EF Core generate edilen sorguya bir where şartı eklenmektedir.
#endregion

#region Farklı entity'ler de aynı isimde sütünların olduğu durumlar
//Entitiylerde aynı olan (mükerrer) kolonlar olablir. Bu kolonları EF Core isimsel olarak özelleştirip ayırıcaktır.
#endregion

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee :Person
{
    public string? Department { get; set; }
}

class Customer : Person
{
    public string? CompanyName  { get; set; }
}

class Technician :Employee
{
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("Ayirici"); //Adını değiştirme
            //.HasValue<Person>("A")
            //.HasValue<Employee>("B")
            //.HasValue<Customer>("C")
            //.HasValue<Technician>("D");
        //Eğerki değerler entitylerin adı yazmasını istemiyorsak bunları yparız
    }
}

















