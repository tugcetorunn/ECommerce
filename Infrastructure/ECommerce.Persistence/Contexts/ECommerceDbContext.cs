using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Contexts
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        // veri ekleme operasyonunda bir optimizasyon yapacağız. tüm entityler için ortak olan propertylerin veritabanına veri ekleme ve güncelleme
        // sürecinde merkezi bir yerde doldurulması için bir interpolation oluşturacağız. yani veri ekleme isteği gelirken araya giren ve merkezi işleri
        // yürüten yapı (interpolation). veriler çok fazlaysa bu ortak değere sahip olacak property ler büyük bir iş haline gelir. bu yüzden bunu
        // otomatikleştirmeliyiz. bir insert veya update isteği geldiyse bunu program kendi üstlenecek. property nin tanımlandığı entity clss ında bunu 
        // yapmıyoruz. çünkü bu işlem insert ve update metodlarına özel olmalı. 
        // interceptor; bir işin başlangıç ve bitiş zamanının arasına girip request te gönderilen datayı yakalayıp işlem yapabilen yapıdır. burada ise
        // örn. insert için gelen datayı interceptor tutacak ve tüm veriler için ortak olan createdDate değerini kendi içinde atayacak (CreatedDate =
        // DateTime.UtcNow;). interceptor özel bir yapılanma olmak zorunda değil bu bir class olabilir bir metod olabilir bir attribute olabilir, araya
        // girebilen her şey olabilir. ef ile çalışıyorsak gelen sorgularla araya girip sorguyu interceptor ile customize ettikten sonra devam etmesini
        // sağlama işini context nesnesi üzerinde override işlemi yapacağımız bir fonksiyon var. bu fonksiyon bizim interceptor ımız -> 
        // sorguları savechanges ile db ye gönderiyoruz bu nedenle interceptor bu metodda olmalı. yani savechanges ı override edersek savechanges her
        // tetiklendiğinde araya girmiş interceptor uygulamış oluruz. 

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) // repository de parametresiz savechangesaync
                                                                                                  // kullandığımız için burada da o metodu seçiyoruz.
                                                                                                  // cancellationtoken default olduğu için onu parametre
                                                                                                  // olarak görmeyebiliriz.
        {
            // yapılan değişiklikleri yakalamak için dbcontextten gelen changeTracker adında bir propertymiz var. entityler üzerinden yapılan
            // değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. update operasyonlarında track edilen verileri yakalayıp elde
            // etmemizi sağlar. insertte zaten yeni veri eklendiği için onun track edilmesine gerek yoktur. changeTracker update edilen veriyi track eder.
            var entryDatas = ChangeTracker.Entries<BaseEntity>(); // entries ile değişiklik yapılarak sürece giren request girdisindeki verileri
                                                                  // yakalıyoruz. bu girdi de bize IEnumerable<EntityEntry> tipinde gelir. hangi entity
                                                                  // de olacağını bilemeyiz bu yüzden baseentity tipinde olacağını yazıyoruz. girdi bize
                                                                  // tek veri olarak da gelebilir, çoklu da gelebilir bu yüzden ıenumerable. bu sebeple
                                                                  // de verileri gezmemiz ve insert update ayırmamız lazım.

            foreach(var entryData in entryDatas)
            {
                // switch case ile insert ve update i ayırıp işlemi uyguluyoruz. ayırma işlemini repo larda verdiğimiz state değerleri ile yapabiliriz.
                // switch ten önce yazılan kısım sürece giren datanın state değerini gösterir. switch ten sonraki kısım eğer bu state değeri added ise
                // createdDate e şuanki tarihi saati ata, modified ise updatedDate e şuanki tarih ve saati ata.
                _ = entryData.State switch // çıktı dönmesine gerek olmadığı için değer atamıyoruz. (discard yapılandırması)
                {
                    EntityState.Added => entryData.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entryData.Entity.UpdatedDate = DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken); // araya girdikten sonra metodun tekrardan devreye sokulduğu kod.
        }

        // gelen insert ve update request lerinde insertse createdDate i updatese updatedDate i doldurup devam etmesini sağlıyoruz.
    }
}
