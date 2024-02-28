// See https://aka.ms/new-console-template for more information
//using Microsoft.EntityFrameworkCore;
//ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");



#region One To One ilişkisel Senaryolarda veri silme işlemi
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);
//if (person != null)
//{
//    context.Addresses.Remove(person.Address);
//    await context.SaveChangesAsync();
//}
#endregion

#region One To Many ilişkisel Senaryolarda veri silme işlemi
//id = 1 olan bütün postları cagırdık
//Blog? blog = await context.Blogs
//    .Include(b=>b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);
////id =2 olan postu ele aldık
//Post? silinecekPost = blog.Posts.FirstOrDefault(p => p.Id == 2);
////silinecek
//context.Posts.Remove(silinecekPost);
//await context.SaveChangesAsync();



#endregion

#region Many To Many ilişkisel Senaryolarda veri silme işlemi
//1.kitab'a karsılık 2.yazar'ı silinmesi
//Book? book =await context.Books
//    .Include(b=>b.Authors)
//    .FirstOrDefaultAsync(b => b.Id==1);

//Author silinecekYazar = await context.Authors.FirstOrDefaultAsync(b => b.Id==2);
//        // DİKKAT
////context.Authors.Remove(silinecekYazar); //Yazarlar tablosundan yazarı silmeye kalkışır !!!

////Bizim amacımız cross table daki ilişkiyi koparmak
//book.Authors.Remove(silinecekYazar);
//await context.SaveChangesAsync();

#endregion

#region Cascade Delete
//Bu davranış modelleri fLuent API ile konfigüre edilebilmektedir.

//Davranışları denerken migration almayı unutma !

#region Cascade
//Esas tablodan silinen karşı/bağımlı tabloda bulunan ilişkli verilerin silinmesini sağlar.

//Blog? blog = await context.Blogs.FindAsync(1);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync(); 

#endregion

#region SetNull
//Esas tablodan silinen karşı/bağımlı tabloda bulunan ilişkli verilere NULL değerin atanmasını sağlar.

//One to One(1-1) senaryolarda eğer ki foreign key ve Primary key kolonları aynı ise o zaman SetNull davranılını KULLANAMAYIZ!
#endregion

#region Restrict
//Esas tablodan herhangi bir veri silinmeye çalışıldığında o veriye karşılık dependent(bağımlı)table'da 
//ilişkisel veri/veriler varsa eğer bu sefer bu silme işlemini engellemesini sağlar.

#endregion

#endregion

#region Saving Data
//Person person = new()
//{
//Name = "Gençay",
//Address = new()
//{
//PersonAddress = "Yenimahalle/ANKARA"
//}
//};

//Person person2 = new()
//{
//Name = "Hilmi"
//};

//await context.AddAsync(person);
//await context.AddAsync(person2);

//Blog blog = new()
//{
//Name = "Gencayyildiz.com Blog",
//Posts = new List<Post>
//    {
//        new(){ Title = "1. Post" },
//        new(){ Title = "2. Post" },
//        new(){ Title = "3. Post" },
//    }
//};

//await context.Blogs.AddAsync(blog);

//Book book1 = new() { BookName = "1. Kitap" };
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };

//Author author1 = new() { AuthorName = "1. Yazar" };
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);
//await context.SaveChangesAsync();
#endregion

//public class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}
//public class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }
//}
//public class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Post> Posts { get; set; }
//}
//public class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }
//}
//public class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }
//    public ICollection<Author> Authors { get; set; }
//}
//public class Author
//{
//    public Author()
//    {
//        books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<Book> books { get; set; }
//}

//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }
//    public DbSet<Post> Posts { get; set; }
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Book> Books { get; set; }
//    public DbSet<Author> Authors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");

//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);
//        //burda SetNull yapmıyoruz PK'oldugu ıcın
//        //.OnDelete(DeleteBehavior.Cascade);

//        modelBuilder.Entity<Post>()
//            .HasOne(p => p.Blog)
//            .WithMany(p => p.Posts)
//            .OnDelete(DeleteBehavior.Restrict); //Buda verileri silmez

//            //Null degeri alabilir.
//            //.IsRequired(false)
//            //.OnDelete(DeleteBehavior.SetNull);

            
//            //.OnDelete(DeleteBehavior.Cascade);//Cascade edıceksek/Default olarak böyle
//    }

//}