
using Microsoft.EntityFrameworkCore;

ApplicationDbContext contex = new();
Console.WriteLine("Hello, World!");

#region Table Per Type (TPT) Nedir ?
//Entitylerin aralarında kalıtımsal ılıskıye her bir türe/entitye/referansa karsılık bir generate eden davranıştır.
//Her generate edilen bu tablolar hiyararşik düzlemde kendi aralarında birebir ilişkiye sahiptir.
#endregion

#region TPT Nasıl Uygulanır ?
//TPT'yi uygulayabilmek icin öncelikle entityleri kendi aralarında insa etmemiz gerekir.
//Entityler DbSet olarak bildirilmelidir.
//Hiyararşik olarak aralarında kalıtımsal ilişki olan tüm entityler OnModelCreating fonksiyonunda ToTable metodu ile konfigüre edilmelidir.
//Böylece EF Core kalıtımsal ilişki olan bu tablolar arasında TPT davranışının olduğunu anlayacaktır.

#endregion

#region TPT'de Veri Ekleme
//Customer c = new() { Name = "Yeliz", Surname = "Saklı",CompanyName = "Halı Kilim" };
//await contex.AddAsync(c);
//await contex.SaveChangesAsync();

#endregion

#region TPT'de Veri Silme
//Employee sil = await contex.Employees.FindAsync(2);
//contex.Employees.Remove(sil);
//await contex.SaveChangesAsync();
#endregion

#region TPT'de Veri Güncelleme
//Employee guncelle = await contex.Employees.FindAsync(1);
//guncelle.Department = "Yazılım ve Bilgi işlem";
//await contex.SaveChangesAsync();
#endregion

#region TPT'de Veri Sorgulama
//var datas = await contex.Persons.ToListAsync();
//foreach (var data in datas)
//{
//    Console.WriteLine(data);
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
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Technician>().ToTable("Technicians");
    }
}
