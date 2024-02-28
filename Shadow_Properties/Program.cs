// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");


#region Shadow Properties - Gölge Özellikler
// Entity sınıflarında fiziksel olarak tanımlanmyan/modellenmeyen ancak EF Core tarafından ilgili entity icin var olan/var olduğu kabul edilen property'lerdir.

//Tabloda gösterilmesini istemediğimiz/lüzümlu görmediğiğimmiz / entity instance'ı üzerinde işlem yapmayacagımız kolonlar ıcın shadow propertyler kullanılabılir.

//Shadow property'lerin degerleri ve stateleri change Tracker tarafından kontrol edilir.
#endregion

#region Foreign Key - Shadow Properties
//İlişkisel senaryolarda foreign key property'sini tanımlamadığınız halde dependent Entity'e eklenmektedir
//işte bu shadow property'dir.

//var blogs = await context.Blogs.Include(b => b.Posts).ToListAsync();

#endregion

#region Shadow Property Oluşturma
//Bir entity üzerinde shadow property oluşturmak istiyorsak Fluent API'yi kullanmamız gerekmektedir. 
//ModelBuilder'a gidiyoruz

//modelBuilder.Entity<Hangi Entityde yapıcak>()
//            .Property<Türü>("ADI");
#endregion

#region Shadow Property'e Erişim Sağlama

#region ChangeTracker ile Erişim
//Shadow prop'lara erişim sağlamak icin ChangeTracker dan erişilebilir.

//var blog = await context.Blogs.FirstAsync();
// erişim istiyorsak
//var createDate = context.Entry(blog).Property("CreatedDate");
//Console.WriteLine(createDate.CurrentValue);
//Console.WriteLine(createDate.OriginalValue);

//// icindeki veriyi değiştirmke istiyorsak
//createDate.CurrentValue = DateTime.Now;
//await context.SaveChangesAsync();
#endregion

#region EF.Property İle Erişim
//Özellikle LİNQ sorgularında Shadow Property'lerine erişim icin EF property statick yapılanmasını kullanabiliriz.

//postların oluşturulma tarihlerin sıralamak istersek
//var blogs =  await context.Blogs.OrderBy(b => EF.Property<DateTime>(b, "CreatedDate")).ToListAsync();

//2020 den buyuk olanları getırmek istersek

//var blogs = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();

#endregion

#endregion

public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }

    public Blog Blog { get; set; }  
}

class ApplicationDbContext : DbContext
{
    public DbSet<Blog>Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Blog>()
    //        .Property<DateTime>("CreatedDate");
    //}
}