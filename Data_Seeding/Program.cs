
using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine("Hello, World!");

// Genel notlar
//Seed Datalar migration'ların dısında eklenmesi ve değiştirilmesi beklenmeyen durumlar icin kullaılan bir özelliktir.

#region Data Seeding Nedir ?
//EF Core ile inşa edilen veritabanı içersisinde veritabanı nesneleri olabileceği gibi verilerinde migrate sürecinde ürelilmesini isteyebilir.
//Bu ihtiyaca istinaden seed data özelliği ile EF Core üzerinden migration'larda veriler oluşturabilir ve migrate ederken bu verileri hedef tablolarımaza basabiliriz.

//Seed datalar migrate süreçlerinde hazır verierli tablolara basabilmek icin bunları yazılım kısmında tutmamızı gerekmtirmektedirler.
//Böylece bu veriler üzerinde veritabanı seviyesinde istenilen manipülasyonlar gerçekleştirilebilmektedir.
#endregion

//Data Seedin özelliği şu noklatarda kullanılabilir;
//1-Test için geçici verilere ihtiyaç varsa
//2-ASP.NET Core'daki Identity yapılanmasındaki roller gibi static değerlerde tutabiliriz.
//3-Yazılım için temel konfigürasyonel değerler.

#region Seed Data Ekleme
//OnModelCreating metodu iöerisnde Entity fonksiyonundan sonra çağırılan HasData fonksiyonu ilgili entitye karşılık Seed Data'ları eklemizi sağlayan bir fonksiyondur.

//PK deperlerinin manuel olarak bildirilmesi/verilmesi gerekmektedir.
//Neden ?
//diye sorarsak eğer ilişkisel verileri de Seed Datalarla üretebilmek için...

#endregion

#region İlişkisel tablolar icinde Seed Data Ekleme
//İlişkisel senaryolarda dependent table'a veri eklerken foreign Key kolonunu propertysi varsa eğer ona ilişkisel değerini vererek eklme işlemini yapıyoruz
#endregion

#region Seed Datanın Primart Key'ini Değiştirme
//Eğer ki migrate edilen herhangi bir seed datanın sonrasında PK'i değiştirilirse bu datayla varsa
//ilişkisel  başka veriler onlara cascade darvarnışı sergiler.(kısacası ilişkili olduğu veriler silinir.)
#endregion

class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Context { get; set; }

    public Blog Blog { get; set; }
}
class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ICollection<Post>Posts { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasData(
            new Blog() {Id = 1 , Url = "www.ibrahimsariaslan.com/blog" } ,
            new Blog() { Id = 2, Url = "www.Nesine.com/blog" }
            );

        modelBuilder.Entity<Post>()
            .HasData(
            new Post() { Id = 1, BlogId = 1, Title = "A", Context = "..." },
            new Post() { Id = 2, BlogId = 1, Title = "B", Context = "..." },
            new Post() { Id = 3, BlogId = 2, Title = "B", Context = "..." }
            );
    }
}






