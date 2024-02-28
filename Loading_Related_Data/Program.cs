// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;

ApplicationDbContex context = new();

Console.WriteLine("Hello, World!");

#region Loading Related Data

#region Eager Loading
//Eager loading, generate edilen bir sorguya ilişkisel verilerin parça parça eklenmesinin sağlayan ve bunu yaparken irdeli/istekli bir şekilde yapmamızı sağlayen bir yöntemdir.
//Eager loading, Arka planda üretilen sorguya 'JOİN' uygular.
#region Include
//Eager loading operasyonunu yapmamızı sağlayan bir fonksiyondur.
//Yani üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir.

// 2 side aynı birisi tip güvenli
//var employees = await context.Employees.Include("Orders").ToListAsync();
//var employees = await context.Employees
//    .Include(e=>e.Orders)
//    .Include(e=>e.Region) //Birden fazla tablo dahil edilebilir
//    .ToListAsync();
#endregion

#region TheInclude
//ThenInclude,üretilen sorguda Include edilen tabloların ilişkili olduğu diğer tablolarıda sorguya ekleyebilmek için kullanılan bir fonksiyondur
//Eğer ki, Üretilen sorguya include edilen navigation property koleksiyonel bir propertyse işte o zaman bu property üzerinden diğer ilişkisel tabloya erişim gösterilememektedir.
//Böyle bir durumda koleksiyonel propertylerin türlerine erişip o tür ile ilişkili diğer tablolarıda sorguya eklememizi sağlayan fonksiyondur.

//Orderlardan employee ordan dan regiona bunların arasında ICollection veya IList yapılanma olmadıgı ıcn olur.
//var orders = await context.Orders
//.Include(o => o.Employee) -- Bu eski kullanım
//.Include(o => o.Employee.Region) --güncel kullanım bu
//    .ToListAsync();

//Employees Tablasunda Orders kolonu List türünde oldugu icin THENINCLUDE kullandık.
//var regions = await context.Regions
//    .Include(r => r.Employees)
//    .ThenInclude(e => e.Orders)
//    .ToListAsync();
#endregion

#region Filtered Include
//                                    DİKKAT
//Change Tracker'ın aktif olduğu durumlarda Include edilmiş sorgular üzerindeki filitreleme sonuçları belenmeyen olabilkir. 
//Bu durum , daha önce sorgulanmış ve Change Tracker tarafından takip edilmiş veriler arasında filtrenin gereksinimi dışında kalan veriler icin söz konusu olacaktır.
//Bundan dolayı sağlıklı bir filtered ınclude operasyonu için change tracker'ın kullanılmadığı sorguları tercih etmeyi düşünebilirsiniz.

//Sorguda filtreleme yada sıralama yapmak icin kullanılır.

//Sprgulama süreçlerinde Include yaparken sonuclar üzerinde filtreleme ve sıralama gercekleştirebilmemizi sağlar.

//Regions tablosunu getir yanında Employees tablosuda olsun ama employees tablosunun Name kolonunda 'a' harfi olanları getir ve Soyadına göre tersten sırala
//var regions =await context.Regions.Include(r => r.Employees.Where(e => e.Name.Contains("h")).OrderByDescending(e=>e.Surname)).ToListAsync();

//Desteklenen Fonksiyonlar: Where , OrderBy, OrderByDescending , ThenBy , ThenByDescending, Skip , Take
#endregion

#region Eager Loading İçin Kritik Bir Bilgi
//EF Core önceden üretilmiş ve execute edilerek verileri belleğe alınmış olan sorguların verileri,sonraki sorgularda KULLANIR!

//var orders = await context.Orders.ToListAsync();
//var employees = await context.Employees.ToListAsync();
//employ tablosunda ordersların bilgileride gelir cunku yukarda zaten ordes değişkeninin içinde tuttu EF Core ilişkili oldugu ıcın onuda getirdi.
#endregion

#region AutoInclude - EF Core 6
//Uygulama seviyesinde bir entitye karşılık yapılan tüm sorgulamalarda "Kesinlikle" bir tabloya Include işlemi gerçekleştirilecekse eğer bunu her bir sorgu için tek tek yapmaktansa nerjezi bir hale getirmemizi sağlayan özelliktir.

//OnModelCreating'e yazdığımız kod
//modelBuilder.Entity<Employee>()
//    .Navigation(e => e.Region)
//    .AutoInclude();


