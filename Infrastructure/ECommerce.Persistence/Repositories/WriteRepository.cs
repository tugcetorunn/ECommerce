using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext context;
        public WriteRepository(ECommerceDbContext _context)
        {
            context = _context;
        }
        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity); // addasync geriye entityentry<t> tipinde değer dönüyor.
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities); // addRangeAsync void tipli yani dönüş tipi yok
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T value = await Table.FirstOrDefaultAsync(property => property.Id == Guid.Parse(id));
            // EntityEntry<T> entityEntry = Table.Remove(value);
            // return entityEntry.State == EntityState.Deleted; // entity alan remove fonksiyonu zaten bu iki satırdaki işlemi yapıyor onu kullanalım.
            return Remove(value);
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> entities)
        {
            Table.UpdateRange(entities); // void tipinde
            return true;
        }

        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();

    }
}
