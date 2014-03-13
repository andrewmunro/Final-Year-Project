using System;
using System.Collections.Generic;
using System.Linq;

namespace MediBook.Client.Core.Database
{
    public interface IRepository<T> where T : class
    {
        int Count { get; }
        T SingleOrDefault(Func<T, bool> filter);
        IEnumerable<T> Where(Func<T, bool> filter);
        IQueryable<T> AsQueryable();
        T Add(T entity);
        void Delete(T entity);
        T Update(T entity);
    }
}