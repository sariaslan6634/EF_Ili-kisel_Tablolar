// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

//ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");


#region One to One İlişkisel Senraryolarda veri  güncelleme

#region 1.durum | Esas tablodaki veriye bağımlı veriyi değiştirme
//Dikkat var olan adresi değiştirmiyoruz komple orayı silip yeniden adress  tanımlıyoruz.
//Bize idsi 1 olan person'ı getir ama Include(yanında) ilede address bilgisinide getir.
//Person? person =await context.Persons
//    .Include(a => a.Address)
//    .FirstOrDefaultAsync(p=>p.Id ==1);
////Address bilgisini komple sildik
//context.Addresses.Remove(person.Address);
////Yeni adresi ekliyoruz.
//person.Address = new()
//{
//    PersonAddress = "Yeni Adres"
//};
//await context.SaveChangesAsync();

#endregion

#region 2.durum | bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
//addres tablosundan id = 1 olanın bilgilerini bana getir.
//Address? address = await context.Addresses.FindAsync(1);
//context.Addresses.Remove(address); //bunun adress bılgısını sil
//await context.SaveChangesAsync(); //kaydediyoruz.

////Persons tablosundan id = 2 olanın bilgilerini bana getir.
//Person? person = await context.Persons.FindAsync(2);
//address.Person = person; //persons id sini al ve adress personun idsine 
//await context.AddAsync(address); //ekle
//await context.SaveChangesAsync(); //kaydet

//Not address olan veririn bilgilerini elimize aldık ve sildikten sonra tablodan hala bellekte duruyor
//bu yüzden tekrardan kullanabiliyoruz.

//Yeni bir person eklersek aynı adrese
//address.Person = new()
//{
//    Name = "Yeni Personel"
//};
#endregion

#endregion

#region One to Many İlişkisel Senraryolarda veri  güncelleme

#region Saving
//Blog blog = new Blog()
//{
//    Name = "İbrahimsariaslan.com",
//    Posts = new List<Post>
//    { 
//        new Post() { Title = "1.Post"},
//        new Post() { Title = "2.Post"},
//        new Post() { Title = "3.Post"}
//    }
//};
//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion

#region 1.durum | Esas tablodaki veriye bağımlı veriyi değiştirme

//bloglar tablosundan id si 1 olanlanı getir ve onunla ilişkisi olan postlarıda getir.
//Blog? blog = await context.Blogs
//    .Include(b=>b.Posts)
//    .FirstOrDefaultAsync(b =>b.Id == 1);
////id = 2 olanı sil
//Post? silinecekPost = blog.Posts.FirstOrDefault(b => b.Id == 2);
////silinecekPost'u sil
//blog.Posts.Remove(silinecekPost);
////4 ve 5. postları ekle
//blog.Posts.Add(new() { Title = "4. Post" });
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion

#region 2.durum | bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
////id si 4 olan postu getir.
//Post? post = await context.Posts.FindAsync(4);
////2.blog'a ekle 
//post.Blog = new()
//{
//    Name ="2.Blog"
//};
//await context.SaveChangesAsync();

//Eğerki var olan bir blog'a baglamak istiyor isek
//Post? post = await context.Posts.FindAsync(5); //id = 5 olan postlar tablosundaki veriyi getir.
//Blog? blog = await context.Blogs.FindAsync(2); //id = 2 olan Bloglar tablosundaki veriyi getir
////post'üstünden Blog prop'unun icerissine at sonra
//post.Blog = blog; 
////kaydet
//await context.SaveChangesAsync();



#endregion


#endregion

#region Many to Many İlişkisel Senraryolarda veri  güncelleme
#region Saving

//Book book1 = new() { BookName = "1. kitap" };
//Book book2 = new() { BookName = "2. kitap" };
//Book book3 = new() { BookName = "3. kitap" };

//Author author1 = new() { AuthorName ="1. Yazar" };
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

#region Örnek 1->id =  1 olan kitaba 3.Yazarıda ekleme

//Book? book = await context.Books.FindAsync(1);
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);
//await context.SaveChangesAsync();
#endregion

#region Örnek 2->id = 3 olan Yazarın sadece 1.Kitabı yazmıs olması(2 ve 3 ü siliceğiz)

//Author? author = await context.Authors
//    .Include(u=> u.books)
//    .FirstOrDefaultAsync(y=> y.Id == 3);

//foreach (var book in author.books)
//{
//    if (book.Id != 1)
//    {
//        author.books.Remove(book);
//    }
//}
//await context.SaveChangesAsync();
#region id = 1 olan yazarın ilişkisini keselim id =3 olan yazarla ilişki yapalım 4.yazarı ekleyim kitapid =2
//Book? book = await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 2);

//Author? silinecekYazar = book.Authors.FirstOrDefault(y=>y.Id ==1);
//context.Authors.Remove(silinecekYazar);

//Author? eklenecekYazar = await context.Authors.FindAsync(3);
//book.Authors.Add(eklenecekYazar);

//book.Authors.Add(new() { AuthorName = "4.Yazar" });

//await context.SaveChangesAsync();

#endregion
#endregion

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

//class ApplicationDbContext:DbContext
//{
//    public DbSet<Person>Persons { get; set; }
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
//    }

//}