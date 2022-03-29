using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsManager.Jobs.Job
{
    public class DotNetMetricJob : IJob
    {
        private PerformanceCounter _dotnetCounter;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "Gen 0 heap size", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new DotNetMetric
            {
                Value = Convert.ToInt32(_dotnetCounter.NextValue()),
                Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            return Task.CompletedTask;
        }

    }
}