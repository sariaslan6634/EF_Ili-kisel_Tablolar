using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("helloo");
ESirketDbContext context = new();

#region Default Convention
//iki entity arasındaki ilişkiyi navigation propertyler  üzerinde çoğul olarak kurmalıyız.
//(ICollection<>,List<>)
//default conventoin'da cross table'ı manuel olusturmak zorunda degılız. Efcore tasarıma uygun bir şekilde cross table'ı kendısı otomatık basacak ve generate edecektir.
// ve olusturulan cross table'ın ıcerısınde composite primary Keyi'i de otomatik oluşturmuş olackatır.
//public class Kitap
//{
//    public int Id { get; set; }
//    public String KitapAdi { get; set; }
//    public ICollection<Yazar>Yazarlar { get; set; }
//}
//public class Yazar
//{
//    public int Id { get; set; }
//    public String YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}




#endregion

#region Data Annotations
//Cross table manuel olarak oluşturulmak zorundadır.
//Entity'lerde oluşturduğumuz cross table entity'si ile bire cok bir ilişki kurulmalı.
//Cross Table'da composite primary key'i data annotation(attributes)lar ile manuel kuramıyoruz. Bunun icin de fluent API'da çalışma yapmamız gerekiyor.
//Cross Table'a karşılık bir entity modeli oluşturuyorsak eğer bunun context sınıfı icerisnde DbSet property'si olarak bildirmek mecburiyetinde değiliz.
//public class Kitap
//{
//    public int Id { get; set; }
//    public String KitapAdi { get; set; }

//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}
////Cross Table
//class KitapYazar
//{
//    //entity kurallarına uygun
//    public int KitapId { get; set; }
//    public int YazarId { get; set; }

//    //Eğerki ben entity kurallarına uymaz isem kendim Foreign Key olarak belirtmem gerekiyor
//    //[ForeignKey(nameof(Kitap))]
//    //public int KId { get; set; }

//    //[ForeignKey(nameof(Yazar))]
//    //public int YId { get; set; }

//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}
//public class Yazar
//{
//    public int Id { get; set; }
//    public String YazarAdi { get; set; }

//    public ICollection<KitapYazar> Kitaplar { get; set; }
//}


#endregion

#region Fluent API
//Cross table manuel olarak oluşturulmalı DbSet olarak eklenmesine gerek yok
//Composite PK Haskey metodu ile kurulmalı!
public class Kitap
{
    public int Id { get; set; }
    public String KitapAdi { get; set; }

    public ICollection<KitapYazar> Yazarlar { get; set; }
}
public class KitapYazar
{
    public int YazarId { get; set; }
    public int KitapId { get; set; }

    public Yazar Yazar  { get; set; }
    public Kitap Kitap { get; set; }

}

public class Yazar
{
    public int Id { get; set; }
    public String YazarAdi { get; set; }

    public ICollection<KitapYazar> Kitaplar { get; set; }

}
#endregion




public class ESirketDbContext : DbContext
{

    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = EKitapDB;User ID = ;Password=;");
    }
    //Data Annotations kullanırken yapmamız gerekiyor.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(u => new { u.KitapId, u.YazarId });
    //    //Kurallara uymaz isek 
    //    //    .HasKey(u => new { u.KId, u.YId });
    //}

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //PKey i oluşturuyoruz
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });

        // ilişkileri kuruyorum
        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Kitap)
            .WithMany(k => k.Yazarlar)
            .HasForeignKey(ky => ky.KitapId);


        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(k => k.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);
    }
}