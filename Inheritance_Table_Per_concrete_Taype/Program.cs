// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");

#region Table Per Concrete Type (TPC) Nedir ?
//TPC davarnışı kalıtımsal ilişkiye sahip olan entitylerin oldugu çalışmalarda sadece concrate/somut olan entity'lere karşılık bir tablo olusturucak davranış modelidir.
//TPC,TPT'nin daha performanslı versiyonudur.
#endregion

#region TPC Nasıl uygulanır ?
//hiyararşik düzelemde abstrack olan entity üzerinden OnModelCreating üzerinden entity fonksiyonu ile konfigürasyona girip ardından  "UseTpcMappingStrategy" fonksiyonu eşliğinde davranışın TPC olacagını belirleyebiliriz.    
#endregion

#region TPC'de veri ekleme
//await context.Technicians.AddAsync(new() {Name = "Gencay",Surname ="Yıldız",Department ="Bilgi işlem",Branch ="Bilgi" });
//await context.Technicians.AddAsync(new() { Name = "İbrahim", Surname = "Yıldız", Department = "Bilgi işlem", Branch = "Bilgi" });
//await context.Technicians.AddAsync(new() { Name = "Hilmi", Surname = "Yıldız", Department = "Bilgi işlem", Branch = "Bilgi" });
//await context.Technicians.AddAsync(new() { Name = "Mustafa", Surname = "Yıldız", Department = "Bilgi işlem", Branch = "Bilgi" });
await context.SaveChangesAsync();

#endregion

#region TPC'de veri Silme
//Technician sil = await context.Technicians.FindAsync(2);
//context.Technicians.Remove(sil);
//await context.SaveChangesAsync();
#endregion

#region TPC'de veri Güncelleme
Technician guncelle = await context.Technicians.FindAsync(4);
guncelle.Surname = "Başaran";
await context.SaveChangesAsync();

#endregion

#region TPC'de veri Sorgulama
//List<Technician> datas = await context.Technicians.ToListAsync();
//foreach (Technician data in datas)
//{
//    Console.WriteLine(data.Name);
//}

#endregion

abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee : Person
{
    public string? Department { get; set; }
}

class Customer : Person
{
    public string? CompanyName { get; set; }
}

class Technician : Employee
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
        //TPT
        //modelBuilder.Entity<Person>().ToTable("Persons");
        //modelBuilder.Entity<Employee>().ToTable("Employees");
        //modelBuilder.Entity<Customer>().ToTable("Customers");
        //modelBuilder.Entity<Technician>().ToTable("Technicians");

        //                   //TPC
        //Sadece Abstrack sınıfları tanımlıyoruz kactane varsa
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();
    }
}