//var employees = await context.Employees.ToListAsync();
#endregion

#region IgnoreAutoIncludes
//Merkezi AutoInclude'u devre dışı bırakma 
//Bunu sadece bu kod ıcın kapalı oluyor eğer başkası ıcınde kapatmak istiyorsak ondada tekrarlamamız gerekir.
//var employees = await context.Employees.IgnoreAutoIncludes().ToListAsync();
//Bunu yaptıktan sonra REGİON'lar gelmez sadece employees gelir.
#endregion

#region Birbirlerinden türetilmiş(Kalıtımsal (TPT-TPH-TPC)) Entity'ler Arasında Include
//Orders tablosunu da gelıcek verilere eklemek istersek 
//3'ü de aynıdır sn overload kullanıcaksın

#region Cast Operatörü İle Include
//var persons1 = await context.Persons.Include(p => ((Employee)p).Orders).ToListAsync();
#endregion

#region as Operatörü İle Include
//var persons2 = await context.Persons.Include(p => ((p as Employee).Orders).ToListAsync();
#endregion

#region 2. Overload İle Include
//var persons3 = await context.Persons.Include("Orders").ToListAsync();
#endregion
#endregion
#endregion

#region Explicit Loading

//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yaklaşımdır

//Emplooyess tamblosunda id =2 olan ilk veriyi getir. Eğer Employee.Name =="Gençay"'ya o zaman orders tablosundan EmployeeId ==employee.Id si ise  o tablodan o kolonuda getir.

//Ameleus yöntemi 
//var employee = await context.Employees.SingleOrDefaultAsync(e=>e.Id ==2);
//if (employee.Name == "Gencay")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}

#region Reference
//Explicit loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation property'si eğer tekil bir türse bu tabloyu referance ile sorguya ekleyebilmekteyiz.

//var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id ==2);
//..Diğer kodlar
//..Diğer kodlar
//await context.Entry(employee).Reference(e=>e.Region).LoadAsync();
#endregion

#region Collection
//Explicit loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation property'si eğer çoğul(koleksiyone) bir türse bu tabloyu Collection ile sorguya ekleyebilmekteyiz.

//var empleyee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//....
//...
//await context.Entry(empleyee).Collection(e=>e.Orders).LoadAsync();
#endregion

#region Collection'lar da Aggregate Operatörü Uygulamak
////Employees tablosunda id == 2 olan getir.
//var empleyee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//yanında Orders tablosunuda getirir
////ve kaçtane orders oldugunu söyler.
//var count = await context.Entry(empleyee).Collection(e => e.Orders).Query().CountAsync();
#endregion

#region  Collection'lar da  Filtreleme Gerçekleştirmek
//var empleyee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////Bugun satılan işlemleri göstereri.
//var orders = await context.Entry(empleyee).Collection(e => e.Orders).Query().Where(q=>q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
#endregion

#endregion

#region Lazy Loading

//var employee = await context.Employees.FindAsync(2);
//Console.WriteLine(employee.Region.Name);

#region Lazy Loading Nedir ?
//Navigation property'ler üzerinde bir işlem yapılmaya çalışıldığı takdirde ilgili
//propertynin temsil ettiği/karşılık gelen tabloya özel bir sorgu oluşturulup execute edilmesine ve verilern yüklenmesini sağlayan bir yaklaşımdır. 

#endregion
#region Prox'lerle Lazy Loading
//Microsoft.EntityFrameworkCore.Proxies

#region Property'lerin Virtual olması
//Eğer ki proxler üzerinden lazy loading operasyonu gerçekleştireceksek navigation propertylerin virtual ile işaretlenmiş olması gerekmektedir.
//Aksi taktirde patlama meydana gelicektir.
#endregion
#endregion

#region Proxy Olmaksizin Lazy Loading
//Prox'ler tüm platformlarda desteklenmeyebilir.
//böyle bir durumda manuel bir şekilde Lazy loading'i uygulamak mecburiyetinde kalabiliriz.

//Manuel yapılan Lazy Loading operasyonlarında Navigation Propertylerin virtual ile işaretlenmesine gerek yoktur!
#region ILazyLoader Interface'i İle Lazy Loading
//Microsoft.EntityFrameworkCore.Abstractinons --Kütüphanesini yüklememiz gerekiyor.

