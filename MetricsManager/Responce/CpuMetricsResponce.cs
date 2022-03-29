
using MetricsManager.DAL.Models;

namespace MetricsManager.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}