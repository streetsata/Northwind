using Contracts;
using Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected NorthwindContext NorthwindContext { get; set; }

        public RepositoryBase(NorthwindContext northwindContext)
        {
            this.NorthwindContext = northwindContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.NorthwindContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.NorthwindContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.NorthwindContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.NorthwindContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            this.NorthwindContext.Set<T>().Update(entity);
        }
    }
}
