
using AgentManager.Interface;
using AgentManager.Models;
using Dapper;
using MetricsAgent.DAL;
using System.Data.SQLite;

namespace AgentManager.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                //  Запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    // Анонимный объект с параметрами запроса
                    new
                    {
                        // Value подставится на место "@value" в строке запроса
                        // Значение запишется из поля Value объекта item
                        value = item.Value,

                        // Записываем в поле time количество секунд
                        time = item.Time
                    });
            }
        }

        //public void Delete(int id)
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
        //            new
        //            {
        //                id = id
        //            });
        //    }
        //}

        //public void Update(DotNetMetric item)
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id",
        //            new
        //            {
        //                value = item.Value,
        //                time = item.Time,
        //                id = item.Id
        //            });
        //    }
        //}

        //public IList<DotNetMetric> GetAll()
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        // Читаем, используя Query, и в шаблон подставляем тип данных,
        //        // объект которого Dapper, он сам заполнит его поля
        //        // в соответствии с названиями колонок
        //        return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
        //    }
        //}

        //public DotNetMetric GetById(int id)
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        return connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id=@id",
        //            new { id = id });
        //    }
        //}
        public IList<DotNetMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT id, value, time FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}