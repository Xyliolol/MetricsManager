using Dapper;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Model;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repository
{
    public class AgentsRepository : IAgentsRepository
    {
        public void Create(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                connection.Execute("INSERT INTO agents (uri,isenabled) VALUES (@uri,@isenabled);",
                new
                {
                    uri = agent.Url,
                    isenabled = agent.IsEnabled
                }
                );
            }
        }

        public IList<AgentInfo> Get()
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                IList<AgentInfo> result = connection.Query<AgentInfo>("SELECT * FROM agents").ToList();

                return result;
            }
        }

        public AgentInfo GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.QuerySingle<AgentInfo>("SELECT * FROM agents WHERE id=@id",
                    new { id });
            }
        }

        public void Update(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("UPDATE agents SET Enabled = @state WHERE AgentId = @agentId",
                    new
                    {
                        agent = agent
                    });
            }
        }
    }
}