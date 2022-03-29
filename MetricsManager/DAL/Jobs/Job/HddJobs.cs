using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsManager.Jobs.Job
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new HddMetric
            {
                Value = Convert.ToInt32(_hddCounter.NextValue()),
                Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            return Task.CompletedTask;
        }
    }
}