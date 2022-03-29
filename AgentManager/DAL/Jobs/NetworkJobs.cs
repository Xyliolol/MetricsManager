using AgentManager.Interface;
using AgentManager.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;

        // Счётчик для метрики CPU
        private PerformanceCounter _networkCounter;


        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("IPsec Connections", "Total Bytes Out since start");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new NetworkMetric
            {
                Value = Convert.ToInt32(_networkCounter.NextValue()),
                Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            return Task.CompletedTask;
        }
    }
}