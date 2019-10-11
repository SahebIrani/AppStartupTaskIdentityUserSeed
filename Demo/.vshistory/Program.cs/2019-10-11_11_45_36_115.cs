using System.Threading.Tasks;

using Demo.StartupTask;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunWithTasksAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureServices(services => services.AddTransient<IStartupTask, MigratorStartupFilter>())
                    .UseStartup<Startup>();
                });
    }
}
