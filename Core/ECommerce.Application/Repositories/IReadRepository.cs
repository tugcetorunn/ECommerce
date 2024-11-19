using ECommerce.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerce.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(); // list ienumarable içindedir. verileri in memory ye çeker onun içerisinde işlemleri yapar. iqueryable yazdığımız şartlar ilgili
                                // veritabanı sorgusuna eklenir. sorgu üzerinde çalışmak istiyorsak iquerable, in memoryde çalışacaksak ienumarable kullanırız.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression); // şarta uygun birden fazla veri getirir. ef core da lambda ile where kullanıp özel
                                                                      // tanımlı fonksiyon oluşturup birden fazla veri getiriyorduk bunun metodlaşmış halidir.
                                                                      // parametre bu filtreyi oluşturmaya yarar. 

        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression); // şarta uygun tek bir veri getirir. FirstOrDefault orm fonksiyonunun async olanını
                                                                      // kullanacağız o yüzden metodları async olarak oluşturduk. yukarıdaki iki çoğul metodda
                                                                      // orm nin async metodu olmadığı için sync olarak oluşturduk.
        Task<T> GetByIdAsync(string id); // id ye göre tek veri getirir.

    }
}
