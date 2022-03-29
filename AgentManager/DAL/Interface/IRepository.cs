namespace AgentManager.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(long fromTime, long toTime);    
         void Create(T item);
       
    }
}