//var employee = await context.Employees.FindAsync(2);
//Console.WriteLine();
#endregion


#region LazyLoader(Interface'i olmadan[ILazyLoader]) ile manuel tanımlama
/*public class Employee
{
    ILazyLoader _lazyLoader;
    Region _region;

    public Employee()
    {
             
    }
    public Employee(ILazyLoader lazyLoader)
        => _lazyLoader = lazyLoader;

    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public  List<Order> Orders { get; set; }
    public  Region Region
    {
        get => _lazyLoader.Load(this, ref _region);
        set => _region = value;
    }
}
public class Region
{
    ILazyLoader _lazyLoader;
    ICollection<Employee> _employees;

    public Region()
    {
           
    }
    public Region(ILazyLoader lazyLoader)
            => _lazyLoader = lazyLoader;


    public int Id { get; set; }
    public string Name { get; set; }

    public  ICollection<Employee> Employees 
    {
        get => _lazyLoader.Load(this, ref _employees);
        set => _employees = value;
    }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public  Employee Employee { get; set; }
}*/
#endregion

#region Delegate İle Lazy Loading
/*
public class Employee
{
    Action<object, string> _lazyLoader;
    Region _region;

    public Employee()
    {
             
    }
    public Employee(Action<object, string> lazyLoader)
        => _lazyLoader = lazyLoader;

    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public  List<Order> Orders { get; set; }
    public  Region Region
    {
        get => _lazyLoader.Load(this, ref _region);
        set => _region = value;
    }
}
public class Region
{
    Action<object, string> _lazyLoader;
    ICollection<Employee> _employees;

    public Region()
    {
           
    }
    public Region(Action<object, string> lazyLoader)
            => _lazyLoader = lazyLoader;


    public int Id { get; set; }
    public string Name { get; set; }

    public  ICollection<Employee> Employees 
    {
        get => _lazyLoader.Load(this, ref _employees);
        set => _employees = value;
    }
}

public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}
static public class LazyLoadingExtension
{
    public static TRelated Load<TRelated>(this Action<object, string> loader,object entity,ref TRelated navigation, [CallerMemberName] string navigationName =null)
    {
        loader.Invoke(entity, navigationName);
        return navigation;
    }
}
*/
#endregion

#endregion


#region N+1 Problemi
//Burdaki hata her döngüdeki ordes'lar icin sürekli sorgu olusturacak kaç tane orders varsa.

//id =1 olan region'u getir
//var region = await context.Regions.FindAsync(1);
//donguye al
//foreach (var employee in region.Employees)
//{
//      orders'ın icine at
//    var orders = employee.Orders;
      //orders'ları döngüye al
//    foreach (var order in orders)
//    {
//        Hepsini yazdır.
//        Console.WriteLine(order.OrderDate);
//    }
//}
#endregion

//Lazy Loading, Kullanım acısından oldukca maliyetli ve performans dusurucu bır etkıye sahıp yöntemdir.
//O yüzden kullanırken mümkn mertebe dikkatli olmalı  ve özellikle navigation propertylerin döngüsel tektiklenme durumlarında lazy loading'i tercih etmemeye odaklanmalıyız.
//Aksi taktirde her bir tetiklemeye karşılık aynı sorguları üretip execute edecektir.
//Bu durum N+1 problemi olarak nitelendirmekteyiz.

//Mümkün mertebe ilişkisel verileri eklerken lazy loading KULLANMAMAYA özen göstermeliyiz.

#endregion

#endregion

public class Person
{
    public int Id { get; set; }
}
public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public virtual List<Order>Orders { get; set; }
    public virtual Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Employee>Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public virtual Employee Employee { get; set; }
}

class ApplicationDbContex : DbContext
{
    //public DbSet<Person> Persons { get; set;}
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //Employee tablosu her sorgulandıgında yanında kesinlikle Region tablosuda gelsin istiyorsak.
        //modelBuilder.Entity<Employee>()
        //    .Navigation(e => e.Region)
        //    .AutoInclude();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server =(localdb)\MSSQLLocalDB;Database = ApplicationDb;User ID =;Password = ;");
        
        //Prox'i yi aktif etmek icin 2 yöntem vardir
        //1-optionsBuilder.UseLazyLoadingProxies().UseSqlServer();
        optionsBuilder.UseLazyLoadingProxies();
    }
}