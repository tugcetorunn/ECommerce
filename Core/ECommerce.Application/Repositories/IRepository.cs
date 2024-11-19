using ECommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        // repository pattern ı (veri erişim modeli) kullanmamın temel gereksinimi; bir projede birden fazla veritabanında entity lerimiz olabilir. bunları merkezi bir yapıda tek
        // çatı altında soyutlayabilmek için repository kullanıyoruz. 
        // eğer basit orta ölçekli bir yazılım projesi yapıyor ve tek bir tane veritabanı kullanıyorsak, arada da bir orm aracı kullanıyorsak repository
        // pattern ihtiyacımız yoktur. orm üzerinden rahatça işlemlerimizi yapabiliriz. orm zaten bir tane veritabanına özel bize gerekli custom yaklaşımı
        // sergilememizi sağlıyor. fakat kimi yazılımlarda birden fazla veritabanı kullanmak gerekir.
        // başka bir maddede de örn. cqrs pattern ı kullanıyoruz. command ler ve query ler ayrılıp ayrı database olarak yönetilirler. gerçek sahada bu bu şekilde
        // yani insert, update, delete farklı database de sorgulama işlemleri farklı database de yapılır. farklı veritabanı türlerine uygun şekilde ayrı ayrı 
        // kod yazmaktansa bunları ortak bir zeminde birleştirip kendimize göre özelleştirebiliriz repository ile.
        // repository pattern bir anlamda solide aykırı. çümkü sorgulamalar ve manipülasyon yapan işlemlerin hepsi aynı sınıfta. fakat biz command lerin ve
        // query lerin yazıldığı işlemleri ayırırsak bu problem de ortadan kalkar. hem single resp hem de interface segregation a (sorumlulukları ayır) uyar.
        // bu interface te de global genel olan tüm repository lerin içinde olmasını istedğimiz temel operasyonları yazacağız. mesela hangi db olursa olsun
        // hepsi için bize var olan tüm property leri getirecek bir metod. diğer repository interface leri bu interface ten miras alacak kapsayıcı olduğu için.

        DbSet<T> Table {  get; } // hangi entity olursa olsun içindeki property leri getirecek set olmayacak burada set yapmayız.
    }
}
