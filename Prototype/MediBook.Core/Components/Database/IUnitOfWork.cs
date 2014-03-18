namespace MediBook.Client.Core.Components.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
