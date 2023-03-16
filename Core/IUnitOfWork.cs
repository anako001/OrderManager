namespace OrderManager.Core
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }

        Task CompleteAsync();
    }
}
