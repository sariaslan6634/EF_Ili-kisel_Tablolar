// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();


Console.WriteLine("Hello, World!");

#region Default Convention
//default convention yönteminde 1 e cok ilişki kurarken foreign key karsılıgına gelen bir property tanımlamak zorunda değiliz
//eğer tanımlamazsak EF Core kendisi yapıcaktır.
//tanımlarsak onu baz alıcaktır
//public class Calisan
//{

//    public int Id { get; set; }
//    public int DepartmanId { get; set; }// --> Nah bu

//    public String Adi { get; set; }
//    public Departman Departman { get; set; }
//}
//public class Departman
//{
//    public int Id { get; set; }
//    public String DepartmanAdi { get; set; }
//    public ICollection<Calisan>Calisanlar { get; set; }

//}

#endregion

#region Data Annotations
//Default convention yönetiminde foreign key kolonuna karsılık gelen propery'i tamımladığımızda
//bu property ismi temel geleneksel entity tanımlama kuralına uyumuyorsa eğer data annotations'lar ile müdahalede buluna biliriz.
//public class Calisan{

//    public int Id { get; set; }
//    //DId'nın foreign Key olmasını istersek böyle yapıryoruz.
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }


//    public String Adi { get; set; }
//    public Departman Departman { get; set; }
//}
//public class Departman
//{
//    public int Id { get; set; }
//    public String DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region Fluent API
//
public class Calisan
{

    public int Id { get; set; }
    public int DId { get; set; }
    public String Adi { get; set; }
    public Departman Departman { get; set; }
}
public class Departman
{
    public int Id { get; set; }
    public String DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}

#endregion

public class ESirketDbContext:DbContext
{

    public DbSet<Calisan>Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ESirketDB;User ID = ;Password=;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(c => c.Calisanlar)// buraya kadar okey eğer kendimiz kolon acıcaksak devam(vermemize gerek cunku EFCore bunu kendı otomatıkmen yapıyor EntityAdı+Id)
            .HasForeignKey(c=>c.DId);//DId yenı kolon adı 
    }

}