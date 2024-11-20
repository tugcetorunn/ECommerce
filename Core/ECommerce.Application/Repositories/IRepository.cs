using ECommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; } // hangi entity olursa olsun içindeki property leri getirecek set olmayacak burada set yapmayız.
    }
}
