using ECommerce.Domain.Entities;

namespace ECommerce.Application.Repositories // customer, order, product klasörleri açtığımız için namespace te ECommerce.Application.Repositories.Customer vs.
                                             // yazıyor. fakat aşağıda ıreadrepo interface ini implemente ederken customer entity sini vermemiz gerekiyor.
                                             // bu sefer program bu namespaceteki customer ı algılıyor ve hata veriyor. bu yüzden klasörleme yapısı kullansak da
                                             // class içinde namespace ten entity yazısını silmemiz gerekiyor.
{
    public interface ICustomerReadRepository : IReadRepository<Customer>
    {
    }
}
