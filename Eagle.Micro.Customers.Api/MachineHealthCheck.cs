using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Customers.Api
{
    public class MachineHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
