using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        { 
            // connectionstring i kodların içinde değil bir json, xml, txt veya dış kaynaklı dosyalardan almak daha doğrudur. bunu da configuration paketi 
            // ile sağlıyoruz. fakat başka yerlerde de connectionstringi kullanabiliriz ve single responsibility ye göre extension class oluşturmak 
            // daha doğrudur çünkü burası serviceRegistiration class ı. bunun için aşağıdaki standart kodları buradan alıyoruz. 
            // ConfigurationManager configurationManager = new();
            // configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../Presentation/ECommerce.WebAPI"));
            // configurationManager.AddJsonFile("appsettings.json");

            services.AddDbContext<ECommerceDbContext>(options => options.UseNpgsql(ConfigurationExtension.GetConnectionString()));

        }
    }
}
