namespace Bussines.Interfaces
{
    public interface ITransactionService
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}