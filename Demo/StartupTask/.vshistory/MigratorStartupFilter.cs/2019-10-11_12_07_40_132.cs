using System;
using System.Threading;
using System.Threading.Tasks;

using Demo.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync(cancellationToken);

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user0 = await context.Users.ToListAsync();
            IdentityUser user = await userManager.FindByNameAsync("Sinjul.MSBH@Yahoo.Com");
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "Sinjul.MSBH@Yahoo.Com",
                    NormalizedUserName = "Sinjul.MSBH@Yahoo.Com".ToUpperInvariant(),
                    Email = "Sinjul.MSBH@Yahoo.Com",
                    NormalizedEmail = "Sinjul.MSBH@Yahoo.Com".ToUpperInvariant(),
                    //ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                };
                await userManager.CreateAsync(user, "Sinjul_4");
            }
        }
    }

    public static class StartupTaskWebHostExtensions
    {
        public static async Task RunWithTasksAsync(this IHost webHost, CancellationToken cancellationToken = default)
        {
            await webHost.Services.GetService<IStartupTask>().ExecuteAsync(cancellationToken);
            await webHost.RunAsync(cancellationToken);
        }
    }
}
