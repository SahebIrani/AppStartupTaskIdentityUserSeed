using System.Threading;
using System.Threading.Tasks;

using Demo.StartupTask;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public static class Program
    {
        public static async Task Main(string[] args, CancellationToken ct) => await CreateHostBuilder(args).Build().RunWithTasksAsync(ct);

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
