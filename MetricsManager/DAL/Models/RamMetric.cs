namespace MetricsManager.DAL.Models
{
    public class RamMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }

        public int AgentId { get; set; }
    }
}
