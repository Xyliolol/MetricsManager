using System.Data.SQLite;

namespace MetricsManager.DAL.Interface
{
    public interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}