
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");


//Owned Entity Types => Sahip olunan Entity Türleri
#region Owned Entity Types Nedir ?
//EF Core Entity sınıflarını parçalayarak , propertylerini kümesel olarak farklı sınıflarda barındırmamıza ve tüm bu sınıfları ilgili entity'de birleştirip bütünsel olarak calışmamıza izin vermektedir.
//Böylece bir entity sahip olunan(owned) birden fazla alt sınıfın birleşmesiyle meydana gelebilmektedir.
#endregion

#region Owned Entity Types'ı Neden Kullanırız ?
//Domain Driven Design(DDD) yaklaşımında value object'lere karşılık olarak owned entity types'lar kullanılır.
#endregion

#region Owned Entity Types Nasıl Kullanılır ?
//Normal bir entiy'de farklı sınıfların referans edılmesı prımary key vs. gibi hatalara sebebiyet verecektir. Cunku direkt bir sınıfın referans olarak alınması EF Core tarafından ilişkisel bir tasarım olarak algılanır
//Bizlerin entity icerisindeki orıoertyleri kümesel olarak barındıran sınıfları o entity'nin bir parçası olduğunu bildirmemiz özellikle gerkmektedir.
#region OwnsOne Metodu
//OnModelCreating
#endregion
#region Owned Attribute'u
//sınıfların başında [owned] dersek de olur.
#endregion
#region IEntityTypeConfiguration<T> Arayüzü
//OnModelCreating'ile yapılanların aynısı gibi yapıp OnModelCreating'de aktif ediyoruz.
#endregion

#region OwnsMany Metodu
//OwnsMany metodu, entity'nin farklı özelliklerine başka bir sınıftan ICollection türünde navigation property aracılığı ile ilişkisel olarak erişebilmemizi sağlayan bir işleve sahiptir.
//Normalde Has ilişki olarak kurulabilecek bu ilişkinin temel farkı, Has ilişkisi DbSet property'si gerektirirken OwnsMany metodu ise dbset'e ihtiyaç duymaksızın gercekleştirmemizi sağlamaktadır.

//var d = await context.Employees.ToListAsync();
#endregion

#endregion

#region Sınırlılıklar
//Herhangi bir Owned Entity Type için DbSet Property'sine ihtiyac yoktur.
//OnModelCreating Fonksiyonunda Entity<T> metodu ile Owned Entiy Type türünden bir sınıf üzerinde herhangi bir konfigürasyon gerçekleştirilemez.
//Owned Entity Type'ların kalıtımsal hiyerarşi desteği yoktur.
#endregion

class Employee
{
    public int Id { get; set; }
    public string IsActive { get; set; }

	//Bunların hepsini ayırdık burada da tanımlaya bilirdik(Tasıdık ama aktarmadık)
    //public string Name { get; set; }
    //public string MiddleName { get; set; }
    //public string LastName { get; set; }
    //public string StreetAddress { get; set; }
    //public string Location { get; set; }

	//Buralarda Aktardık.
	public EmployeeName EmployeeName { get; set; }
	public Address Address { get; set; }

	public ICollection<Order> Orders { get; set; }
}
//Ne Id ' nede ForeginKey Tanımlamıyoruz.
class Order
{
    public string OrderDate { get; set; }
    public int Price { get; set; }
}

//[Owned]
class EmployeeName
{
	public string Name { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }
}

//[Owned]
class Address
{
	public string StreetAddress { get; set; }
	public string Location { get; set; }
}

class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
	//OnModelCreating'ile yapılanların aynısı 
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.OwnsOne(e=>e.EmployeeName , builder =>
		{
			builder.Property(e => e.Name).HasColumnName("Name");
			builder.Property(e => e.LastName).HasColumnName("LastName");
		});
		builder.OwnsOne(e => e.Address, builder =>
		{
			builder.Property(e => e.StreetAddress).HasColumnName("StreetAddress");
		});
	}
}


class ApplicationDbContext : DbContext
{
	public DbSet<Employee>Employees { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		#region OwnsOne
		//OwnsOne seklinde tanımlama
		//Bu şekide EmployeeName'ın bir tablo değil bağlı bir sınıf olduğunu belirtiyoruz.
		//modelBuilder.Entity<Employee>().OwnsOne(e => e.EmployeeName, builder =>
		//{ 
		//	//Kolon adlarını özelleştirmezisek o sınıfın adını alır sonra prop'ları yazar
		//	//EmployeeName_Name şeklinde kolon adı olur.
		//	//Burada kolon adını özelleştiriyoruz
		//	builder.Property(e=>e.Name).HasColumnName("Name");
		//	builder.Property(e => e.LastName).HasColumnName("LastName");
		//});
		#endregion
		#region OwnsMany
		modelBuilder.Entity<Employee>().OwnsMany(e => e.Orders, builder =>
		{
			//ForeignKey'ini bildirdik
			builder.WithOwner().HasForeignKey("OwnedEmployeeId");
			builder.Property<int>("Id"); //Id Diye property'ekledik
			builder.HasKey("Id"); //Id'nın Primary Key oldugunu bıldırdık.
		});


		#endregion
		modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
	}
}