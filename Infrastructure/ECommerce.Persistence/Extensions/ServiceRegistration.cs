using ECommerce.Application.Abstractions;
using ECommerce.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        { // persistence ta daha bir çok service olabilir bunları ioc container a ekleyebilmemiz için bu extension metodunu oluşturuyoruz.
            services.AddSingleton<IProductService, ProductService>(); // uygulamanın çalışmaya başladığı andan duruncaya kadar geçen tüm süre boyunca
                                                                      // yalnızca bir kez oluşturulur ve her zaman aynı nesne kullanılır. (addSingletonda)
        }
        // bu extension ı oluşturduktan sonra ioc container ı barındıran presentation katmanından çağrılması gerekiyor. (program.cs)
        // uygulamayı ayağa kaldırdığımızda ioc container ayağa kalkacak ve program.cs deki fonksiyon ve daha sonra sonra bu extension çalışacak ve
        // persistence taki service i ekleyecek.
    }
}
