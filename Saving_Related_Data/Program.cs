// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

//ApplicationDbContext context = new();


#region One to One ilişkisel senaryolarda veri ekleme işlemi

#region 1.Yöntem -> Principal Entity(Bağımsız) üzerinden dependent Entity(bağımlı) verisi ekleme

//Person person = new();
//person.Name = "İbrahim";
//person.Address = new() { PersonAddress = "Esentepe/istanbul" };

//await context.AddAsync(person);
//await context.SaveChangesAsync();

#endregion

//Eğer ki principal entity üzerinden ekleme geröekleşiyorsa dependenbt entity nesnesi verilmek zorunda  değildir.
//Amma velakin  dependent entity üzerinden ekleme işlemi gercekleştiriliyorsa eğer burada principal entitynin nesnesine ihtiyacımız zaruridir.

#region 2.Yöntem -> dependent Entity(bağımlı) üzerinden Principal Entity(Bağımsız) ekleme

//Address address = new()
//{
//    PersonAddress = "Güzelyurt/istanbul",
//    Person = new() { Name ="Mehmet"}
//};


//await context.AddAsync(person);
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

//public class ApplicationDbContext : DbContext
//{

//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(a => a.Address)
//            .HasForeignKey<Address>(a => a.Id);
//    }

//}


#endregion

#region One to Many İlişkisel Senaryolarda Veri ekleme

#region 1. Yöntem -> Principal Entity Üzerinden Principal Entity Verisi Ekleme

#region Nesne Referansı Üzerinden eklme

//Blog blog = new() { Name = "ibrahimsariaslan.com Blog" };
//blog.Posts.Add(new() { Title = "Post 1" });
//blog.Posts.Add(new() { Title = "Post 2" });
//blog.Posts.Add(new() { Title = "Post 3" });

//await context.AddAsync(blog);
//await context.SaveChangesAsync();

#endregion

#region Object initializer üzerinden ekleme

//Blog blog2 = new Blog()
//{
//    Name = "A blog",
//    Posts = new HashSet<Post>() {new() { Title = "Post 4" }, new() { Title = "Post 5" }}
//};
//await context.AddAsync(blog2);
//await context.SaveChangesAsync();

#endregion

#endregion

#region 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Hiç kullanmayacaksın ama yapmayalım
//Bağımlıdan bağımsıza verı ekleme

//Post post = new()
//{
//    Title = "Post 6",
//    Blog = new() { Name = "B Blog" }
//};
//await context.AddAsync(post);
//await context.SaveChangesAsync();


#endregion

#region 3. Yöntem -> Foreign Key kolonu üzerinden veri ekleme
// 1 ve 2 yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken bu 3. yontemle
//önceden eklemiş olan bir pricipal entity verisiyle yeni dependent entitylerin ilişkisel olarak eşleştirilmesini sağlamaktadır.

//Eğerki foreign key anahatarın varsa bunuda kullanabılırsın
//Post post = new() 
//{
//    BlogId = 1, //foreign key
//    Title = "Post 7"
//};
//await context.AddAsync(post);
//await context.SaveChangesAsync();

#endregion


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

//public class ApplicationDbContext : DbContext
//{

//    public DbSet<Post> Posts { get; set; }
//    public DbSet<Blog> Blogs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");
//    }
//}
#endregion

#region Many to Many İlişkisel Senaryolarda Veri ekleme

#region 1. Yöntem -> default convention
// n t n ilişkisi eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir

//Book book = new()
//{
//BookName = "A Kitabı",
//Authors = new HashSet<Author>()
//    {
//        new() { AuthorName = "Hilmi" },
//        new() { AuthorName = "Fatma" },
//        new() { AuthorName = "Ayşe" },
//    }
//};
//await context.AddAsync(book);
//await context.SaveChangesAsync();


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
//public class ApplicationDbContext : DbContext
//{

//    public DbSet<Book> Books { get; set; }
//    public DbSet<Author> Authors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");
//}
//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    //Authorid ile bookid PK olarak ayarladık
//    modelBuilder.Entity<BookAuthor>()
//        .HasKey(ba => new { ba.AuthorId, ba.BookId });

//    modelBuilder.Entity<BookAuthor>()
//        .HasOne(ba => ba.Book)
//        .WithMany(ba => ba.Authors)
//        .HasForeignKey(ba => ba.BookId);

//    modelBuilder.Entity<BookAuthor>()
//        .HasOne(ba => ba.Author)
//        .WithMany(ba => ba.books)
//        .HasForeignKey(ba => ba.AuthorId);
//}
//}
#endregion

#region 2. Yöntem -> Fluent Api
//n t n ilişkisi eğer ki fluent api ile tsarlanmış ise kullanılan bir yöntemdir

//Yazar eklemek istersek
//Author author = new()
//{
//    AuthorName = "Mustafa",
//    //olan bir kitaba yazarı eklersek
//    books =new HashSet<BookAuthor>()
//    {
//        //olan bir kitaba yazarı eklersek(kitabın id=1)
//        new() {BookId =1},
//        //Yeni kitap veritabanında yok ise
//        new() {Book = new(){BookName = "B kitabı"} }
//    }   
//};
//await context.AddAsync(author);
//await context.SaveChangesAsync();

//public class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<BookAuthor>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }
//    public ICollection<BookAuthor> Authors { get; set; }
//}

//public class BookAuthor
//{
//    public int BookId { get; set; }
//    public int AuthorId { get; set; }
//    public Book Book { get; set; }
//    public Author Author { get; set; }
//}

//public class Author
//{
//    public Author()
//    {
//        books = new HashSet<BookAuthor>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<BookAuthor> books { get; set; }
//}

//public class ApplicationDbContext : DbContext
//{

//    public DbSet<Book> Books { get; set; }
//    public DbSet<Author> Authors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID = ;Password=;");
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        //Authorid ile bookid PK olarak ayarladık
//        modelBuilder.Entity<BookAuthor>()
//            .HasKey(ba => new { ba.AuthorId, ba.BookId });

//        modelBuilder.Entity<BookAuthor>()
//            .HasOne(ba => ba.Book)
//            .WithMany(ba => ba.Authors)
//            .HasForeignKey(ba => ba.BookId);

//        modelBuilder.Entity<BookAuthor>()
//            .HasOne(ba => ba.Author)
//            .WithMany(ba => ba.books)
//            .HasForeignKey(ba => ba.AuthorId);
//    }
//}

#endregion

#endregion



