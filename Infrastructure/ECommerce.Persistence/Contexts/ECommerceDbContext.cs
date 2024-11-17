using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Contexts
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
            // ioc containerda doldurulmak üzere oluşturulması gerekiyor.
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        // şimdi bunları ioc container a eklememiz gerekiyor api üzerinden erişilmesi için. ayrıca mediatr cqrs yapısını entegre ederken handle ların da
        // erişebilmeleri için. onion arch da katmanlar arası birşeyler gönderirken yaptığımız şey -> registiration yani ister extension üzerinden ister
        // direk api katmanına ıservicecollection nesnesi ile register etmek. bu katmanda oluşturduğumuz serviceRegistration class ı bunu sağlayabilir.
    }
}
