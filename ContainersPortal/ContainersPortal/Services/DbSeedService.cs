

namespace ContainersPortal.Services;

public class DbSeedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
