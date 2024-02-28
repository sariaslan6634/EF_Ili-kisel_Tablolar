
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();

#region OnModelCreating
//Genel anlamda veri tabanı ile ilgili konfigürasyonel operasyonların dışında Entityler üzerinde Configürasyonel çalışmamızı sağlayan bir fonksiyondur.

#endregion

#region IEntityTypeConfiguration<T> Arayüzü
//Entity bazlı yapılacak olan konfigürasyonları o entity'e özel harici bir dosya üzerinde yapmamızı sağlanyan bir arayüzdür.
//Harici bir dosyada konfigürasyonların yürütülmesi merkezi bir yapılandırma noktası oluşturmamızı sağlamaktadır.
//Harici bir dosyada konfigürasyonların yürütülmesi entity sayısının fazla oldugu senerayaolarda yonetılebılırlığı arttıracak ve yapılandırma ile ilgili geliştiricinin yükünü azaltacaktır.

#endregion

#region ApplyConfiguration Metodu
//Bu metot harici konfigürasyonel sınıflarımız EFCore'a bildirmek icin kullandığımız bir metotdur.
#endregion

#region ApplyConfigurationsFromAssembly Metodu
//uygulama bazında oluşturulan harici konfigürasyonel sınfların her biribni onmodelCreating metodunda ApplyConfiguration ile tek tek bilrdirmek yerine
//Bu sınıfın Assemply'i bildirerek IEntityTypeConfiguration arayüzünden türeyen tüm sınnıfları ilgili entitye karşılık konfigürasyonel değer olarak baz almasını tek kalemde gercekleştirmemizi sağlayan metotdur.
#endregion






class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}
//Order Sınıfına ait yapıları burada kuruyor OnModelCreating de tanımlıyoruz.
class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(p=>p.Description)
            .HasMaxLength(50);
        builder.Property(p => p.OrderDate)
            .HasDefaultValueSql("GetDate()");
    }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDb;User ID=;Password=");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Tanımladıgımız ordeConfiguration'u burada calıstırıyoruz
        //modelBuilder.ApplyConfiguration(new OrderConfiguration());

        //Hepsini tekte baglarsak
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}