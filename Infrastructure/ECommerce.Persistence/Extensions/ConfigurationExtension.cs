using Microsoft.Extensions.Configuration;

namespace ECommerce.Persistence.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetConnectionString()
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerce.WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}
