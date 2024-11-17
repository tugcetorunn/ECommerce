using Microsoft.Extensions.Configuration;

namespace ECommerce.Persistence.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetConnectionString()
        {
            // appsettings.json konfigürasyon dosyasının içini okumak için öncelikle Microsoft.Extensions.Configurations ve
            // Microsoft.Extensions.Configurations.Json paketlerini yüklüyoruz. ikinci adım nesne oluşturup dosyaya ulaşma yolunu belirlemek.
            // setbasepath ve addjsonfile fonksiyonlarını Microsoft.Extensions.Configurations.Json paketiyle kullanabiliyoruz.
            ConfigurationManager configurationManager = new();
            // aşağıdaki parametre ile içinde bulunduğumuz bu dosyanın dışına doğru appsettings e giden yolu yazıyoruz. iki dış dosyaya gidip presentation
            // içinden appsettings i bulunduran api projesine gidiyoruz.
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerce.WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}
