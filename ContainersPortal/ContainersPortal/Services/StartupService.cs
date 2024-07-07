

using ContainersPortal.Constants;
using ContainersPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ContainersPortal.Services;

public class StartupService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StartupService(IServiceScopeFactory serviceScopeFactory, ILogger<StartupService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        GlobalConstants.HOST_IP_ADDRESS = Environment.GetEnvironmentVariable("HOST_IP_ADDRESS") ??
            throw new Exception("Environment variable 'HOST_IP_ADDRESS' is not set.");
        GlobalConstants.HOST_USERNAME = Environment.GetEnvironmentVariable("HOST_SSH_USERNAME") ??
            throw new Exception("Environment variable 'HOST_SSH_USERNAME' is not set."); ;
        GlobalConstants.HOST_PASSWORD = Environment.GetEnvironmentVariable("HOST_SSH_PASSWORD") ??
            throw new Exception("Environment variable 'HOST_SSH_PASSWORD' is not set."); ;

        _logger.LogInformation("Environment variables read successfully.");

        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                var user = new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    UserName = "admin"
                };
                var password = "Admin12358!";

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
