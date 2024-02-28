// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

#region RelationShips(ilişkiler) Terimler

#region principal Entity(Asıl entity)
//Kendi basına var olabilen tabloyu modelleyen entity'e denir
//Departmanlar tablosunun modelleyen 'Depertman' entitysidir.
//Örneğin elimizde Calısanlar ve Departmanlar Tablosu olsun
//Calısanlar tablosu tek basına var olamaz ilişkidir.(DepartmanId) den dolayı o olamdan tek basına olamaz
//ama departman tablosunun ilişkisi yoktur o tek basına var olabilir.
#endregion

#region Dependent Entity(Bağımlı Entity)
//Kendi başına var olamayan bir başka tabloya bağımlı(ilişkisel olara)
// olan tabloyu modelleyen entity'e denir 
// Calısanlar tablosunju modelleyen 'Calisan' entity'sidir.
#endregion

#region Foreign Key
//Principal entity ile dependent entity arasındaki ilişkiyi sağlayan key'dir.
//Calısanlar tablosundaki DepartmanId
//Dependent Entity'de tanımlanır.
//Principal Entiy'de ki principal Key'i tutar.
#endregion

#region Princpal Key
//Principal entity deki id'nin kendisidir.
//Principal entiy'nin kimliği olan kolonu ifade eden propertydir.
//Departmanlar tablosundaki Id
#endregion

class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }
    public Departman Departman  { get; set; }
}
class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}

#region Navigation Propert Nedir ?
//İlişkisel tablolar arasındaki fiziksel erişimi entity classları üzerinden sağlayan property'lerdir.
//bir property'nin navigation property olabilmesi icin kesinlikle entity türünden olası gerekiyor.

////Navigation property'ler entity'lerdeki tanımlarına göre
//n'e n yahut 1'e n(coktan coğa,Tekten coğa) şeklinde ilişki türlerini ifade etmektedirler.
//Sonraki derslerimizde ilişkisel yapıları tam teferruatlı pratikte incelerken
//Navigation proeprty'lerin bu özelliklerinden istifade ettiğimizi göreceksiniz.

#endregion

#region İlişki Türleri

#region One to one(bire bir[1-1])
//Çalışan ile adresi arasındaki ilişki(Eğerki aynı şırkette aynı yerde oturan yoksa)
//Karı koca ilişkisi(Her kadının bir tane kocası her kocanın bir tane karısı olur)
#endregion

#region One to Many(Coktan coğa [1-n])
//Calısan ile departman arasındaki ilişki
//Anne ve cocukları arasındaki ilişki(Her cocugun 1 tane annesi vardır. ama annenin birden fazla cocugu olabilir.)

#endregion

#region Many to Many(Coktan coğa [n-n])
//Calısanlar ile projeler arasındaki ilişki
//kardeşler arasındaki ilişki
#endregion

#endregion

#endregion

#region Entity Framework Core'da İlişki Yapılandırma yöntemleri
#region Default Conventions
//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
//Navigation property'leri kullanarak ilişki şablonlarını cıkarmaktadır.
#endregion

#region Data Annotations Attributes
//Entity'nin niteliklerine göre ince ayarlar yapmamızı sağlayan attribute'lardır.
//örnek = [key],[ForeignKey]

#endregion

#region Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı calısmamızı saglayan yöntemdir.

#region HasOne
//İlgili entity'nin ilişkisel entity'ye birebir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion

#region HasMany
//İlgili entity'nin ilişkisel entity'ye coka bir yada çoka cok olacak şekilde ilişşkisini yapılandırmaya başlayan metottur.
#endregion

#region WithOne
//HasOne yada HasMany'den sonra bire bir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#region WithMany
//HasOne yada HasMany'den sonra bire cok yada  çoka çok olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#endregion

#endregion