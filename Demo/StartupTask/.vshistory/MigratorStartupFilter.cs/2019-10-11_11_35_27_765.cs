using System;
using System.Threading;
using System.Threading.Tasks;

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

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _seviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                //Do the migration
                await myDbContext.Database.MigrateAsync();
            }
        }
    }
}
