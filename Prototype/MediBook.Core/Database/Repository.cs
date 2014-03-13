using System;
using System.Collections.Generic;
using System.Linq;

namespace MediBook.Client.Core.Database
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly SQLiteConnection db;

        private readonly Lazy<IList<T>> items;

        private bool isDisposed;

        public int Count
        {
            get
            {
                return this.items.Value.Count;
            }
        }

        public Repository(SQLiteConnection db)
        {
            this.db = db;
            this.items = new Lazy<IList<T>>(() => db.Table<T>().ToList());
        }

        public T Add(T item)
        {
            this.db.Insert(item);
            return item;
        }

        public IEnumerable<T> Where(Func<T, bool> filter)
        {
            return this.items.Value.Where(filter);
        }


        public IQueryable<T> AsQueryable()
        {
            return this.items.Value.AsQueryable();
        }

        public T SingleOrDefault(Func<T, bool> filter)
        {
            return this.items.Value.SingleOrDefault(filter);
        }

        public T Update(T item)
        {
            this.db.Update(item);
            return item;
        }

        public void Delete(T item)
        {
            this.items.Value.Remove(item);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    if (this.db != null)
                    {
                        this.db.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }
    }
}