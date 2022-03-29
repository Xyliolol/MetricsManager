using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsManager.Jobs.Job
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new RamMetric
            {
                Value = Convert.ToInt32(_ramCounter.NextValue()),
                Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            return Task.CompletedTask;
        }
    }

}