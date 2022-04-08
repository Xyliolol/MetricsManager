using Dapper;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repository
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {

        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute(
                 $"INSERT INTO dotnetmetrics (agentId,Time,Value) VALUES (@agentId,@time,@value);",
                     new
                     {
                         AgentId = item.AgentId,
                         Time = item.Time,
                         Value = item.Value,
                     }
                 );
            }
        }

        public DateTimeOffset GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {

                var result = connection.ExecuteScalar<long>("SELECT Max(Time) FROM dotnetmetrics WHERE agentId = @agentId",

                new { agentId });

                return DateTimeOffset.Parse(result.ToString());
            }
        }

        public IList<DotNetMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Value, Time FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime",
                     new
                     {
                         fromTime = fromTime,
                         toTime = toTime
                     }).ToList();
            }
        }

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {

                return connection.Query<DotNetMetric>("SELECT Id,agentId,Time,Value FROM dotnetmetrics WHERE (agentId = @agentId) time BETWEEN @fromTime AND @toTime",
                       new
                       {
                           agentId = agentId,
                           fromTime = fromTime.ToUnixTimeMilliseconds(),
                           toTime = toTime.ToUnixTimeMilliseconds()
                       }).ToList();
            }
        }
    }
}
