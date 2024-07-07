using ContainersPortal.Database;
using Microsoft.EntityFrameworkCore;

namespace ContainersPortal.Extensions;

public static class DatabaseManager
{
    public static IHost MigrateDatabase(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<UserContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        return webHost;
    }

    public static void EnsureDatabaseCreated(this IApplicationBuilder app, IConfiguration config)
    {
        using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<UserContext>())
            {
                context?.Database.EnsureCreated();
            }
        }
    }
}