using System;
using DataAccess.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Interfaces
{
    public interface ICustomersContext : IDisposable
    {
        DbSet<Addresses> Addresses { get; set; }
        DbSet<Customers> Customers { get; set; }
        int SaveChanges();
        //DbContext methods
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
