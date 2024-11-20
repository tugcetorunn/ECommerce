using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext context; 
        public ReadRepository(ECommerceDbContext _context)
        {
            context = _context;
        }
        
        public DbSet<T> Table => context.Set<T>(); 

        public IQueryable<T> GetAll(bool tracking = true)
        {
            // table dbset türünde. asnotacking ıqueryable türünde verilerle çalışıyor. o yüzden önce ıqueryable a döndürüyoruz.
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            IQueryable<T> query = Table.Where(expression);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //  => await Table.FirstOrDefaultAsync(entity => entity.Id == Guid.Parse(id));
        //  => await Table.FindAsync(Guid.Parse(id)); // findasync veya find ıqueryable da olmadığı için marker kullanıyoruz.
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(entity => entity.Id == Guid.Parse(id));
        }


        // ef core aracılığıyla veritabanında çekilen sorgular ile gelen dataların otomatik olarak takip edilmesini sağlayan tracking mekanizmasında
        // optimizasyon yapmamız gerek. dbcontext aracılığıyla veritabanında çektiğimiz tüm datalar (1 tane de olsa n tane de olsa) otomatik olarak tracking
        // mekanizması ile takibe alınır. sorgular ile getirdiğimiz bu dataların üzerinde manipülasyon işlemi yapıyoruz. savechanges yaptığımızda yapılan
        // değişikliğin ne olduğunu ef core, tracking ile anlıyor (delete mi, update mi vs). fakat her zaman manipülasyon yapmıyoruz hatta daha çok read işlemi
        // yapılıyor, hem de büyük miktarlarda günlük hayatta. read de değişiklik yok dolayısıyla tracking mekanizmasının çalışması fazladan zaman ve maliyet
        // harcıyor. bu yüzden burada devredışı bırakıyoruz.
    }
}
