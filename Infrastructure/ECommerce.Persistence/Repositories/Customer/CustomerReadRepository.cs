using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        // sadece ıcustomerreadrepo yu implemente edersek readrepository deki tüm metodları customer a göre customize etmemiz gerekir. ama zaten biz repository
        // leri generic oluşturduk bunu yapmamız gerekmiyor. bu gereklilik de hata doğuruyor bunu önüne geçmek için readrepo concrete ini implemente edersek
        // zaten implemente etmemiz zorunda kılınan metodları implmemnte etmiş oluyoruz. yani customer entity sini generic repositorylere göndermek için bu
        // interface in yanında readrepo concrete class ı implemente etmemiz gerek. ve bunu yapınca da reponun ihtiyacı olan context i buradan gönderiyoruz.
        // readrepo ve ıcustomerreadrepo nun ikisini de implemente ederek customer servicelerde tek bir class (customerreadrepo) ile iletişime geçme imkanı
        // sağlarız. yani özelleştirilmiş ve genel repo yu birleştirerek noktayı koyuyoruz.
        // readrepo base class ı ile ı customerreadrepo yu uygulamış oluyoruz çünkü kökeninde readrepo generic class ı var.
        // icustomerreadrepo ve diğer entity lerle oluşturulan abstract repolar entitylere özel metodlar yazabilmemiz için oluşturuldu.
        // sadece readrepo<customer> implemente etseydik genel oluşturulan read repo da customer nesnesini kullanmış halde metodlar elde ederdik. diğer
        // interface i de ekleyerek özelleştirilmiş metodlara da yer açmış olduk.
        public CustomerReadRepository(ECommerceDbContext _context) : base(_context)
        {
            // readrepo ya context i göndermeden kendi elde edemeyeceği için bu class ın bir amacı da budur.
        }
    }
}
