using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");

#region Sequence Nedir ?
//Veritabanında benzersiz ve ardışık sayısal değerler üreten veritabanı nesnesidir.
//Sequence herhangi bir tablonun özelliği değildir. Veritabanı nesnesidir. Birden fazla tablo tarafından kullanılabilir.
#endregion

#region Sequence Tanımlama
//Sequence'ler üzerinden değer olustururken veritabanına özgü çalışma yapılması zorunludur.
//SQL Server'a özel yazılan sequence tanımı misal olarak Oracle için hata verebilir.

#region HasSequence

#endregion

#region HasDefaultValueSql

#endregion
#endregion

#region Sequence Yapılandırması

#region StartsAt
//kactan baslasın
#endregion

#region IncrementsBy
//kaçar kacar artsın
#endregion


#endregion

#region Sequence ile Identity Farkı
//Sequence bir  veritabanı nesnesiyken , Identity ise tabloların özellikleridir.
//yani Sequence herhangi bir tabloya bağımlı değildir.
//Identity bir sonraki değeri diskten alırken Sequence ise RAM'den alır.
//Bu yüzden önemli ölçüde Identity'e nazaran daha hızlı performanslı ve az maliyetlidir
#endregion

//await context.Employees.AddAsync(new() { Name = "İbrahim", Surname = "SARIASLAN", Salary = 1000 });
//await context.Employees.AddAsync(new() { Name = "FATMANUR", Surname = "SARIASLAN", Salary = 1000 });
//await context.Employees.AddAsync(new() { Name = "ARİFE", Surname = "SARIASLAN", Salary = 1000 });
//await context.Employees.AddAsync(new() { Name = "ORHAN", Surname = "SARIASLAN", Salary = 1000 });

//await context.Customers.AddAsync(new() { Name = "ALİ" });

//await context.SaveChangesAsync();

class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

}
class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        //modelBuilder.HasSequence("EC_Sequence")//Sequence oluşturma
        //    .StartsAt(100) //100'den başlasın
        //    .IncrementsBy(2); //2şer 2 şer artsın

        //modelBuilder.Entity<Employee>()
        //    .Property(e => e.Id)
        //    .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

        //modelBuilder.Entity<Customer>()
        //    .Property(c => c.Id)
        //    .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
}