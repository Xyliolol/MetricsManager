using Dapper;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute(
                $"INSERT INTO networkmetrics (agentId,time,value) VALUES (@agentId,@time,@value);",
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
                var result = connection.ExecuteScalar<long>("SELECT Max(time) FROM networkmetrics WHERE agentId = @agentId",

                new { agentId });

                return DateTimeOffset.Parse(result.ToString());

            }
        }

        public IList<NetworkMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Value, Time FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                   new
                   {
                       fromTime = fromTime,
                       toTime = toTime
                   }).ToList();
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT Id,agentId,Time,Value FROM hddmetrics WHERE (agentId = @agentId) time BETWEEN @fromTime AND @toTime",
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