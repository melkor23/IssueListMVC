using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    
    public interface IRepositorio<TEntity> where TEntity : class
    {
       
        TEntity Obtain(params object[] id);

        
        IQueryable<TEntity> ObtainQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        void Insert(TEntity entity);


       
        void Update(TEntity entity);

    
        void Delete(params object[] id);

       
        void Delete(TEntity entity);


        int ExecuteSqlCommand(string sql);

       
        void SaveChanges();
    }
}
