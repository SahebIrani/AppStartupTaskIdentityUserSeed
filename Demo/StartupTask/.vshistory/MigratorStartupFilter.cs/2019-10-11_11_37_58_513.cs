using System;
using System.Threading;
using System.Threading.Tasks;

using Demo.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.StartupTask
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
    public class MigratorStartupFilter : IStartupTask
    {
        public MigratorStartupFilter(IServiceProvider serviceProvider)
            => ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        public IServiceProvider ServiceProvider { get; }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using var scope = ServiceProvider.CreateScope();
            var myDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await myDbContext.Database.MigrateAsync();
        }
    }

    public static class StartupTaskWebHostExtensions
    {
        public static async Task RunWithTasksAsync(this IWebHost webHost, CancellationToken cancellationToken = default)
        {
            var startupTasks = webHost.Services.GetServices<IStartupTask>();
            foreach (var startupTask in startupTasks)
                await startupTask.ExecuteAsync(cancellationToken);
            await webHost.RunAsync(cancellationToken);
        }
    }
}
