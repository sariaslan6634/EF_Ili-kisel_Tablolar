
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

ApplicationDbContext context = new();

Console.WriteLine("Hello, World!");

#region Primary Key Constraint
//Bir kolononu PK constraint ile birincil anahtar yapmak istiyorsak eğer bunun icin name convention'dan istifade edebliriz.
//ID,Id,entityNameId,entitynameID şeklinde olan tanımlanan tüm propertyler default olarak EF Core tarafından PK constraint olacak şekilde generate edilir.
//Eğerki farklı bir property'e PK kullanacaksan HasKey Fuluent API ile yada Key attribute'u ile yapılır.

#region HasKey Fonksiyonu

#endregion
#region Key Attribute'u

#endregion

#region Alternate Keys - HasAlternateKey
//Bir entity içerisnde PK'e ek olarak her entity instance' icin alternatif bir benzersiz tanımlayıcı işlevine sahip olan bir Key'dir.
#endregion

#region Compısite Alternate Key

#endregion
#region HasName Fonksiyonu ile Primary Key constraint'e İsim verme

#endregion

#endregion

#region Foreign Key Constraint

#region HasForeignKey Fonksiyonu

#endregion
#region ForeignKey Attribute'u

#endregion
#region Composite Foreign Key
#endregion

#region Shadow Property Üzerinden Foreign Key
//OnModelCreating icinde yapıyoruz.
#endregion

#region HasstraintName Fonksiyonu il Primary Key Constraint'e isim verme

#endregion
#endregion

#region Unique Constraint

#region hasIndex - IsUnique Fonksiyonları

#endregion
#region Index,IsUnique Attribute'ları
//Blog sınıfındaki Url'i Uniq yapmak istersek sınıfın basında yapıyoruz
#endregion
#region Alternate Key

#endregion

#endregion

#region Check Constratint

#region HasCheckConstraint

#endregion

#endregion

//[Index(nameof(Blog.Url),IsUnique = true)]
class Blog
{
    public int Id { get; set; }
    public string BlogName { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }

    //[ForeignKey(nameof(Blog))]
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string BlogUrl { get; set; }

    public int A { get; set; }
    public int B { get; set; }

    public Blog Blog { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(x => x.Url)
        //    .HasName(nameof(Blog.Url));

        //modelBuilder.Entity<Blog>()
        //    .HasMany(p => p.Posts)
        //    .WithOne(p => p.blogs)
        //    .HasForeignKey(p => p.BlogId);

        //Shadow property olusturma ve foreignKey yapma constraintName değiştirme
        //modelBuilder.Entity<Blog>()
        //    .Property<int>("BlogForeiginKeyId");

        //modelBuilder.Entity<Blog>()
        //    .HasMany(p => p.Posts)
        //    .WithOne(p => p.Blog)
        //    .HasForeignKey("BlogForeiginKeyId")
        //    .HasConstraintName("OrnekForeignKey");

        //Url'i Uniq yapma
        //modelBuilder.Entity<Blog>()
        //    .HasIndex(b => b.Url)
        //    .IsUnique();
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(b => b.Url);

        ////check etme
        //modelBuilder.Entity<Post>()
        //    .HasCheckConstraint("a_b_check_constraint", "[A] > [B]");
    }
}