using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        // dependency injection ioc container a context nesnesini eklemiştir onu burada talep ediyoruz.
        private readonly ECommerceDbContext context; // readrepository şu haliyle ioc container dan gelip gelmeyeceğini bilemez. yani çalıştırmadan önce ioc
                                                     // container a kaydedilmesi lazım repositorynin. yani context ve repository ioc containerda olmazsa manuel
                                                     // bir şekilde kendi contructor ına context nesnesini göndermemiz gerekir. ioc mekanizması böyledir.
        public ReadRepository(ECommerceDbContext _context)
        {
            context = _context;
        }
        // orm de generic yapılanmalarda (repository pattern gibi) generic parametredeki türe ait olabilecek dbContext i bize döndürecek metodları vardır.
        // generic entity yi getirmek için set metodunu kullanırız.
        public DbSet<T> Table => context.Set<T>(); // t entity sini tüm metodlarda kullanacağımız için direk table nesnesini alabiliriz artık.

        public IQueryable<T> GetAll()
            => Table; // table dbset türünde. dbset in içine girersek iqueryable türünde generic bir interface olduğunu görürüz. bu nedenle direk table
                      // nesnesi bize tüm liste şeklinde gelir.

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
            => Table.Where(expression); // where zaten içine tam da gönderilen parametredeki gibi bir ifade istiyor.

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
            => await Table.FirstOrDefaultAsync(expression); // firstOrDefault da zaten içine tam da gönderilen parametredeki gibi bir ifade istiyor.

        public async Task<T> GetByIdAsync(string id) // generic çalıştığımız için parametredeki id ile eşitleyeceğimiz id property si referansı bize gelmiyor.
                                                     // bu tarz çalışmalarda yapılması gereken 2 yol vardır. ya reflection uygulanır ya da marker pattern uygulanır.
                                                     // marker ile yapacağız. repositoryleri oluştururken T generic ifadesini bir class olması zorunluluğu koyduk. 
                                                     // fakat her class ta id var mı bilemeyiz. fakat bizim entity lerin base i bir baseentity class ımız var. tüm
                                                     // entity ler de buradan miras alıyor yani içerisinde entityleri kapsıyor. ayrıca tüm entitylerde olmasını
                                                     // istediğimiz id de bu class ın içinde bu sebeple T yi class gibi belirsiz bir tipin yerine baseEntity
                                                     // türünde olacağını söylersek id ye de ulaşmış oluruz. marker (işaretleyici) mantığında, kullanılan bir
                                                     // interface (tüm entityler ientity adında bir interface ten de türeyebilirdi) veya baseEntity generic
                                                     // yapılanmada o base sınıfa özel oluşturulur. baseentity bizim marker ımız oluyor yani. reflection uzun ve
                                                     // maliyetlidir.
                                                     // => await Table.FirstOrDefaultAsync(property => property.Id == Guid.Parse(id));
            => await Table.FindAsync(Guid.Parse(id));
        

        
    }
}
