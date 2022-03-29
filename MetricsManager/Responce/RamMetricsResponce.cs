using MetricsManager.DAL.Models;

namespace MetricsManager.Response
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetric> Metrics { get; set; }
    }
}