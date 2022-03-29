namespace MetricsManager.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(long fromTime, long toTime);
        IList<T> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId);

        void Create(T item);
        DateTimeOffset GetAgentLastMetricDate(int agentId);
    }
}