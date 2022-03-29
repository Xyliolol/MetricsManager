using MetricsManager.DAL.Model;

namespace MetricsManager.DAL.Interface
{
    public interface IAgentsRepository
    {
        void Create(AgentInfo agent);
        IList<AgentInfo> Get();
        AgentInfo GetById(int id);
        void Update(AgentInfo agent);
    }
}