using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Get(string id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}
