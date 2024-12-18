﻿using ECommerce.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerce.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true); // tracking kullanıp kullanılmayacağı bilgisini tutup içeride ona göre işlem uygulanacak.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true); 
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true); 
        Task<T> GetByIdAsync(string id, bool tracking = true); 

    }
}
