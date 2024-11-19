using ECommerce.Application.Repositories;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // 5-controllerda gerekli düzenlemeyi yaptık burayı singleton yapmaya gerek kalmadı.
            // 2-controllerda get metoduna request attığımızda context nesnesi dispose olmuştu addscoped durumundayken. sonra aşağıdaki değişimi yaptık(3);
            // 4-dispose edilmesinin asıl sebebi scoped değilmiş. controllerda açıklama mevcut.
            // 3-adddbcontext in default olarak addscoped lifetime olarak çalıştığını görmüştük. scoped, request esnasından ioc den talep edilen herhangi bir
            // örneğini bir tane oluşturur, o nesneden döndürür. dolayısıyla her requestte yeni bir nesne döndürecektir. productcontrollerda şuan iki tane
            // repository service i talep ediyoruz ve ikisinde de ayrı dbcontext nesnesi türetecek bu haliyle (scoped mantığıyla). bu yüzden aşağıda en içte
            // gönderdiğimiz parametrede bu lifetime ı singletona döndürelim. bu şekilde uygulamaya ait bir tane dbcontext nesnesi oluşturulur. 
            // reposity serviceleri de singleton a geçelim. (ConfigurationExtension.GetConnectionString()), ServiceLifetime.Singleton)
            services.AddDbContext<ECommerceDbContext>(options => options.UseNpgsql(ConfigurationExtension.GetConnectionString()));
            // 1-adddbcontext metodunun addscoped ile register edildiğini görüyoruz üstüne geldiğimizde. aynı lifetimelarda olması için repoları da öyle yapalım.
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            // scoped ile ioc container a bir nesneyi eklediğimizde şöyle davranış sergiliyor -> controller a bir request geldiğinde controller ioc
            // containerdan talepte bulunuyor. ve bu nesne containera scoped ile koyulduysa gelen requeste karşılık container a kaç tane talepte bulunuluyorsa 
            // her birine ve her bir injectiona aynı nesne gönderilecektir. her request için oluşturulan nesne iş bittikten sonra imha (dispose) edilir. başka
            // requestte ise başka bir nesne üretilecek ve o gönderilecektir. ama bütün inject taleplerine aynı nesne gönderilecektir.
            // singletonda uygulamanın herhangi bir noktasına tekil bir tane nesne gönderilir.
        }   // transientte ise herhangi bir requeste herhangi bir inject talebinde direk nesne oluşturulur ve gönderilir. başka bir requestte yine
            // nesne oluşturulur ve gönderilir.
    }
}
