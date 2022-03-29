
using AgentManager.Interface;
using AgentManager.Models;
using Dapper;
using MetricsAgent.DAL;
using System.Data.SQLite;

namespace AgentManager.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                //  Запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
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
        //        connection.Execute("DELETE FROM rammetrics WHERE id=@id",
        //            new
        //            {
        //                id = id
        //            });
        //    }
        //}

        //public void Update(RamMetric item)
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id=@id",
        //            new
        //            {
        //                value = item.Value,
        //                time = item.Time,
        //                id = item.Id
        //            });
        //    }
        //}

        //public IList<RamMetric> GetAll()
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        // Читаем, используя Query, и в шаблон подставляем тип данных,
        //        // объект которого Dapper, он сам заполнит его поля
        //        // в соответствии с названиями колонок
        //        return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics").ToList();
        //    }
        //}

        //public RamMetric GetById(int id)
        //{
        //    using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
        //    {
        //        return connection.QuerySingle<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE id=@id",
        //            new { id = id });
        //    }
        //}
        public IList<RamMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT id, value, time FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}