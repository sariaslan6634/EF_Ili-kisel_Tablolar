using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

ESirketDbContext context = new();

Console.WriteLine("Hello, World!");



#region Default Conventon
//Her iki entityde navigation property ile birbirlerini referans ederek tekil olarak fiziksel bir ilişkinin olacagı anlamına gelir.
//One to One ilişki türünde hangisinin default olarak belirlemek pek kolay değildir.
//fiziksel olarak bir Fereign Key 'e karsılık property yada kolan tanımlayarak cozebiliyorz
//Böylelikle foreign Key'e karsılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.


//class Calisan
//{
//    public int Id { get; set; }
//    public String Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; } // Fereign
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}





#endregion

#region Data Annotations
//Navigation property'ler tanımlanmalıdır.
//Foreign Kolanunun ismi default convention'ın dısındda bir kolon olusturulcaksa
//eğer foreignkey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu olusturulmak zorunda degıldır.
//1'e bir ilişkide ekstradan foreign Key kolonuna ihtiyac olmayacagından dolayı 
//Dependent entity'deki id kolonunu hem foreign Key hem de Primary Key olarak kullanmayı tercih ediyoruz.


//class Calisan
//{
//    public int Id { get; set; }
//    public String Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    //Burda id'ye hem PrimeryKey hemde ForeignKey attribute sayesınde verdik.
//    [Key,ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }

//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}



#endregion

#region Fluent API
//Navigation Propertyler tanımlanmalı!
//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı icerinde OnModelCreating fonksiyonunun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir.
//Yani yüm sorumluluk fonksiyon içerisindeki çalışmalardadır.

class Calisan
{
    public int Id { get; set; }
    public String Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
#endregion

class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ESirketDB;User ID = ;Password=;");
    }
    //Model'ların(entity) veritabanında generate edilecek yapıların bu fonksiyon icerisine konfigüre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //CalişanAdresi sınıfındaki Id'ye PremaryKey verdik
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
        //Foreign olan yeri sectik
            .HasForeignKey<CalisanAdresi>(c => c.Id);


        //Böyle de yapılabilir.
        //modelBuilder.Entity<CalisanAdresi>()
        //    .HasOne(c => c.Calisan)
        //    .WithOne(c => c.CalisanAdresi)
        //    .HasForeignKey<CalisanAdresi>(c=>c.Id);


            


    }
}
