using Microsoft.Extensions.Configuration;
using System.IO;

namespace MiddleWare
{
    public static class AppConfiguration
    {
        public static string GetFileName()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            
            var route= Path.Combine(System.IO.Directory.GetCurrentDirectory(), root.GetSection("MySettings").GetSection("ArchivoClaves").Value);

            return route;
        }
    }
}
