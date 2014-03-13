namespace MediBook.Client.Core.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
