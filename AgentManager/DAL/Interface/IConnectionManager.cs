using System.Data.SQLite;

namespace MetricsAgent.DAL.Interface
{
    public interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}