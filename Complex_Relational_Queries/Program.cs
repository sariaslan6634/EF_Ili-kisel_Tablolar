// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region Complex Query Operators

#region Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();

#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo,person) => new 
//    {
//        person.Name,
//        photo.Url,
//    });
//var datas =await query.ToListAsync();
#endregion

#region Multiple Columns Join
//Birden fazla kolonla sorgulama yapıcaksak 
#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on new { photo.PersonId, photo.Url } equals new { person.PersonId, Url = person.Name }
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();
#endregion

#region Method Syntax
//ilk tablomuzu secıyoruz ve join diyip 2.tabloyu secıyoruz
//var query = context.Photos
//    .Join(context.Persons,
//    //ilk tabloda karsılastırıcagımız yerlerı yazıyoruz
//    photo => new 
//    {
//        photo.PersonId,
//        photo.Url
//    },//2. tablodada oyle yapıyoruz Dikkat = Burda isimleri aynı olucak personda Url yok biz onu Url'nin icine attık
//    person => new 
//    {
//        person.PersonId,
//        Url = person.Name
//    },//2 tabloyu barındırıcak yer yaptık
//    (photo,person) => new
//    { //istediklerimizi yazdırdık
//        person.Name,
//        photo.Url
//    });
//var datas = await query.ToListAsync();
#endregion

#endregion

#region 2'den fazla Tabloyla Join
//2 den fazla tabloyu joinleme
#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            join order in context.Orders
//                on person.PersonId equals order.PersonId
// join tekrar eklemek istersek join diyip eklıyoruz.
//            select new
//            {
//                person.Name,
//                photo.Url,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//ilk 2 tablo işlemlerini yapılıyor sonra tekrar join atılıyor ve 3. tabloda ekleniyor.
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.PersonId,
//        person.Name,
//        photo.Url
//    })
//    .Join(context.Orders,
//    oncekisorgu => oncekisorgu.PersonId,  ---oncekisorgu adının yerine personPhotos adı verebilirsi.
//    order => order.PersonId,
//    (oncekisorgu, order) => new
//    {
//        oncekisorgu.Name,
//        oncekisorgu.Url,
//        order.Description
//    });
//var datas = await query.ToListAsync();
#endregion

#endregion

#region Group Join - GroupBy DEĞİL !
//personOrders = ordes'ları gruplar 
//Order'ları grup joinden cıkartmak istersek 2.Adıma gıdıyoruz.
//group join'den cıkartırsak orderları personOrders'ı kullanamayız.

//var query = from person in context.Persons
//            join order in context.Orders            //Group join
//                on person.PersonId equals order.PersonId into personOrders 
////2.Adım-   from order in personOrders
//            select new
//            {
//                person.Name,
//                Count = personOrders.Count(),
//                //order.Person --> şeklinde ulasabılırız.(içeriğine)
//            };
//var datas = await query.ToListAsync();
#endregion
#endregion

//Left ve Right joinler sadece Query Syntaxla yapılabilir.
#region Left Join
//DefaultIfEmpty: Sorgulama sürecinde ilişkisel olarak karşılığı olmayan verilere default değerini yazdıran yani Left join sorgusunu oluşturan bir fonksiyondur.

//2 tabloyu bırlestırdık ve group join yaptık
//sonra personOrder.DefaultIfEmpty(default değerlerini yada boş olanı göster)  group joinden cıkartık

//var query = from person in context.Persons
//            join order in context.Orders
//                on person.PersonId equals order.PersonId into personOrder
//            from order in personOrder.DefaultIfEmpty()
//            select new 
//            {
//                person.Name,
//                order.Description
//            };
//var datas = await query.ToListAsync();

#endregion

#region Right Join
//Ef Core'da Right join yoktur ama Leftin tersi oldugu icin bu mantıkta yapabılırız
//Order ve personun yer değiştirmiş hali

//var query = from order in context.Orders
//            join person in context.Persons
//                on order.PersonId equals person.PersonId into orderPerson
//            from person in orderPerson.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion

#region Full Join
//Ef Core'da Full join yoktur ama Left ve right'ı yaparsak (right leftin tersi oldugu icin ) bu mantıkta yapabılırız

//Left Join yapıyoruz
//var leftQuery = from person in context.Persons
//                join order in context.Orders
//                    on person.PersonId equals order.PersonId into personOrders
//                from order in personOrders.DefaultIfEmpty()
//                select new
//                {
//                    person.Name,
//                    order.Description
//                };

//Right Join yapıyoruz
//var rightQuery = from order in context.Orders
//                 join person in context.Persons
//                    on order.PersonId equals person.PersonId into orderPerson
//                 from person in orderPerson.DefaultIfEmpty()
//                 select new
//                 {
//                     person.Name,
//                     order.Description
//                 };

//Left ve Right joinleri olusturduk sımdı full join yapalım

//var fullJoin = leftQuery.Union(rightQuery);
//var datas =await fullJoin.ToListAsync();

#endregion

#region Cross Join
//Hangisini önce yazdığımız fark etmez istersen orders tablosunu once yaz ıstersen Persons tablosunu.

//var query = from order in context.Orders
//            from person in context.Persons
//            select new 
//            {
//                //Tüm verileri getirir.
//                order,
//                person
//            };
#endregion

#region Collection Selector'da Where Kullanma Durumu
//Cross join olusturuyoruz WHERE yaptıgımızda collection selector

//var query = from order in context.Orders
//            from person in context.Persons.Where(p => p.PersonId == order.PersonId)
//            select new
//            {
//                order,
//                person
//            };
//var datas = await query.ToListAsync();
#endregion

#region Cross Apply
//Inner Joine benzer
//Cross joine select eklersek cross apply olurs

//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name)
//            select new 
//            {
//                person,
//                order
//            };

//var datas = await query.ToListAsync();
#endregion

#region Outer Apply
//Left Joine benzer
//cross apply'ye DefaultIfEmpty eklersek puther apply olur 

//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name).DefaultIfEmpty()
//            select new
//            {
//                person,
//                order
//            };

//var datas = await query.ToListAsync();
#endregion

#endregion

public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }

    public Person Person { get; set; }
}

public enum Gender {Man,Woman }

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }

    public Photo Photo { get; set; }  
    public ICollection<Order> Orders { get;}

}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Photo>()
            .HasKey(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Person)
            .HasForeignKey<Photo>(p => p.PersonId);

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