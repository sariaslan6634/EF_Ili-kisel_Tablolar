﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region Stored Procedure Nedir ?
//Stored Procedure , view'ler gibi kompleks sorgularımızı daha basit bir şekilde tekrar kullanılabilir bir hale getirmemizi sağlayan veritabanı nesnesidir.
//View'ler tablo misali bir davranış sergilerken Store Porecedure'lar ise fonksiyonel bir davranış sergilerler.
//Ve türlü türlü artılarıda vardır.
#endregion
#region EF Core İle Stored Procedure Kullanımı

#region Stored Procedure Oluşturma
//1. Adım : Boş bir migration oluşturunuz.
//2. Adım : Migration'ın içerisindeki Up fonksiyonuna SP'ın Create komutlarını yazınız, Down fonk. ise drop komutlarını yazınız.
//3. Adım : Migrate ediniz.
#endregion
#region Stored Procedure'ü Kullanma
//SP'ı kullanabilmek için bir entity'e ihtiyacımız vardır.
//Bu entityi'nin DbSet propertysi olarak context nesnesine de eklenmesi gerekmektedir.

//Bu DbSet propertysi üzerinden FromSql Fonksiyonunu kullanarak  'EXEC .....' komutu eşliğinde SP yapılanmasını çalıştırıp sorgu neticesini elde edebilirsiniz.
#region FromSql
//var datas = await context.PersonOrders.FromSql($"EXEC sp_PersonOrders").ToListAsync();

#endregion

#endregion

#region Geriye Değer Döndüren Stored Procedure'ü Kullanma

//SqlParameter countParameter = new()
//{
//    ParameterName = "count",  
//    SqlDbType = System.Data.SqlDbType.Int,
//    Direction = System.Data.ParameterDirection.Output
//};

//await context.Database.ExecuteSqlRawAsync($"EXEC @count = sp_bestSellingStaff", countParameter);
//Console.WriteLine(countParameter.Value);

#endregion
#region Parametreli Stored Precedure Kullanımı

#region Input Parametreli Stored Precedure'ü Kullanma

#endregion
#region Output Parametreli Stored Procedure'ü Kullanma

#endregion

//SqlParameter nameParameter = new()
//{
//    ParameterName = "name",
//    SqlDbType = System.Data.SqlDbType.NVarChar,
//    Direction = System.Data.ParameterDirection.Output,
//    Size = 1000
//};

//await context.Database.ExecuteSqlRawAsync($"EXECUTE sp_PersonOrders2 7, @name OUTPUT", nameParameter);
//Console.WriteLine(nameParameter.Value);

#endregion

#endregion


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
 //-> Bu bir tablo değil'demek
[NotMapped]
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
            .HasNoKey();

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