using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Saleforce.Permissions.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, params string[] urls)
        {
            var builder = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            if (urls?.Length > 0)
            {
                builder.UseUrls(urls);
            }

            return builder;
        }

        /// <summary>
        /// To be used by EF tooling.
        /// Hacky, but it works.
        /// </summary>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
