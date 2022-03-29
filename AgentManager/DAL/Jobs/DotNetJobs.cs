using AgentManager.Interface;
using AgentManager.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        // Счётчик для метрики CPU
        private PerformanceCounter _dotnetCounter;


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