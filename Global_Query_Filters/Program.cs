using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");

#region Global Query filters Nedir ?
//Bir entitye özel uygulama seviyesinde genel/ön kabullü sartlar oluşturmamızı ve böylece verileri global bir şekilde filtrelememizi sağlayan bir özelliktir.
//Böylece velirtilen entity üzerinden yapılan tüm sorgulamalarda ekstradan bir şart ifadesine gerek kalmaksızın filtreleri otomatik uygulayarak hızlıca sorgulama yapmamızı sağlamaktadır.

//Genellikle uygulama bazında aktif(IsActive) gibi verilerle çalışıldığı durumlarda,
//MultiTenancy uygulamalarda TenantId tanımlarken vs. kullanılabilir.

#endregion

#region Global Query Filters Nasıl Uygulanır ?
//Her sorguda where eklemek yerine OnModelCreating gidip oradan otamatik gelmesini bildirebiliriz.
//await context.Persons.Where(p=>p.IsActive).ToListAsync();
#endregion

#region Navigation Property Üzerinden Global Query Filters Kullanımı
//Her kosula orders > 0 eklemek yerine global olarak tanımlarsak direkt sorguda otomatik olarak kendi gelmektedir.
//var p1 = await context.Persons
//	.AsNoTracking()
//	.Include(p => p.Orders)
//	.Where(p => p.Orders.Count > 0)
//	.ToListAsync();

//var p2 =await context.Persons.ToListAsync();

#endregion

#region Global Query Filters Nasıl Ignore Edilir - IgnoreQueryFilters() 
//Bu şartı eğer o anlık kapatmak istiyorsak IgnoreQueryFilters() kullanırız.
//var person = await context.Persons.IgnoreQueryFilters().ToListAsync();
#endregion

#region Dikkat Edilmesi Gereken Husus!
//global query Filter uygulanmış bir kolona farkında olmkasızın şart uygulanabilmektedir.Bu duruma dikkat edilmelidir.

//Biz zaten global olarak aktif olanları getiriyoruz üstüne birde sorguda kullanırsak ekstra maliyet ve sacma bir sorgu olusturmuz oluruz.
//await context.Persons.Where(p=>p.IsActive).ToListAsync();
#endregion

public class Person
{
	public int PersonId { get; set; }
	public string Name { get; set; }
	public bool IsActive { get; set; }

	public ICollection<Order> Orders { get; set; }
}
public class Order
{
	public int OrderId { get; set; }
	public int PersonId { get; set; }
	public string Description { get; set; }
	public int Price { get; set; }

	public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<Order> Orders { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.Entity<Person>()
			.HasMany(p => p.Orders)
			.WithOne(o => o.Person)
			.HasForeignKey(o => o.PersonId);
		//Bu şekilde.
		modelBuilder.Entity<Person>()
			.HasQueryFilter(p => p.IsActive);

		//Orderlar > 0 ' olan tum verılerı getirir.globaldir.
		//modelBuilder.Entity<Person>()
		//	.HasQueryFilter(p => p.Orders.Count > 0);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
	}
}