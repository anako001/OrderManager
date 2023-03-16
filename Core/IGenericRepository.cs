namespace OrderManager.Core
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
