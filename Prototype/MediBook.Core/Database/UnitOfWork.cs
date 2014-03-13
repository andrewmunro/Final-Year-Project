using System;

namespace MediBook.Client.Core.Database
{
    public class UnitOfWork<T> : IUnitOfWork where T : class, new()
    {
        private readonly SQLiteConnection db;

        private bool isDisposed;

        public IRepository<T> Repository { get; set; }

        public UnitOfWork(SQLiteConnection db)
        {
            this.db = db;
            db.CreateTable<T>();
            this.Repository = new Repository<T>(this.db);

            this.db.BeginTransaction();
        }

        public void Commit()
        {
            this.db.Commit();
        }

        public void Rollback()
        {
            this.db.Rollback();
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    if (this.Repository != null)
                    {
                        ((Repository<T>)this.Repository).Dispose();
                    }

                    if (this.db != null)
                    {
                        this.db.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
